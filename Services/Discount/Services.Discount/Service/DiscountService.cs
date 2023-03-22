using Shared.Util;

namespace Services.Discount.Service
{
    public class DiscountService:IDiscountService
    {
        public Task<Response> GetAll()
        {


            throw new NotImplementedException();
        }

        public Task<Response> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Save(Model.Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Model.Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Use(string code, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
