using BroadbandChoices.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Entities
{
    public class Milk : Product
    {
        public override double UnitPrice
        {
            get
            {
                return 1.15;
            }
        }
    }
}
