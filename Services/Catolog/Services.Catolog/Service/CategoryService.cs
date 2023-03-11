using AutoMapper;
using MongoDB.Driver;
using Services.Catolog.Dto;
using Services.Catolog.Model;
using Services.Catolog.Setting;
using Shared.Util;

namespace Services.Catolog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSetting databaseSetting)
        {
            _mapper = mapper;

            var client = new MongoClient(databaseSetting.ConnectionString);

            var database = client.GetDatabase(databaseSetting.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSetting.CategoryCollectionName);
        }

        public async Task<Response> GetCategoryAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return Response.Return(Response.ResponseStatusEnum.Success, "Kategoriler başarıyla alındı.", categories);
        }

        public async Task<Response> FindByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category is null)
            {
                return Response.Return(Response.ResponseStatusEnum.Error, "Kategori bulunamadı.", null);
            }

            CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);

            return Response.Return(Response.ResponseStatusEnum.Success, "Kategori başarıyla getirildi.", categoryDto);
        }

        public async Task<Response> CreateCategoryAsync(CategoryDto categoryDto)
        {
            Category newCategory = _mapper.Map<Category>(categoryDto);

            await _categoryCollection.InsertOneAsync(newCategory);

            categoryDto = _mapper.Map<CategoryDto>(newCategory);

            return Response.Return(Response.ResponseStatusEnum.Success, "Kategori başarıyla eklendi.", categoryDto);
        }
    }
}