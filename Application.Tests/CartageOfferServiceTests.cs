using Application.CartageErrands;
using Application.CartageOffers;
using Application.Users;
using Domain.CartageErrand;
using Domain.CartageOffer;
using Domain.User;
using NSubstitute;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    [TestFixture]
    public class CartageOfferServiceTests
    {
        private Randomizer Rand = Randomizer.CreateRandomizer();

        private static CartageOfferDto CreateCartageOfferDto()
        {
            return new CartageOfferDto() { Price = 1, BidderId = 1};
        }

        private static User CreateCorrectUser(int id)
        {
            return new User("Bidder", "mrBidd3r@domain.com") { Id = id };
        }

        private static CartageErrand CreateCorrectCartageErrand(int id)
        {
            return new CartageErrand(
                CreateCorrectUser(id),
                "planks",
                "Radom ul. Słowackiego 3",
                "Gdańsk",
                500,
                13.4f,
                3000,
                DateTime.Now + new TimeSpan(24, 0, 0)
                ) { Id = id};
        }

        [Test]
        public void CartageOfferCannotBeAddedToNotExistingCartageErrand()
        {
            var cartageOfferDto = CreateCartageOfferDto();
            int cartageErrandId = Rand.Next(2, 1001);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetUserById(Arg.Is<int>(cartageOfferDto.BidderId)).Returns(CreateCorrectUser(cartageOfferDto.BidderId));
            dataSource.GetCartageErrandById(Arg.Is<int>(cartageErrandId)).Returns(Task.FromException<CartageErrand>(new CartageErrandNotFoundException()));
            var service = new CartageOfferService(dataSource);

            Assert.ThrowsAsync<CartageErrandNotFoundException>(async Task () => await service.Add(cartageErrandId, cartageOfferDto));
            dataSource.Received(1).GetCartageErrandById(Arg.Is(cartageErrandId));
            dataSource.Received(0).SaveChangesAsync();
        }

        [Test]
        public void CartageOfferCannotBeAddedWithBidderIdOfNotExistingUser()
        {
            var cartageOfferDto = CreateCartageOfferDto();
            int cartageErrandId = Rand.Next(2, 1001);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetUserById(Arg.Is<int>(cartageOfferDto.BidderId)).Returns(Task.FromException<User>(new UserNotFoundException()));
            dataSource.GetCartageErrandById(Arg.Is<int>(cartageErrandId)).Returns(CreateCorrectCartageErrand(cartageErrandId));
            var service = new CartageOfferService(dataSource);

            Assert.ThrowsAsync<UserNotFoundException>(async () => await service.Add(cartageErrandId, cartageOfferDto));
            dataSource.Received(1).GetUserById(Arg.Is(cartageOfferDto.BidderId));
            dataSource.Received(0).SaveChangesAsync();
        }

        [Test]
        public void CartageOfferHasToBeAddedToSpecifiedCartageErrand()
        {
            var cartageOfferDto = CreateCartageOfferDto();
            int cartageErrandId = Rand.Next(1, 1000);
            int cartageOfferId = Rand.Next(1000, 2000);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetUserById(Arg.Is<int>(cartageOfferDto.BidderId)).Returns(CreateCorrectUser(cartageOfferDto.BidderId));
            dataSource.GetCartageErrandById(Arg.Is<int>(cartageErrandId)).Returns(CreateCorrectCartageErrand(cartageErrandId));

            dataSource.When(x => x.AddCartageOffer(Arg.Any<CartageOffer>()))
                .Do(y => y.Arg<CartageOffer>().Id = cartageOfferId);

            var service = new CartageOfferService(dataSource);
            var newOfferId = service.Add(cartageErrandId, cartageOfferDto).Result;

            Assert.That(newOfferId, Is.EqualTo(cartageOfferId));
            dataSource.Received(1).GetUserById(Arg.Is(cartageOfferDto.BidderId));
            dataSource.Received(1).GetCartageErrandById(Arg.Is(cartageErrandId));
            dataSource.Received(1).SaveChangesAsync();
        }

        [Test]
        public void CartageOfferServiceHasToReturnIdOfCreatedCartageOffer()
        {
            var cartageOfferDto = CreateCartageOfferDto();
            int cartageErrandId = Rand.Next(2, 2000);
            int cartageOfferId = Rand.Next(2, 2000);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetCartageErrandById(Arg.Is<int>(cartageErrandId)).Returns(CreateCorrectCartageErrand(cartageErrandId));
            dataSource.When(x => x.AddCartageOffer(Arg.Any<CartageOffer>()))
                .Do(y => y.Arg<CartageOffer>().Id = cartageOfferId);
            var service = new CartageOfferService(dataSource);

            Assert.That(service.Add(cartageErrandId, cartageOfferDto).Result, Is.EqualTo(cartageOfferId));
            dataSource.Received(1).GetCartageErrandById(Arg.Is(cartageErrandId));
        }
    }
}
