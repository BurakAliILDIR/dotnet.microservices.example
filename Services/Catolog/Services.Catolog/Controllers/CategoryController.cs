using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catolog.Dto;
using Services.Catolog.Service;
using Shared.ControllerBase;
using Shared.Util;

namespace Services.Catolog.Controllers
{
    public class CategoryController : CustomBaseController
    {
        private  readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<Response> GetAll()
        {
            var categories = await _categoryService.GetCategoryAsync();
            
            return Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Success, "Kurslar başarıyla getirildi.", categories);
        }


        [HttpGet("{id}")]
        public async Task<Response> FindById(string id)
        {
            var categories = await _categoryService.FindByIdAsync(id);
            
            return Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Success, "Kurs başarıyla getirildi.", categories);
        }


        
        [HttpPost]
        public async Task<Response> Create(CategoryDto categoryDto)
        {
            var categories = await _categoryService.CreateCategoryAsync(categoryDto);
            
            return Shared.Util.Response.Return(Shared.Util.Response.ResponseStatusEnum.Success, "Kurs başarıyla getirildi.", categories);
        }


        

    }
}
