using NF.ExchangeRates.Core.Enums;

namespace NF.ExchangeRates.Core
{
    public class ExchangeRate
    {
        public ApiProviders Provider { get; set; }
        public string BaseCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Created { get; set; }
        public string Message { get; set; }
    }
}
