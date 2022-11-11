using System.ComponentModel.DataAnnotations;

namespace NF.ExchangeRates.Api.Contracts
{
    public class GetRateRequest
    {        
        [Required]
        public string From { get; set; }
 
        [Required]
        public string To { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"From:{From}, To:{To}";
        }
    }


}