using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Order.Application.Dto;
using Services.Order.Application.Mapping;
using Services.Order.Application.Query;
using Services.Order.Infrastructure;
using Shared.Util;

namespace Services.Order.Application.Handler
{
    public class GetOrdersByUserIdHandler : IRequestHandler<GetOrdersByUserIdQuery, Response>
    {
        private readonly OrderDbContext _orderDbContext;


        public GetOrdersByUserIdHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).Where(x => x.UserId == request.UserId)
                .ToListAsync();

            if (!orders.Any())
            {
                return Response.Return(Response.ResponseStatusEnum.Success, "Siparişler listelendi.", null);
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response.Return(Response.ResponseStatusEnum.Success, "Siparişler listelendi.", ordersDto);
        }
    }
}