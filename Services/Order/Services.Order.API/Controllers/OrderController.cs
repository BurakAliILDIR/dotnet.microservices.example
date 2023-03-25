using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Order.Application.Command;
using Services.Order.Application.Query;
using Shared.ControllerBase;
using Shared.Service;

namespace Services.Order.API.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _identityService;

        public OrderController(IMediator mediator, ISharedIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = _identityService.GetUserId;

            var response = await _mediator.Send(new GetOrdersByUserIdQuery()
            {
                UserId = userId
            });

            return ReturnActionResult(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            var userId = _identityService.GetUserId;

            createOrderCommand.UserId = userId;

            var response = await _mediator.Send(createOrderCommand);

            return ReturnActionResult(response);
        }
    }
}