namespace NF.ExchangeRates.Core.Entities
{
    public class UserExchangeInfo : EntityBase
    {
        public int UserId { get; set; }
        public string BaseCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal ConvertedAmount { get; set; }
        public DateTime Created { get; set; }
    }
}
