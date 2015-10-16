using BroadbandChoices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Services
{
    public interface IBasketService<T,D>
    {
        void Add(T product, D basket, out D basketUpdated);
        void Remove(T product, D basket, out D basketUpdated);
        BasketResult CalculateTotalPrice(D basket);
    }
}
