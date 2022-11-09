namespace NF.ExchangeRates.Core.Entities
{
    public class RateInfo : EntityBase
    {
        public string BaseCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public DateTime Created { get; set; }
    }
}
