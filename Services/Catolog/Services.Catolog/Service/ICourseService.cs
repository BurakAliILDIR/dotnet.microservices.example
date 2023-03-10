using Services.Catolog.Dto.Course;
using Shared.Util;

namespace Services.Catolog.Service
{
    internal interface ICourseService
    {
        Task<Response> GetCourseAsync();

        Task<Response> FindByIdAsync(string id);

        Task<Response> GetAllByUserIdAsync(string userId);

        Task<Response> CreateCourseAsync(CourseCreateDto courseCreateDto);

        Task<Response> UpdateCourseAsync(CourseUpdateDto courseUpdateDto);

        Task<Response> DeleteCourseAsync(string id);
    }
}