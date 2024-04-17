using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CartageOffers
{
    public class CartageOfferDto
    {
        public int? Id { get; init; }
        public int BidderId { get; init; }
        public int Price { get; init; }
        public bool? HasBeenConsidered { get; init; }
        public bool? HasBeenAccepted { get; init; }
    }
}
