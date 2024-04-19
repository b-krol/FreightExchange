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
using System.Diagnostics.Metrics;

namespace Domain.UnitTests
{
    [TestFixture]
    public class CartageErrandTests
    {
        private (User.User, string?, string?, string?, int, float, int, DateTime) CorrectValuesTuple;
        private static CartageErrand.CartageErrand CreateAcceptableCartageErrand()
        {
            return new CartageErrand.CartageErrand(
                new User.User("Mr. Founder", "mrF0under@domain.com"),
                "wooden planks",
                "Radom ul. Zagajnikowa 3s",
                "Poznań al. Meblowa 28/3",
                400,
                15.3f,
                4000,
                DateTime.Now + new TimeSpan(12, 0, 0)
                );
        }

        [SetUp]
        public void Setup()
        {
            CorrectValuesTuple = (
                new User.User("Mr. Founder", "mrF0under@domain.com"),
                "wooden planks",
                "Radom ul. Zagajnikowa 3s",
                "Poznań al. Meblowa 28/3",
                400,
                15.3f,
                4000,
                DateTime.Now + new TimeSpan(12, 0, 0)
            );
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void CartageErrandCannotBeCreatedWithEmptyGoodsName(string? goodsName)
        {
            CorrectValuesTuple.Item2 = goodsName;
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(CorrectValuesTuple.Item1, CorrectValuesTuple.Item2!, CorrectValuesTuple.Item3!, CorrectValuesTuple.Item4!, CorrectValuesTuple.Item5, CorrectValuesTuple.Item6, CorrectValuesTuple.Item7, CorrectValuesTuple.Item8)
                );
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void CartageErrandCannotBeCreatedWithEmptyStartingAdress(string? startingAdress)
        {
            CorrectValuesTuple.Item3 = startingAdress;
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(CorrectValuesTuple.Item1, CorrectValuesTuple.Item2!, CorrectValuesTuple.Item3!, CorrectValuesTuple.Item4!, CorrectValuesTuple.Item5, CorrectValuesTuple.Item6, CorrectValuesTuple.Item7, CorrectValuesTuple.Item8)
                );
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("      ")]
        public void CartageErrandCannotBeCreatedWithEmptyDestinationAdress(string? destinationAdress)
        {
            CorrectValuesTuple.Item4 = destinationAdress;
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(CorrectValuesTuple.Item1, CorrectValuesTuple.Item2!, CorrectValuesTuple.Item3!, CorrectValuesTuple.Item4!, CorrectValuesTuple.Item5, CorrectValuesTuple.Item6, CorrectValuesTuple.Item7, CorrectValuesTuple.Item8)
                );
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CartageOfferCannotBeCreatedWithLowerOrEqualZeroDistance(int distance)
        {
            CorrectValuesTuple.Item5 = distance;
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(CorrectValuesTuple.Item1, CorrectValuesTuple.Item2!, CorrectValuesTuple.Item3!, CorrectValuesTuple.Item4!, CorrectValuesTuple.Item5, CorrectValuesTuple.Item6, CorrectValuesTuple.Item7, CorrectValuesTuple.Item8)
                );
        }

        [Test]
        [TestCase(0f)]
        [TestCase(-0.01f)]
        public void CartageOfferCannotBeCreatedWithLowerOrEqualZeroWeight(float weight)
        {
            CorrectValuesTuple.Item6 = weight;
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(CorrectValuesTuple.Item1, CorrectValuesTuple.Item2!, CorrectValuesTuple.Item3!, CorrectValuesTuple.Item4!, CorrectValuesTuple.Item5, CorrectValuesTuple.Item6, CorrectValuesTuple.Item7, CorrectValuesTuple.Item8)
                );
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void CartageOfferCannotBeCreatedWithLowerOrEqualZeroMaximumPrice(int maximumPrice)
        {
            CorrectValuesTuple.Item7 = maximumPrice;
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(CorrectValuesTuple.Item1, CorrectValuesTuple.Item2!, CorrectValuesTuple.Item3!, CorrectValuesTuple.Item4!, CorrectValuesTuple.Item5, CorrectValuesTuple.Item6, CorrectValuesTuple.Item7, CorrectValuesTuple.Item8)
                );
        }

        [Test]
        public void CartageOfferCanBeCreatedWithCorrectData()
        {
            var founder = new User.User("Mr. Founder", "mrF0under@domain.com");
            var goodsName = "wooden planks  ";
            var startingAdress = "  Radom ul. Zagajnikowa 3s ";
            var destinationAdress = "      Poznań al. Meblowa 28/3";
            var distance = 400;
            var weight = 15.3f;
            var maxPrice = 4000;
            var endDate = DateTime.Now + new TimeSpan(12, 0, 0);
            var x = new CartageErrand.CartageErrand(
                founder,
                goodsName,
                startingAdress,
                destinationAdress,
                distance,
                weight,
                maxPrice,
                endDate
                );

            Assert.That(x.Founder, Is.EqualTo(founder));
            Assert.That(x.GoodsName, Is.EqualTo(goodsName.Trim()));
            Assert.That(x.StartingAdress, Is.EqualTo(startingAdress.Trim()));
            Assert.That(x.DestinationAdress, Is.EqualTo(destinationAdress.Trim()));
            Assert.That(x.Distance, Is.EqualTo(distance));
            Assert.That(x.Weight, Is.EqualTo(weight));
            Assert.That(x.MaximumPrice, Is.EqualTo(maxPrice));
            Assert.That(x.EndDate, Is.EqualTo(endDate));
            Assert.That(x.ExecutionStatus, Is.EqualTo(CartageErrandExecutionStatus.Active));
        }

        [Test]
        public void CartageErrandCannotGetCancelledWhenItIsNotActive()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();
            testCartageErrand.Cancel();
            Assert.Throws<CartageErrandExecutionStatusChangeNotAllowedException>(() => testCartageErrand.Cancel());
        }

        [Test]
        public void CartageErrandCannotGetFinishedWhenItIsNotActive()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();
            testCartageErrand.Finish();
            Assert.Throws<CartageErrandExecutionStatusChangeNotAllowedException>(() => testCartageErrand.Finish());
        }

        [Test]
        public void CartageErrandCannotReceiveNewCartageOffersWhenItIsFinished()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();
            testCartageErrand.Finish();
            Assert.Throws<CartageErrandAddingNewCartageOfferNotAcceptedException>(() => testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer()));
        }

        [Test]
        public void CartageErrandCannotReceiveNewCartageOffersWhenItIsCancelled()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();
            testCartageErrand.Cancel();
            Assert.Throws<CartageErrandAddingNewCartageOfferNotAcceptedException>(() => testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer()));
        }

        [Test]
        public void CartageErrandCanReceiveNewCartageOffersFittingMaximumPriceWhenItIsInActiveState()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();

            int N = 3;
            for(int i = 0; i < N; i++)
            {
                testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            }

            Assert.That(testCartageErrand.GetSubmittedCartageOffers().Count, Is.EqualTo(N));
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToCancelledAfterItGetsCancelled()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();

            testCartageErrand.Cancel();
            
            Assert.That(testCartageErrand.ExecutionStatus, Is.EqualTo(CartageErrandExecutionStatus.Cancelled));
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToSuccessAfterItGetsFinishedWhileHavingAtLeastOneCartageOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());

            testCartageErrand.Finish();

            Assert.That(testCartageErrand.ExecutionStatus, Is.EqualTo(CartageErrandExecutionStatus.Success));
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToFailureAfterItGetsFinishedWhileHavingNoAnyCartageOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();

            testCartageErrand.Finish();

            Assert.That(testCartageErrand.ExecutionStatus, Is.EqualTo(CartageErrandExecutionStatus.Failure));
        }
        
        [Test]
        public void CartageErrandHasToChooseTheCheapestOfferAfterItGetsFinished()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();
            var cheapestAcceptableOffer = testCartageErrand.CreateCheapestAcceptableOffer();

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(cheapestAcceptableOffer);

            var winningOffer = testCartageErrand.Finish()!;

            Assert.That(winningOffer, Is.EqualTo(testCartageErrand.GetCheapestOfferOrDefault()));
        }

        [Test]
        public void CartageErrandCannotAddCartageOfferThatOffersPriceHigherThanMaximumPrice()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();
            Assert.Throws<CartageErrandAddingNewCartageOfferNotAcceptedException>(
                    () => testCartageErrand.AddOffer(testCartageErrand.CreateUnacceptableOffer())
                );
        }

        [Test]
        public void CartageErrandHasToReturnCheapestOfReceivedCartageOffersWhenAskedForCheapestOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();
            var cheapestAcceptableOffer = testCartageErrand.CreateCheapestAcceptableOffer();

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(cheapestAcceptableOffer);

            var x = testCartageErrand.GetCheapestOfferOrDefault()!;

            Assert.That(x.Id, Is.EqualTo(cheapestAcceptableOffer.Id));
            Assert.That(x.Bidder.Id, Is.EqualTo(cheapestAcceptableOffer.Bidder.Id));
            Assert.That(x.Price, Is.EqualTo(cheapestAcceptableOffer.Price));
        }

        [Test]
        public void CartageErrandHasToChangeAllCartageOffersConsiderationStatusToRejectedWhenCancelled()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(testCartageErrand.CreateCheapestAcceptableOffer());

            testCartageErrand.Cancel();

            Assert.That(testCartageErrand.GetSubmittedCartageOffers().All(x => x.ConsiderationStatus == CartageOfferConsiderationStatus.Rejected), Is.True);
        }

        [Test]
        public void CartageErrandHasToChangeWinningCartageOfferConsiderationStatusToAcceptedAndRestToRejectedWhenFinishedWithAtLeastOneOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand();

            var cheapestAcceptableOffer = testCartageErrand.CreateCheapestAcceptableOffer();
            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(cheapestAcceptableOffer);

            cheapestAcceptableOffer = testCartageErrand.GetCheapestOfferOrDefault();

            testCartageErrand.Finish();

            int acceptedCount = 0;
            foreach (CartageOffer.CartageOffer cartageOffer in testCartageErrand.GetSubmittedCartageOffers())
            {
                if(cartageOffer.ConsiderationStatus == CartageOfferConsiderationStatus.Accepted)
                {
                    Assert.That(cartageOffer, Is.EqualTo(cheapestAcceptableOffer));
                    acceptedCount++;
                }
                else
                {
                    Assert.That(cartageOffer.ConsiderationStatus, Is.EqualTo(CartageOfferConsiderationStatus.Rejected));
                }
            }
            Assert.That(acceptedCount, Is.EqualTo(1));
        }
    }



    public static class Extensions
    {
        public static CartageOffer.CartageOffer CreateAcceptableOffer(this CartageErrand.CartageErrand cartageErrand)
        {
            return new CartageOffer.CartageOffer(new User.User("Użytkownik", "Adres@domena.pl"), (cartageErrand.MaximumPrice / 2));
        }

        public static CartageOffer.CartageOffer CreateUnacceptableOffer(this CartageErrand.CartageErrand cartageErrand)
        {
            return new CartageOffer.CartageOffer(new User.User("Użytkownik", "Adres@domena.pl"), (cartageErrand.MaximumPrice + 1));
        }

        public static CartageOffer.CartageOffer CreateCheapestAcceptableOffer(this CartageErrand.CartageErrand cartageErrand)
        {
            return new CartageOffer.CartageOffer(new User.User("Użytkownik", "Adres@domena.pl"), 0);
        }
    }
}
