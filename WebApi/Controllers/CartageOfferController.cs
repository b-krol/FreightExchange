using Application.CartageOffers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartageOfferController : ControllerBase
    {
        private ICartageOfferService CartageOfferService { get;}
        public CartageOfferController(ICartageOfferService cartageOfferService)
        {
            CartageOfferService = cartageOfferService;
        }

        [HttpGet]
        public async Task<IEnumerable<CartageOfferDto>> GetCartageOffersByCartageErrandId(int cartageOffersId)
        {
            return await CartageOfferService.GetAll();
        }
    }
}
