using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;
using System.Text.Json;
using VirtualAssistant.API.Data;

namespace VirtualAssistant.API
{
    public class Program
    {
        protected Program() { }

        private const string CorsPolicy = "CorsPolicy";
        // -> IConfigutation
        private const int KestrelPort = 8000;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks();

            builder.Services.AddDbContext<VirtualAssistantContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("VirtAssDb")));

            builder.WebHost
                .UseKestrel()
                .UseUrls($"http://+:{KestrelPort}");

            AddCors(builder.Services);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors(CorsPolicy);

            ConfigureEndpoints(app);
            MigrateDb(app.Services);

            app.Run();
        }

        private static void MigrateDb(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<VirtualAssistantContext>();

            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }

        //для домашки пойдет
        private static void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder => builder
                    .SetIsOriginAllowed(host => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        private static void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = WriteResponse
            }).RequireCors(CorsPolicy);
        }

        private static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", "OK");
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(
                Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    }
}
