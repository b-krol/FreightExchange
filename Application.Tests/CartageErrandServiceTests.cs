using Application.CartageErrands;
using Application.MapProfile;
using Application.Users;
using AutoMapper;
using Domain.CartageErrand;
using Domain.User;
using NSubstitute;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    [TestFixture]
    public class CartageErrandServiceTests
    {
        private Randomizer Rand = Randomizer.CreateRandomizer();
        private IMapper Mapper;
        #region staticMethods
        private static User CreateCorrectUser(int id)
        {
            return new User("Bidder", "mrBidd3r@domain.com") { Id = id };
        }
        private static CartageOffer CreateCorrectCartageOffer(int id)
        {
            return new CartageOffer(CreateCorrectUser(id), 1) { Id = id };
        }

        private static CartageErrand CreateCorrectCartageErrand(int id)
        {
            return CreateCorrectCartageErrand(id, new TimeSpan(0, 30, 0));
        }
        private static CartageErrand CreateCorrectCartageErrand(int id, TimeSpan timeToEndTime)
        {
            return new CartageErrand(
                CreateCorrectUser(id),
                "planks",
                "Radom ul. Słowackiego 3",
                "Gdańsk",
                500,
                13.4f,
                3000,
                DateTime.UtcNow + timeToEndTime
                )
            { Id = id };
        }
        private static CartageErrandDto CreateCartageErrandDto(int id)
        {
            return new CartageErrandDto()
            {
                FounderId = id,
                GoodsName = "planks",
                StartingAdress = "Radom",
                DestinationAdress = "Gdańsk",
                Distance = 1000,
                Weight = 13.4f,
                MaximumPrice = 5000,
                EndDate = DateTime.UtcNow + new TimeSpan(24, 0, 0)
            };
        }
        #endregion
        [SetUp]
        public void Setup()
        {
            var myProfile = new EntityToDtoMap();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            Mapper = new Mapper(configuration);
        }

        [Test]
        public void CartageErrandCannotBeAddedWithFounderIdOfNotExistingUser()
        {
            var cartageErrandDto = CreateCartageErrandDto(Rand.Next(2, 2000));

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetUserById(Arg.Is<int>(cartageErrandDto.FounderId)).Returns(Task.FromException<User>(new UserNotFoundException()));
            var service = new CartageErrandService(dataSource, Substitute.For<IMapper>());

            Assert.ThrowsAsync<UserNotFoundException>(async () => await service.Add(cartageErrandDto));
            dataSource.Received(1).GetUserById(cartageErrandDto.FounderId);
        }

        [Test]
        public void CartageErrandServiceHasToReturnIdOfCreatedCartageErrand()
        {
            var cartageErrandDto = CreateCartageErrandDto(Rand.Next(2, 2000));
            int cartageErrandId = Rand.Next(2, 2000);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.When(x => x.AddCartageErrand(Arg.Any<CartageErrand>()))
                .Do(y => y.Arg<CartageErrand>().Id = cartageErrandId);
            var service = new CartageErrandService(dataSource, Substitute.For<IMapper>());

            Assert.That(service.Add(cartageErrandDto).Result, Is.EqualTo(cartageErrandId));
        }

        [Test]
        public void ReturnedCartageErrandRepresentingObjectHasToContainSetId()
        {
            int cartageErrandId = Rand.Next(2, 2000);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetCartageErrandById(Arg.Any<int>()).Returns(Task.FromResult(CreateCorrectCartageErrand(cartageErrandId)));
            var service = new CartageErrandService(dataSource, Mapper);

            Assert.That(service.GetById(cartageErrandId).Result.Id, !Is.Null);
            dataSource.Received(1).GetCartageErrandById(cartageErrandId);
        }

        [Test]
        public void ReturnedCartageErrandRepresentingObjectHasToContainMatchingSetId()
        {
            int cartageErrandId = Rand.Next(2, 2000);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetCartageErrandById(Arg.Any<int>()).Returns(Task.FromResult(CreateCorrectCartageErrand(cartageErrandId)));
            var service = new CartageErrandService(dataSource, Mapper);

            Assert.That(service.GetById(cartageErrandId).Result.Id, Is.EqualTo(cartageErrandId));
            dataSource.Received(1).GetCartageErrandById(cartageErrandId);
        }
    }
}
