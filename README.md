# ExchangeRate-Api
Sample for a microservice Api for Exchange Rate that can read Exchage rate data from more than 1 provider

---

## Functionalitis:

-  [x] Integrating to 2 rate provider
```https://img.shields.io/static/v1?label=<LABEL>&message=<MESSAGE>&color=<COLOR> https://currencylayer.com/```
```https://img.shields.io/static/v1?label=<LABEL>&message=<MESSAGE>&color=<COLOR> https://apilayer.com/marketplace/currency_data-api```

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
- [x] Unit Tests
- [x] Logging
