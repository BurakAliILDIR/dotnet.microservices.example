using Services.Catolog.Dto;
using Services.Catolog.Model;
using Shared.Util;

namespace Services.Catolog.Service
{
    public interface ICategoryService
    {
        public Task<Response> GetCategoryAsync();

        public Task<Response> FindByIdAsync(string id);

        public Task<Response> CreateCategoryAsync(CategoryDto categoryDto);
    }
}
