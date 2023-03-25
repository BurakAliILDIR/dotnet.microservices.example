using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Basket.Dto;
using Services.Basket.Service;
using Shared.ControllerBase;
using Shared.Service;

namespace Services.Basket.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetBasket()
        {
            var userId = _sharedIdentityService.GetUserId;

            return ReturnActionResult(await _basketService.Get(userId));
        }


        [HttpPost]
        public async Task<IActionResult> SetBasket(BasketDto basketDto)
        {
            var userId = _sharedIdentityService.GetUserId;

            basketDto.UserId = userId;

            var response = await _basketService.SaveOrUpdate(basketDto);

            return ReturnActionResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = _sharedIdentityService.GetUserId;

            return ReturnActionResult(await _basketService.Delete(userId));
        }
    }
}