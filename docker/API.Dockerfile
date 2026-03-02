# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY src/AgroSolutions.Properties.API/AgroSolutions.Properties.API.csproj AgroSolutions.Properties.API/
COPY src/AgroSolutions.Properties.Application/AgroSolutions.Properties.Application.csproj AgroSolutions.Properties.Application/
COPY src/AgroSolutions.Properties.Domain/AgroSolutions.Properties.Domain.csproj AgroSolutions.Properties.Domain/
COPY src/AgroSolutions.Properties.Data/AgroSolutions.Properties.Data.csproj AgroSolutions.Properties.Data/

RUN dotnet restore AgroSolutions.Properties.API/AgroSolutions.Properties.API.csproj

COPY src .
WORKDIR /src/AgroSolutions.Properties.API
RUN dotnet publish "AgroSolutions.Properties.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AgroSolutions.Properties.API.dll"]