using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.Order.Application.Dto;
using Shared.Util;

namespace Services.Order.Application.Command
{
    public class CreateOrderCommand : IRequest<Response>
    {
        public string UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}