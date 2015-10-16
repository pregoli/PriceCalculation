using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Entities.Base
{
    public class Basket
    {
        public Basket()
        {
            Products = new List<Product>();
            Offers = new List<Offer>();
        }

        public List<Product> Products { get; set; }
        public List<Offer> Offers { get; set; }
    }
}
