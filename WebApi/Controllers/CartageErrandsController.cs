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
        public async Task<IEnumerable<CartageErrandDto>> GetCartageErrands()
        {
            return await CartageErrandService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartageErrandById(int id)
        {
            try
            {
                return Ok(await CartageErrandService.GetById(id));
            }
            catch (CartageErrandNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("Active")]
        public async Task<IEnumerable<CartageErrandDto>> GetActiveCartageErrands()
        {
            var allCartageErrands = await GetCartageErrands();
            return allCartageErrands.Where(
                    (cartageErrand) => cartageErrand.IsActive ?? false
                );
        }

        [HttpGet("Finished")]
        public async Task<IEnumerable<CartageErrandDto>> GetFinishedCartageErrands()
        {
            var allCartageErrands = await GetCartageErrands();
            return allCartageErrands.Where(
                    (cartageErrand) => !cartageErrand.IsActive ?? false
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartageErrandById(int id)
        {
            try
            {
                await CartageErrandService.Delete(id);
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
        public async Task<IActionResult> AddCartageErrand(CartageErrandDto cartageErrandDto)
        {
            var newCartageErrandId = await CartageErrandService.Add(cartageErrandDto);
            return Created($"{Request.GetEncodedUrl()}/{newCartageErrandId}", await CartageErrandService.GetById(newCartageErrandId));
        }

    }
}
