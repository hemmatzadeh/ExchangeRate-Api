# ExchangeRate-Api
Sample for a microservice Api for Exchange Rate that can read Exchage rate data from more than 1 provider.

---
![screen shot](https://github.com/navidfaridi/ExchangeRate-Api/blob/main/execution-01.png)
## Functionalitis:

-  [x] Integrating to 2 rate providers : [CurrencyLayer]( https://currencylayer.com/ ) and [ApiLayer-Currency-data]( https://apilayer.com/marketplace/currency_data-api)

- [x] Retainig information abount currency exchange trades carried out by its clients in database
- [x] When an exchange rate is used, it cached and if not older than 30 minutes it used again in other exchanges (30 minutes is configurable in appSettings.json)
- [x] Limiting each client to 10 currency exchange trades per hour ( number 10, is configurable in appSettings.json)

---

## Technologies

- [x] C# (.NET 6 Web Api)
- [x] Docker/ Docker Compose
- [x] Entity Framework Core
- [x] MsSQL Server 2022
- [x] RESTful APIs
- [x] xUnit Tests
- [x] Fluent Assertion
- [x] Caching
- [x] Logging
- [x] Fluent Validation
- [x] HealthChecks

---

## How to run

- first go to the NF.ExchangeRate.Api project folder, open appSettings.Json and put your [access key](https://github.com/navidfaridi/ExchangeRate-Api/blob/main/NF.ExchangeRates/src/NF.ExchangeRates.Api/appsettings.json#L17) or [api key](https://github.com/navidfaridi/ExchangeRate-Api/blob/main/NF.ExchangeRates/src/NF.ExchangeRates.Api/appsettings.json#L22) for currencylayer or apilayer
- if you have accesskey fo CurrencyLayer, go to the line [29](https://github.com/navidfaridi/ExchangeRate-Api/blob/main/NF.ExchangeRates/src/NF.ExchangeRates.Api/Program.cs#L29) of program.cs and un-comment it, then comment line [30](https://github.com/navidfaridi/ExchangeRate-Api/blob/main/NF.ExchangeRates/src/NF.ExchangeRates.Api/Program.cs#L30)
- then go to the NF.ExchangeRates folder and run this command in terminal : `docker compose up --build`
- when docker compose built and finished its work, in docker desktop execute DbUp project once, it creates database and required tables
- now in your browser try http://localhost/swagger and see the api list and ...
- also you can check healthcheck endpoint by this url : http://localhost/healthz
