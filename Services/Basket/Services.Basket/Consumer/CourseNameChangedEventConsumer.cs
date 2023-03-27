using MassTransit;
using Services.Basket.Service;
using Shared.Message;

namespace Services.Basket.Consumer
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;

        public CourseNameChangedEventConsumer(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            _redisService.GetDatabase();
        }
    }
}