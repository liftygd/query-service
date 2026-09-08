FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QueryService.API/QueryService.API.csproj", "QueryService.API/"]
COPY ["QueryService.Infrastructure/QueryService.Infrastructure.csproj", "QueryService.Infrastructure/"]
COPY ["QueryService.Domain/QueryService.Domain.csproj", "QueryService.Domain/"]
COPY ["QueryService.Application/QueryService.Application.csproj", "QueryService.Application/"]
RUN dotnet restore "QueryService.API/QueryService.API.csproj"
COPY . .
WORKDIR "/src/QueryService.API"
RUN dotnet build "./QueryService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QueryService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueryService.API.dll"]
