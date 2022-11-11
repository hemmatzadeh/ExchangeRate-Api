using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Core
{ 
    public class Clock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
