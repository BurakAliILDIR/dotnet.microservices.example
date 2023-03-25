using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Discount.Service;
using Shared.ControllerBase;
using Shared.Service;
using System.Data;

namespace Services.Discount.Controllers
{
    [Authorize(Roles = "User")]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;


        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _discountService.GetAll();
            return ReturnActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var response = await _discountService.FindById(id);
            return ReturnActionResult(response);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> FindByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;

            var response = await _discountService.FindByCodeAndUserId(code, userId);
            return ReturnActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Model.Discount discount)
        {
            var response = await _discountService.Save(discount);
            return ReturnActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Model.Discount discount)
        {
            var response = await _discountService.Update(discount);
            return ReturnActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountService.Delete(id);
            return ReturnActionResult(response);
        }
    }
}