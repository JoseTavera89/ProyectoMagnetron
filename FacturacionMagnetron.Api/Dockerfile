#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV DB_CONNECTION_STRING="Server=192.168.10.2,1433;Database=Magnetron;User Id=soporte;Password=soporte;MultipleActiveResultSets=true;TrustServerCertificate=True;"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FacturacionMagnetron.Api/FacturacionMagnetron.Api.csproj", "FacturacionMagnetron.Api/"]
COPY ["FacturacionMagnetron.Application/FacturacionMagnetron.Application.csproj", "FacturacionMagnetron.Application/"]
COPY ["FacturacionMagnetron.Domain/FacturacionMagnetron.Domain.csproj", "FacturacionMagnetron.Domain/"]
COPY ["FacturacionMagnetron.Infrastructure/FacturacionMagnetron.Infrastructure.csproj", "FacturacionMagnetron.Infrastructure/"]
RUN dotnet restore "./FacturacionMagnetron.Api/FacturacionMagnetron.Api.csproj"
COPY . .
WORKDIR "/src/FacturacionMagnetron.Api"
RUN dotnet build "./FacturacionMagnetron.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FacturacionMagnetron.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FacturacionMagnetron.Api.dll"]