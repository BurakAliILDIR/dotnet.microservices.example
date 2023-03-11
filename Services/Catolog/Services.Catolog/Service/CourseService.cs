using AutoMapper;
using MongoDB.Driver;
using Services.Catolog.Dto.Course;
using Services.Catolog.Model;
using Services.Catolog.Setting;
using Shared.Util;

namespace Services.Catolog.Service
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSetting databaseSetting)
        {
            _mapper = mapper;

            MongoClient client = new MongoClient(databaseSetting.ConnectionString);

            IMongoDatabase database = client.GetDatabase(databaseSetting.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSetting.CourseCollectionName);

            _categoryCollection = database.GetCollection<Category>(databaseSetting.CategoryCollectionName);
        }

        public async Task<Response> GetCourseAsync()
        {
            IEnumerable<Course> courses = await _courseCollection.Find<Course>(course => true).ToListAsync();

            if (!courses.Any())
            {
                return Response.Return(Response.ResponseStatusEnum.Warning, "Kurs bulunamadı.", null);
            }

            foreach (Course course in courses)
            {
                course.Category = await _categoryCollection
                    .Find<Category>(category => category.Id == course.CategoryId).FirstAsync();
            }

            IEnumerable<CourseDto> coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return Response.Return(Response.ResponseStatusEnum.Success, "", coursesDto);
        }


        public async Task<Response> FindByIdAsync(string id)
        {
            Course course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (course is null)
            {
                return Response.Return(Response.ResponseStatusEnum.Error, "Kurs bulunamadı.", null);
            }

            course.Category = await _categoryCollection.Find(category => category.Id == course.CategoryId)
                .FirstOrDefaultAsync();

            CourseDto courseDto = _mapper.Map<CourseDto>(course);

            return Response.Return(Response.ResponseStatusEnum.Success, "Kurs başarıyla getirildi.", courseDto);
        }


        public async Task<Response> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(course => course.UserId == userId).ToListAsync();

            if (!courses.Any())
            {
                return Response.Return(Response.ResponseStatusEnum.Warning, "Kurs bulunamadı.", null);
            }

            foreach (var course in courses)
            {
                course.Category = await _categoryCollection
                    .Find<Category>(category => category.Id == course.CategoryId).FirstAsync();
            }

            var coursesDto = _mapper.Map<CourseDto>(courses);

            return Response.Return(Response.ResponseStatusEnum.Success, "", coursesDto);
        }


        public async Task<Response> CreateCourseAsync(CourseCreateDto courseCreateDto)
        {
            Course newCourse = _mapper.Map<Course>(courseCreateDto);

            newCourse.CreatedAt = DateTime.Now;

            await _courseCollection.InsertOneAsync(newCourse);

            CourseDto courseDto = _mapper.Map<CourseDto>(newCourse);

            return Response.Return(Response.ResponseStatusEnum.Success, "Kurs başarıyla eklendi.", courseDto);
        }

        public async Task<Response> UpdateCourseAsync(CourseUpdateDto courseUpdateDto)
        {
            Course updateCourse = _mapper.Map<Course>(courseUpdateDto);

            Course result =
                await _courseCollection.FindOneAndReplaceAsync(
                    x => x.Id == courseUpdateDto.Id && x.UserId == courseUpdateDto.UserId, updateCourse);

            if (result is null)
            {
                return Response.Return(Response.ResponseStatusEnum.Warning, "Kurs bulunamadı.", null);
            }

            return Response.Return(Response.ResponseStatusEnum.Success, "Kurs başarıyla güncellendi.", null);
        }

        public async Task<Response> DeleteCourseAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response.Return(Response.ResponseStatusEnum.Success, "Kurs başarıyla silindi.", null);
            }

            return Response.Return(Response.ResponseStatusEnum.Warning, "Kurs bulunamadı.", null);
        }
    }
}