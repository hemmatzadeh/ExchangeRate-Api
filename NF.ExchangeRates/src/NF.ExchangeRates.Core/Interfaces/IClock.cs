namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}
