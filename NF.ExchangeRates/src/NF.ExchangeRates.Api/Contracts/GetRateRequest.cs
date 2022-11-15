using System.ComponentModel.DataAnnotations;

namespace NF.ExchangeRates.Api.Contracts
{
    public class GetRateRequest
    {        
        public string From { get; set; }        
        public string To { get; set; } = string.Empty;
        public override string ToString() => $"From:{From}, To:{To}";        
    }
}