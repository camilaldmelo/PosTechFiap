# Use a imagem oficial do SDK do .NET 7.0 como base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use a imagem oficial do SDK do .NET 7.0 para construir o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /.
COPY ["PaymentAPI/API.csproj", "PaymentAPI/"]
RUN dotnet restore "PaymentAPI/API.csproj"
COPY . .
WORKDIR "/PaymentAPI"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

# Criar a imagem final do aplicativo
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
