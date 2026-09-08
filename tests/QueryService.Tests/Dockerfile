FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["tests/QueryService.Tests/QueryService.Tests.csproj", "tests/QueryService.Tests/"]
COPY ["src/QueryService.Application/QueryService.Application.csproj", "src/QueryService.Application/"]
COPY ["src/QueryService.Domain/QueryService.Domain.csproj", "src/QueryService.Domain/"]
COPY ["src/QueryService.Infrastructure/QueryService.Infrastructure.csproj", "src/QueryService.Infrastructure/"]
RUN dotnet restore "tests/QueryService.Tests/QueryService.Tests.csproj"
COPY . .
WORKDIR "tests/QueryService.Tests"
RUN dotnet build "./QueryService.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QueryService.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueryService.Tests.dll"]