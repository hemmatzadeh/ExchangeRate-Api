#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/NF.ExchangeRates.Api/NF.ExchangeRates.Api.csproj", "src/NF.ExchangeRates.Api/"]
COPY ["src/NF.ExchangeRates.Core/NF.ExchangeRates.Core.csproj", "src/NF.ExchangeRates.Core/"]
COPY ["src/NF.ExchangeRates.CurrencyLayer/NF.ExchangeRates.CurrencyLayer.csproj", "src/NF.ExchangeRates.CurrencyLayer/"]
COPY ["src/NF.ExchangeRates.MSSql/NF.ExchangeRates.MsSql.csproj", "src/NF.ExchangeRates.MSSql/"]
RUN dotnet restore "src/NF.ExchangeRates.Api/NF.ExchangeRates.Api.csproj"
COPY . .
WORKDIR "src/NF.ExchangeRates.Api"
RUN dotnet build "NF.ExchangeRates.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NF.ExchangeRates.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NF.ExchangeRates.Api.dll"]