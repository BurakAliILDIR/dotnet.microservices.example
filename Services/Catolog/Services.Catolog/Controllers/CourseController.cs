using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catolog.Dto.Course;
using Services.Catolog.Service;
using Shared.ControllerBase;
using Shared.Util;

namespace Services.Catolog.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;


        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetCourseAsync();

            return ReturnActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.FindByIdAsync(id);

            return ReturnActionResult(response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var response = await _courseService.GetAllByUserIdAsync(userId);

            return ReturnActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateCourseAsync(courseCreateDto);

            return ReturnActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateCourseAsync(courseUpdateDto);

            return ReturnActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteCourseAsync(id);

            return ReturnActionResult(response);
        }
    }
}