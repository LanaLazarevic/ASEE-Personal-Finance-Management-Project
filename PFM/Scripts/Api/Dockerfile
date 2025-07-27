FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PFM.Domain/PFM.Domain.csproj", "PFM.Domain/"]
COPY ["PFM.Application/PFM.Application.csproj", "PFM.Application/"]
COPY ["PFM.Infrastructure/PFM.Infrastructure.csproj", "PFM.Infrastructure/"]
COPY ["PFM.Api/PFM.Api.csproj", "PFM.Api/"]
RUN dotnet restore "PFM.Api/PFM.Api.csproj"
COPY . .
WORKDIR "/src/PFM.Api"
RUN dotnet build "PFM.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PFM.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PFM.Api.dll"]