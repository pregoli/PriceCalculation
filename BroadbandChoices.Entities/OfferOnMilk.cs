using BroadbandChoices.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Entities
{
    public class OfferOnMilk : Offer
    {
        public override string Message
        {
            get
            {
                return "Congratulations! Based on your purchase you are eligible for our offer 'Buy 3 Milk and get the 4th milk for free'.";
            }
        }
    }
}
