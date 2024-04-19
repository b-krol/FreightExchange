using Application.CartageErrands;
using Application.Users;
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

        #region staticMethods
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
                EndDate = DateTime.Now + new TimeSpan(24, 0, 0)
            };
        }
        #endregion

        [Test]
        public void CartageErrandCannotBeAddedWithFounderIdOfNotExistingUser()
        {
            var cartageErrandDto = CreateCartageErrandDto(1);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetUserById(Arg.Is<int>(cartageErrandDto.FounderId)).Returns(Task.FromException<User>(new UserNotFoundException()));
            var service = new CartageErrandService(dataSource);

            Assert.ThrowsAsync<UserNotFoundException>(async () => await service.Add(cartageErrandDto));
            dataSource.Received(1).GetUserById(cartageErrandDto.FounderId);
        }

        [Test]
        public void CartageErrandServiceHasToReturnIdOfCreatedCartageErrand()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ReturnedCartageErrandRepresentingObjectHasToContainSetId()
        {
            int cartageErrandId = Rand.Next(2, 2000);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetCartageErrandById(Arg.Any<int>()).Returns(Task.FromResult(CreateCorrectCartageErrand(cartageErrandId)));
            var service = new CartageErrandService(dataSource);

            Assert.That(dataSource.GetCartageErrandById(cartageErrandId).Result.Id, !Is.Null);
        }

        [Test]
        public void ReturnedCartageErrandRepresentingObjectHasToContainMatchingSetId()
        {
            int cartageErrandId = Rand.Next(2, 2000);

            var dataSource = Substitute.For<IDataSource>();
            dataSource.GetCartageErrandById(Arg.Any<int>()).Returns(Task.FromResult(CreateCorrectCartageErrand(cartageErrandId)));
            var service = new CartageErrandService(dataSource);

            Assert.That(dataSource.GetCartageErrandById(cartageErrandId).Result.Id, Is.EqualTo(cartageErrandId));
        }
    }
}
