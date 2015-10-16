using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Entities.Base
{
    public class Product
    {
        public virtual double UnitPrice { get; set; }
        public double FinalPrice { get; set; }
    }
}
