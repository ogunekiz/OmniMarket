FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Tüm katmanların csproj dosyalarını kopyalıyoruz
COPY ["src/Services/Order/OmniMarket.Order.API/OmniMarket.Order.API.csproj", "src/Services/Order/OmniMarket.Order.API/"]
COPY ["src/Services/Order/OmniMarket.Order.Application/OmniMarket.Order.Application.csproj", "src/Services/Order/OmniMarket.Order.Application/"]
COPY ["src/Services/Order/OmniMarket.Order.Domain/OmniMarket.Order.Domain.csproj", "src/Services/Order/OmniMarket.Order.Domain/"]
COPY ["src/Services/Order/OmniMarket.Order.Infrastructure/OmniMarket.Order.Infrastructure.csproj", "src/Services/Order/OmniMarket.Order.Infrastructure/"]

# Bağımlılıkları restore ediyoruz
RUN dotnet restore "./src/Services/Order/OmniMarket.Order.API/OmniMarket.Order.API.csproj"

# Tüm kodu kopyalayıp build ediyoruz
COPY . .
WORKDIR "/src/src/Services/Order/OmniMarket.Order.API"
RUN dotnet build "./OmniMarket.Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OmniMarket.Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OmniMarket.Order.API.dll"]