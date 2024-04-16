using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartageOffer
{
    public class CartageOffer
    {
        public int Id {  get; set; }
        public User.User Applicant {  get; set; }
        public int Price { get; set; }
        public CartageOfferConsiderationStatus ConsiderationStatus { get; set; }

        public CartageOffer(User.User applicant, int requestedPrice, CartageOfferConsiderationStatus considerationStatus)
        {
            Applicant = applicant;
            Price = requestedPrice;
            ConsiderationStatus = considerationStatus;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as CartageOffer);
        }

        public bool Equals(CartageOffer other)
        {
            if(Id != other.Id)
                return false;
            if(Applicant != other.Applicant)
                return false;
            if(Price != other.Price)
                return false;
            if(ConsiderationStatus != other.ConsiderationStatus)
                return false;
            return true;
        }
    }
}
