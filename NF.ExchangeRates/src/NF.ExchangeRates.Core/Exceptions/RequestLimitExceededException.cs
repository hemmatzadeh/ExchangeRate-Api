namespace NF.ExchangeRates.Core.Exceptions
{
    public class RequestLimitExceededException : Exception
    {
        public RequestLimitExceededException(string message) : base(message) { }
    }
}
