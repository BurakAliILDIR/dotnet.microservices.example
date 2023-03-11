using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catolog.Dto;
using Services.Catolog.Service;
using Shared.ControllerBase;
using Shared.Util;

namespace Services.Catolog.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetCategoryAsync();

            return ReturnActionResult(categories);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            var categories = await _categoryService.FindByIdAsync(id);

            return ReturnActionResult(categories);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var categories = await _categoryService.CreateCategoryAsync(categoryDto);

            return ReturnActionResult(categories);
        }
    }
}