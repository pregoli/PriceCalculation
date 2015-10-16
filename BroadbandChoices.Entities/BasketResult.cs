using BroadbandChoices.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Entities
{
    public class BasketResult : Basket
    {
        public double OriginalPrice { get; set; }
        public double FinalPrice { get; set; }
        public double SavedAmount { get; set; }
    }
}
