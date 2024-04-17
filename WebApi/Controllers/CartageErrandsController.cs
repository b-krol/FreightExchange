using Application.CartageErrands;
using Application.Users;
using Domain.CartageErrand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartageErrandsController : ControllerBase
    {

        private ICartageErrandService CartageErrandService { get; }

        public CartageErrandsController(ICartageErrandService cartageErrandService)
        {
            CartageErrandService = cartageErrandService;
        }
        [HttpGet]
        public IEnumerable<CartageErrandDto> GetCartageErrands()
        {
            return CartageErrandService.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetCartageErrandById(int id)
        {
            try
            {
                return Ok(CartageErrandService.GetById(id));
            }
            catch (CartageErrandNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("Active")]
        public IEnumerable<CartageErrandDto> GetActiveCartageErrands()
        {
            return GetCartageErrands().Where(
                    (cartageErrand) => cartageErrand.IsActive ?? false
                );
        }

        [HttpGet("Finished")]
        public IEnumerable<CartageErrandDto> GetFinishedCartageErrands()
        {
            return GetCartageErrands().Where(
                    (cartageErrand) => !cartageErrand.IsActive ?? false
                );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCartageErrandById(int id)
        {
            try
            {
                CartageErrandService.Delete(id);
                return Ok();
            }
            catch (CartageErrandNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (CartageErrandNotDeletedException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCartageErrand(CartageErrandDto cartageErrandDto)
        {
            var newCartageErrandId = CartageErrandService.Add(cartageErrandDto);
            return Created($"{Request.GetEncodedUrl()}/{newCartageErrandId}", CartageErrandService.GetById(newCartageErrandId));
        }

    }
}
