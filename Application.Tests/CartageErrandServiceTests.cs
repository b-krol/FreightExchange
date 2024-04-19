using Application.CartageErrands;
using Application.Users;
using Domain.CartageErrand;
using Domain.User;
using NSubstitute;
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
    }
}
