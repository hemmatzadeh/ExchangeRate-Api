using System.ComponentModel.DataAnnotations;

namespace NF.ExchangeRates.Api.Contracts
{
    public class GetRateRequest
    {
        [Required]
        [Range(minimum: 1, int.MaxValue, ErrorMessage = "userid should be greater than 0")]
        public int UserId { get; set; }
        
        [Required]
        public string From { get; set; }
 
        [Required]
        public string To { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"UserId:{UserId}, From:{From}, To:{To}";
        }
    }


}