using Services.Basket.Dto;
using Shared.Util;

namespace Services.Basket.Service
{
    public interface IBasketService
    {

        Task<Response> Get(string userId);

        Task<Response> SaveOrUpdate(BasketDto basketDto);

        Task<Response> Delete(string userId);
    }
}
