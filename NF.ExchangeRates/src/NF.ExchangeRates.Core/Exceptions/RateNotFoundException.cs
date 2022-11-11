namespace NF.ExchangeRates.Core.Exceptions
{
    public class RateNotFoundException : Exception
    {
        public RateNotFoundException(string message) : base(message) { }
    }
}
