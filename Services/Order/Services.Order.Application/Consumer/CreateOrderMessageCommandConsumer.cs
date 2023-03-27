using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Services.Order.Domain.Aggregate;
using Services.Order.Infrastructure;
using Shared.Message;

namespace Services.Order.Application.Consumer
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.Aggregate.Address(context.Message.Address.Province,
                context.Message.Address.District, context.Message.Address.Street, context.Message.Address.ZipCode,
                context.Message.Address.Line);

            Domain.Aggregate.Order order = new Domain.Aggregate.Order(context.Message.UserId, newAddress);

            context.Message.OrderItems.ForEach(x =>
            {
                order.OrderItems.Add(new Domain.Aggregate.OrderItem()
                {
                    CourseId = x.CourseId,
                    CourseName = x.CourseName,
                    PictureUrl = x.PictureUrl,
                    Price = x.Price
                });
            });

            await _orderDbContext.AddAsync(order);
            
            await _orderDbContext.SaveChangesAsync();
        }
    }
}