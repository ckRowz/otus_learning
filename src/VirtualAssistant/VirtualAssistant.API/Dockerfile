FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VirtualAssistant.API.csproj", "VirtualAssistant.API/"]
RUN dotnet restore "./VirtualAssistant.API/VirtualAssistant.API.csproj"

WORKDIR "/src/VirtualAssistant.API"
COPY . .
RUN dotnet build "./VirtualAssistant.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VirtualAssistant.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VirtualAssistant.API.dll"]