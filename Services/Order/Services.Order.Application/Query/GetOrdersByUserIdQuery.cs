using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.Order.Application.Dto;
using Shared.Util;

namespace Services.Order.Application.Query
{
    public class GetOrdersByUserIdQuery:IRequest<Response>
    {
        public string UserId { get; set; }
    }
}
