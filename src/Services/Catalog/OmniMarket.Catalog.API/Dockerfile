# 1. Aşama: Çalışma Zamanı (Runtime) İmajı
# Uygulamanın ayağa kalkacağı hafif katman
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 2. Aşama: Derleme (Build) İmajı
# Tüm projeleri derlemek için ağır olan SDK imajını kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Katmanlı mimari kullandığımız için tüm .csproj dosyalarını Docker içerisine kopyalıyoruz
COPY ["src/Services/Catalog/OmniMarket.Catalog.API/OmniMarket.Catalog.API.csproj", "src/Services/Catalog/OmniMarket.Catalog.API/"]
COPY ["src/Services/Catalog/OmniMarket.Catalog.Application/OmniMarket.Catalog.Application.csproj", "src/Services/Catalog/OmniMarket.Catalog.Application/"]
COPY ["src/Services/Catalog/OmniMarket.Catalog.Domain/OmniMarket.Catalog.Domain.csproj", "src/Services/Catalog/OmniMarket.Catalog.Domain/"]
COPY ["src/Services/Catalog/OmniMarket.Catalog.Infrastructure/OmniMarket.Catalog.Infrastructure.csproj", "src/Services/Catalog/OmniMarket.Catalog.Infrastructure/"]

# Bağımlılıkları restore ediyoruz (Paketleri indiriyoruz)
RUN dotnet restore "./src/Services/Catalog/OmniMarket.Catalog.API/OmniMarket.Catalog.API.csproj"

# Kalan tüm kaynak kodları Docker'a kopyalıyoruz
COPY . .
WORKDIR "/src/src/Services/Catalog/OmniMarket.Catalog.API"
RUN dotnet build "./OmniMarket.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Aşama: Yayınlama (Publish) Aşaması
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OmniMarket.Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Aşama: Final Aşaması
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Docker içindeki uygulamanın Production ayarlarını okumasını söylüyoruz
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "OmniMarket.Catalog.API.dll"]