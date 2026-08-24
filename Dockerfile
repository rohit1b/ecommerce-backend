# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy everything and restore
COPY ECommerceApp/ ./ECommerceApp/
WORKDIR /src/ECommerceApp/ECommerce.API
RUN dotnet restore

# Publish
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Render provides the PORT env variable; ASP.NET Core needs to listen on it
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "ECommerce.API.dll"]