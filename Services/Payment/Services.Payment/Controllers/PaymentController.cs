using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBase;

namespace Services.Payment.Controllers
{
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