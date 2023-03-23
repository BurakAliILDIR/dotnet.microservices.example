using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.Order.Application.Command;
using Services.Order.Application.Dto;
using Services.Order.Application.Mapping;
using Services.Order.Domain.Aggregate;
using Services.Order.Infrastructure;
using Shared.Util;

namespace Services.Order.Application.Handler
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.AddressDto.Province, request.AddressDto.District,
                request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);

            Domain.Aggregate.Order newOrder = new Domain.Aggregate.Order(request.UserId, newAddress);

            var orderItems = ObjectMapper.Mapper.Map<List<OrderItem>>(request.OrderItemDtos);

            orderItems.ForEach(x => { newOrder.OrderItems.Append(x); });

            await _orderDbContext.AddAsync(newOrder);

            await _orderDbContext.SaveChangesAsync();

            return Response.Return(Response.ResponseStatusEnum.Success, "Sipariş edildi.", new CreatedOrderDto()
            {
                OrderId = newOrder.Id
            });
        }
    }
}