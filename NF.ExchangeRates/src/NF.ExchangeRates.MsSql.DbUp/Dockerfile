#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/NF.ExchangeRates.MsSql.DbUp/NF.ExchangeRates.MsSql.DbUp.csproj", "src/NF.ExchangeRates.MsSql.DbUp/"]
RUN dotnet restore "src/NF.ExchangeRates.MsSql.DbUp/NF.ExchangeRates.MsSql.DbUp.csproj"
COPY . .
WORKDIR "/src/src/NF.ExchangeRates.MsSql.DbUp"
RUN dotnet build "NF.ExchangeRates.MsSql.DbUp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NF.ExchangeRates.MsSql.DbUp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NF.ExchangeRates.MsSql.DbUp.dll"]