using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBase;
using System.Data;
using MassTransit;
using Services.Payment.Dto;
using Shared.Message;
using Shared.Service;

namespace Services.Payment.Controllers
{
    [Authorize(Roles = "User")]
    public class PaymentController : BaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly ISharedIdentityService _sharedIdentityService;


        public PaymentController(ISendEndpointProvider sendEndpointProvider,
            ISharedIdentityService sharedIdentityService)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            // TODO : RabbitMQ'ya mesaj göndermek için:
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand();

            createOrderMessageCommand.UserId = _sharedIdentityService.GetUserId;
            createOrderMessageCommand.Address.Province = paymentDto.Order.Address.Province;
            createOrderMessageCommand.Address.District = paymentDto.Order.Address.District;
            createOrderMessageCommand.Address.Street = paymentDto.Order.Address.Street;
            createOrderMessageCommand.Address.Line = paymentDto.Order.Address.Line;
            createOrderMessageCommand.Address.ZipCode = paymentDto.Order.Address.ZipCode;

            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem()
                {
                    CourseId = x.CourseId,
                    CourseName = x.CourseName,
                    PictureUrl = x.PictureUrl,
                    Price = x.Price
                });
            });

            await sendEndpoint.Send(createOrderMessageCommand);

            return ReturnActionResult(Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Success,
                "Ödeme alındı.", null));
        }
    }
}