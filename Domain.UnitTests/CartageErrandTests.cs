using Domain.CartageErrand;
using Domain.CartageOffer;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Domain.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests
{
    [TestFixture]
    public class CartageErrandTests
    {
        private static User.User UserTest { get; } = new User.User("Mr. Founder", "mrF0under@domain.com");
        private static CartageErrand.CartageErrand CreateCartageErrand(
                User.User founder, string goods, string startingAdress, string destinationAdress, int distance, float weight, int maxPrice, TimeSpan timeToEnd, CartageErrandExecutionStatus executionStatus
            )
        {
            return new CartageErrand.CartageErrand(
                    founder,
                    goods,
                    startingAdress,
                    destinationAdress,
                    distance,
                    weight,
                    maxPrice,
                    DateTime.Now + timeToEnd,
                    executionStatus
                );
        }

        private static CartageErrand.CartageErrand CreateCartageErrand(
                string goods, string startingAdress, string destinationAdress, int distance, float weight, int maxPrice, TimeSpan timeToEnd, CartageErrandExecutionStatus executionStatus
            )
        {
            return CreateCartageErrand(UserTest, goods, startingAdress, destinationAdress, distance, weight, maxPrice, timeToEnd, executionStatus);
        }

        private static CartageErrand.CartageErrand CreateCartageErrand(CartageErrandExecutionStatus executionStatus)
        {
            return CreateCartageErrand(
                UserTest,
                "wooden planks",
                "Radom ul. Zagajnikowa 3s",
                "Poznań al. Meblowa 28/3",
                400,
                15.3f,
                4000,
                new TimeSpan(12, 0, 0),
                executionStatus
                );
        }

        private CartageOffer.CartageOffer CreateCartageOffer(User.User bidder, int price, CartageOfferConsiderationStatus considerationStatus )
        {
            return new CartageOffer.CartageOffer(bidder, price, considerationStatus);
        }

        private CartageOffer.CartageOffer CreateCartageOffer(int price)
        {
            return CreateCartageOffer(UserTest, price, CartageOfferConsiderationStatus.Waiting);
        }

        private CartageOffer.CartageOffer CreateAcceptableOffer(this CartageErrand.CartageErrand cartageErrand)
        {
            return CreateCartageOffer(new User.User("Użytkownik", "Adres@domena.pl"), (cartageErrand.MaximumPrice / 2), CartageOfferConsiderationStatus.Waiting);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(CartageErrandExecutionStatus.Cancelled)]
        [TestCase(CartageErrandExecutionStatus.Failure)]
        [TestCase(CartageErrandExecutionStatus.Success)]
        public void CartageErrandCannotGetCancelledWhenItIsNotActive(CartageErrandExecutionStatus executionStatus)
        {
            CartageErrand.CartageErrand testCartageErrand = CreateCartageErrand(executionStatus);
            Assert.Throws<CartageErrandExecutionStatusChangeNotAllowedException>(() => testCartageErrand.TryCancel());
        }

        [Test]
        [TestCase(CartageErrandExecutionStatus.Cancelled)]
        [TestCase(CartageErrandExecutionStatus.Failure)]
        [TestCase(CartageErrandExecutionStatus.Success)]
        public void CartageErrandCannotGetFinishedWhenItIsNotActive(CartageErrandExecutionStatus executionStatus)
        {
            CartageErrand.CartageErrand testCartageErrand = CreateCartageErrand(executionStatus);
            Assert.Throws<CartageErrandExecutionStatusChangeNotAllowedException>(() => testCartageErrand.TryFinish());
        }

        [Test]
        [TestCase(CartageErrandExecutionStatus.Cancelled)]
        [TestCase(CartageErrandExecutionStatus.Failure)]
        [TestCase(CartageErrandExecutionStatus.Success)]
        public void CartageErrandCannotReceiveNewCartageOffersWhenItIsNotActive(CartageErrandExecutionStatus executionStatus)
        {
            CartageErrand.CartageErrand testCartageErrand = CreateCartageErrand(executionStatus);
            Assert.Throws<CartageErrandNewCartageOfferReceivingNotAllowedException>(() => testCartageErrand.TryAddOffer(testCartageErrand.CreateAcceptableOffer()));//TODO why cant use extension method?
        }

        [Test]
        public void CartageErrandCanReceiveNewCartageOffersFittingMaximumPriceWhenItIsInActiveState()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.TryAddOffer(CreateCartageOffer());
            testCartageErrand.TryAddOffer(CreateCartageOffer());

            Assert.That(testCartageErrand.GetSubmittedCartageOffers().Count == 2);
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToCancelledAfterItGetsCancelled()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.TryCancel();
            
            Assert.That(testCartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Cancelled);
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToSuccessAfterItGetsFinishedWhileHavingAtLeastOneCartageOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.TryAddOffer(CreateCartageOffer());

            testCartageErrand.TryFinish();

            Assert.That(testCartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Success);
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToFailureAfterItGetsFinishedWhileHavingNoAnyCartageOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.TryFinish();

            Assert.That(testCartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Failure);
        }
        //TODO add rest of the tests
        [Test]
        public void CartageErrandHasToChooseTheCheapestOfferAfterItGetsCancelled()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CartageErrandCannotAddCartageOfferThatOffersPriceHigherThanMaximumPrice()
        {
            throw new NotImplementedException();
        }

    }
}
