FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Gateway/OmniMarket.Gateway/OmniMarket.Gateway.csproj", "src/Services/Gateway/OmniMarket.Gateway/"]
RUN dotnet restore "./src/Services/Gateway/OmniMarket.Gateway/OmniMarket.Gateway.csproj"

COPY . .
WORKDIR "/src/src/Services/Gateway/OmniMarket.Gateway"
RUN dotnet build "./OmniMarket.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OmniMarket.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OmniMarket.Gateway.dll"]