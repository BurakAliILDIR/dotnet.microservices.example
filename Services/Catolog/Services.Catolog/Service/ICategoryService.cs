using Services.Catolog.Model;
using Shared.Util;

namespace Services.Catolog.Service
{
    public interface ICategoryService
    {
        public Task<Response> GetCategoryAsync(string categoryName);

        public Task<Response> FindByIdAsync(string id);

        public Task<Response> CreateCategoryAsync(Category category);
    }
}
