using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Util;

namespace Services.Discount.Service
{
    public interface IDiscountService
    {
        Task<Response> GetAll();

        Task<Response> FindById(int id);

        Task<Response> Save(Model.Discount discount);

        Task<Response> Update (Model.Discount discount);

        Task<Response> Delete(int id);

        Task<Response> Use(string code, string userId);

    }

}
