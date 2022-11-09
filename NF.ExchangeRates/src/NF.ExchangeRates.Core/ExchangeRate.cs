namespace NF.ExchangeRates.Core
{
    public class ExchangeRate
    {
        public string BaseCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Created { get; set; }
    }
}
