# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . ./
WORKDIR /src/MyShopAPI

RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Bring in the published app
COPY --from=build /app/publish ./

# ✅ Temporary diagnostics
RUN dotnet --info \
 && echo "📂 Contents of /app:" \
 && ls -la /app \
 && echo "🔍 Searching for System.IdentityModel.Tokens.Jwt.dll:" \
 && (ls -la /app/System.IdentityModel.Tokens.Jwt.dll && echo "✅ Jwt DLL found") || echo "❌ Jwt DLL NOT found in /app"

# Runtime settings
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "MyShopAPI.dll"]
