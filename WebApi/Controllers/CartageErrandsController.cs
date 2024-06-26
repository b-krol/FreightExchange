﻿using Application.CartageErrands;
using Application.CartageOffers;
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
    public class CartageErrandsController : AuthorizeController
    {

        private ICartageErrandService CartageErrandService { get; }
        private ICartageOfferService CartageOfferService { get; }

        public CartageErrandsController(ICartageErrandService cartageErrandService, ICartageOfferService cartageOfferService)
        {
            CartageErrandService = cartageErrandService;
            CartageOfferService = cartageOfferService;
        }

        #region CartageErrands
        [HttpGet]
        public async Task<IEnumerable<CartageErrandDto>> GetCartageErrands()
        {
            return await CartageErrandService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartageErrandWithOffersDto>> GetCartageErrandById(int id)
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
            var allCartageErrands = await CartageErrandService.GetAll();
            return allCartageErrands.Where(
                    (cartageErrand) => cartageErrand.IsActive
                );
        }

        [HttpPost]
        public async Task<IActionResult> AddCartageErrand(CartageErrandDto cartageErrandDto)
        {
            var newCartageErrandId = await CartageErrandService.Add(cartageErrandDto);
            return Created($"{Request.GetEncodedUrl()}/{newCartageErrandId}", await CartageErrandService.GetById(newCartageErrandId));
        }
        #endregion

        #region CartageOffers
        [HttpGet("{id}/Offers")]
        public async Task<IEnumerable<CartageOfferDto>> GetCartageOffersForErrand(int id)
        {
            return await CartageOfferService.GetAllByCartageErrand(id);
        }

        [HttpPost("{id}/Offers")]
        public async Task<IActionResult> AddCartageOffer(int id, CartageOfferDto cartageOfferDto)
        {
            var newCartageOfferId = await CartageOfferService.Add(id, cartageOfferDto);
            return Created($"{Request.GetEncodedUrl()}/{newCartageOfferId}", await CartageOfferService.GetById(newCartageOfferId));
        }
        #endregion
    }
}
