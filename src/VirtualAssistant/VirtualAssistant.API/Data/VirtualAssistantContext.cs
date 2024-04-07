using Microsoft.EntityFrameworkCore;
using VirtualAssistant.API.Data.Domain;

namespace VirtualAssistant.API.Data
{
    public class VirtualAssistantContext(DbContextOptions<VirtualAssistantContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
