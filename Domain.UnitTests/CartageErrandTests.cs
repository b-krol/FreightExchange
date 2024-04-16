﻿using Domain.CartageErrand;
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
        private static CartageErrand.CartageErrand CreateAcceptableCartageErrand(CartageErrandExecutionStatus executionStatus)
        {
            return new CartageErrand.CartageErrand(
                new User.User("Mr. Founder", "mrF0under@domain.com"),
                "wooden planks",
                "Radom ul. Zagajnikowa 3s",
                "Poznań al. Meblowa 28/3",
                400,
                15.3f,
                4000,
                DateTime.Now + new TimeSpan(12, 0, 0),
                executionStatus
                );
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void CartageErrandCannotBeCreatedWithEmptyGoodsName(string? goodsName)
        {
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(
                         new User.User("Mr. Founder", "mrF0under@domain.com"),
                         goodsName!,
                         "Radom ul. Zagajnikowa 3s",
                         "Poznań al. Meblowa 28/3",
                         400,
                         15.3f,
                         4000,
                         DateTime.Now + new TimeSpan(12, 0, 0),
                         CartageErrandExecutionStatus.Active
                     )
                );
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void CartageErrandCannotBeCreatedWithEmptyStartingAdress(string? startingAdress)
        {
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(
                         new User.User("Mr. Founder", "mrF0under@domain.com"),
                         "deski",
                         startingAdress!,
                         "Poznań al. Meblowa 28/3",
                         400,
                         15.3f,
                         4000,
                         DateTime.Now + new TimeSpan(12, 0, 0),
                         CartageErrandExecutionStatus.Active
                     )
                );
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("      ")]
        public void CartageErrandCannotBeCreatedWithEmptyDestinationAdress(string? destinationAdress)
        {
            Assert.Throws<ArgumentException>(
                    () => new CartageErrand.CartageErrand(
                         new User.User("Mr. Founder", "mrF0under@domain.com"),
                         "deski",
                         "Radom ul. Zagajnikowa 3s",
                         destinationAdress!,
                         400,
                         15.3f,
                         4000,
                         DateTime.Now + new TimeSpan(12, 0, 0),
                         CartageErrandExecutionStatus.Active
                     )
                );
        }

        [Test]
        [TestCase(CartageErrandExecutionStatus.Cancelled)]
        [TestCase(CartageErrandExecutionStatus.Failure)]
        [TestCase(CartageErrandExecutionStatus.Success)]
        public void CartageErrandCannotGetCancelledWhenItIsNotActive(CartageErrandExecutionStatus executionStatus)
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(executionStatus);
            Assert.Throws<CartageErrandExecutionStatusChangeNotAllowedException>(() => testCartageErrand.Cancel());
        }

        [Test]
        [TestCase(CartageErrandExecutionStatus.Cancelled)]
        [TestCase(CartageErrandExecutionStatus.Failure)]
        [TestCase(CartageErrandExecutionStatus.Success)]
        public void CartageErrandCannotGetFinishedWhenItIsNotActive(CartageErrandExecutionStatus executionStatus)
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(executionStatus);
            Assert.Throws<CartageErrandExecutionStatusChangeNotAllowedException>(() => testCartageErrand.Finish());
        }

        [Test]
        [TestCase(CartageErrandExecutionStatus.Cancelled)]
        [TestCase(CartageErrandExecutionStatus.Failure)]
        [TestCase(CartageErrandExecutionStatus.Success)]
        public void CartageErrandCannotReceiveNewCartageOffersWhenItIsNotActive(CartageErrandExecutionStatus executionStatus)
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(executionStatus);
            Assert.Throws<CartageErrandAddingNewCartageOfferNotAcceptedException>(() => testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer()));//TODO why cant use extension method?
        }

        [Test]
        public void CartageErrandCanReceiveNewCartageOffersFittingMaximumPriceWhenItIsInActiveState()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());

            Assert.That(testCartageErrand.GetSubmittedCartageOffers().Count == 2);
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToCancelledAfterItGetsCancelled()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.Cancel();
            
            Assert.That(testCartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Cancelled);
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToSuccessAfterItGetsFinishedWhileHavingAtLeastOneCartageOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());

            testCartageErrand.Finish();

            Assert.That(testCartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Success);
        }

        [Test]
        public void CartageErrandHasToChangeItsStatusToFailureAfterItGetsFinishedWhileHavingNoAnyCartageOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.Finish();

            Assert.That(testCartageErrand.ExecutionStatus == CartageErrandExecutionStatus.Failure);
        }
        
        [Test]
        public void CartageErrandHasToChooseTheCheapestOfferAfterItGetsFinished()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);
            var cheapestAcceptableOffer = testCartageErrand.CreateCheapestAcceptableOffer();

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(cheapestAcceptableOffer);

            var winningOffer = testCartageErrand.Finish();

            cheapestAcceptableOffer = testCartageErrand.GetSubmittedCartageOffers().Min(
                    Comparer<CartageOffer.CartageOffer>.Create(
                            (x, y) =>
                                x.Price > y.Price ? 1 :
                                x.Price < y.Price ? -1 :
                                0
                        )
                );

            Assert.That(winningOffer.Equals(cheapestAcceptableOffer));
        }

        [Test]
        public void CartageErrandCannotAddCartageOfferThatOffersPriceHigherThanMaximumPrice()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);
            Assert.Throws<CartageErrandAddingNewCartageOfferNotAcceptedException>(
                    () => testCartageErrand.AddOffer(testCartageErrand.CreateUnacceptableOffer())
                );
        }

        [Test]
        public void CartageErrandHasToReturnCheapestOfReceivedCartageOffersWhenAskedForWinningOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);
            var cheapestAcceptableOffer = testCartageErrand.CreateCheapestAcceptableOffer();

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(cheapestAcceptableOffer);

            Assert.That(testCartageErrand.GetWinningOfferOrDefault().Equals(cheapestAcceptableOffer));
        }

        [Test]
        public void CartageErrandHasToChangeAllCartageOffersConsiderationStatusToRejectedWhenCancelled()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);

            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(testCartageErrand.CreateCheapestAcceptableOffer());

            testCartageErrand.Cancel();

            foreach(CartageOffer.CartageOffer cartageOffer in testCartageErrand.GetSubmittedCartageOffers())
            {
                Assert.That(cartageOffer.ConsiderationStatus == CartageOfferConsiderationStatus.Rejected);
            }
        }

        [Test]
        public void CartageErrandHasToChangeWinningCartageOfferConsiderationStatusToAcceptedAndRestToRejectedWhenFinishedWithAtLeastOneOffer()
        {
            CartageErrand.CartageErrand testCartageErrand = CreateAcceptableCartageErrand(CartageErrandExecutionStatus.Active);

            var cheapestAcceptableOffer = testCartageErrand.CreateCheapestAcceptableOffer();
            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(testCartageErrand.CreateAcceptableOffer());
            testCartageErrand.AddOffer(cheapestAcceptableOffer);

            testCartageErrand.Finish();

            cheapestAcceptableOffer = testCartageErrand.GetSubmittedCartageOffers().Min(
                    Comparer<CartageOffer.CartageOffer>.Create(
                            (x, y) =>
                                x.Price > y.Price ? 1 :
                                x.Price < y.Price ? -1 :
                                0
                        )
                );

            int acceptedCount = 0;
            foreach (CartageOffer.CartageOffer cartageOffer in testCartageErrand.GetSubmittedCartageOffers())
            {
                if(cartageOffer.ConsiderationStatus == CartageOfferConsiderationStatus.Accepted)
                {
                    Assert.That(cheapestAcceptableOffer.Equals(cartageOffer));
                    acceptedCount++;
                }
                else
                {
                    Assert.That(cartageOffer.ConsiderationStatus == CartageOfferConsiderationStatus.Rejected);
                }
            }
            Assert.That(acceptedCount == 1);
        }
    }



    public static class Extensions
    {
        public static CartageOffer.CartageOffer CreateAcceptableOffer(this CartageErrand.CartageErrand cartageErrand)
        {
            return new CartageOffer.CartageOffer(new User.User("Użytkownik", "Adres@domena.pl"), (cartageErrand.MaximumPrice / 2), CartageOfferConsiderationStatus.Waiting);
        }

        public static CartageOffer.CartageOffer CreateUnacceptableOffer(this CartageErrand.CartageErrand cartageErrand)
        {
            return new CartageOffer.CartageOffer(new User.User("Użytkownik", "Adres@domena.pl"), (cartageErrand.MaximumPrice + 1), CartageOfferConsiderationStatus.Waiting);
        }

        public static CartageOffer.CartageOffer CreateCheapestAcceptableOffer(this CartageErrand.CartageErrand cartageErrand)
        {
            return new CartageOffer.CartageOffer(new User.User("Użytkownik", "Adres@domena.pl"), 0, CartageOfferConsiderationStatus.Waiting);
        }
    }
}
