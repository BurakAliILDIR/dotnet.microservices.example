using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBase;
using System.Data;

namespace Services.Payment.Controllers
{
    [Authorize(Roles = "User")]
    public class PaymentController : BaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return ReturnActionResult(Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Success,
                "Ödeme alındı.", null));
        }
    }
}