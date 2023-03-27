using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Order.Infrastructure;
using Shared.Message;

namespace Services.Order.Application.Consumer
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly OrderDbContext _orderDbContext;

        public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderItems = await _orderDbContext.OrderItems.Where(x => x.CourseId == context.Message.CourseId)
                .ToListAsync();

            foreach (var item in orderItems)
            {
                item.CourseName = context.Message.CourseUpdateName;
            }

            await _orderDbContext.SaveChangesAsync();
        }
    }
}