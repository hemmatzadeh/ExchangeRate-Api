using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ExchangeRates.Core
{
    public class ExchangeResult
    {
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
    }
}
