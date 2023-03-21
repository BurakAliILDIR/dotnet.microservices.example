using System.Text.Json;
using Services.Basket.Dto;
using Shared.Util;

namespace Services.Basket.Service
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;


        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response> Get(string userId)
        {
            var existBasket = await _redisService.GetDatabase().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return Response.Return(Response.ResponseStatusEnum.Error, "Sepet yok.", null);
            }

            var basket = JsonSerializer.Deserialize<BasketDto>(existBasket);

            return Response.Return(Response.ResponseStatusEnum.Success, "Sepet var.", basket);
        }

        public async Task<Response> SaveOrUpdate(BasketDto basketDto)
        {
            var basketJson = JsonSerializer.Serialize(basketDto);

            var status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId, basketJson);

            if (status)
                return Response.Return(Response.ResponseStatusEnum.Success, "Sepet kayıt edildi.", basketDto);


            return Response.Return(Response.ResponseStatusEnum.Error, "Sepet kayıt edilemedi.", null, 500);
        }

        public async Task<Response> Delete(string userId)
        {
            var status = await _redisService.GetDatabase().KeyDeleteAsync(userId);

            return Response.Return(Response.ResponseStatusEnum.Success, "Sepet silindi.", null);
        }
    }
}