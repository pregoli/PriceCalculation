using BroadbandChoices.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Entities
{
    public class OfferOnButter : Offer
    {
        public override string Message
        {
            get
            {
                return "Congratulations! Based on your purchase you are eligible for our offer 'Buy 2 Butter and get a Bread at 50% off'.";
            }
        }

        public override int TotalItems { get; set; }
    }
}
