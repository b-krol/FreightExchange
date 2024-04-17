using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests
{
    [TestFixture]
    public class CartageOfferTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(-1)]
        [TestCase(-100)]
        public void CartageOfferCannotBeCreatedWithPriceLowerThanZero(int price)
        {
            Assert.Throws<ArgumentException>(
                    () => new CartageOffer.CartageOffer(
                        new User.User("Mr. Applicant", "Applicant@domain.com"),
                        price,
                        CartageOffer.CartageOfferConsiderationStatus.Waiting
                    )
                );
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void CartageOfferCanBeCreatedWithPriceHigherOrEqualZero(int price)
        {
            var newCartageOffer = new CartageOffer.CartageOffer(
                        new User.User("Mr. Applicant", "Applicant@domain.com"),
                        price,
                        CartageOffer.CartageOfferConsiderationStatus.Waiting);
            Assert.That(newCartageOffer.Price, Is.EqualTo(price));
        }
    }
}
