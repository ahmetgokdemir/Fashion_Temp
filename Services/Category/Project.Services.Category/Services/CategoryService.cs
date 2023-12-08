using AutoMapper;
using Project.Services.Category.DTOs;
using Project.Services.Category.OptionPatternSettings;
using Project.Shared.DTOs;
// using MongoDB.Driver.Core;
using MongoDB.Driver;

namespace Project.Services.Category.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Models.Category> _categoryCollection; // table \ MongoDB › Collection

        private readonly IMapper _mapper;

        // 2:52 / 19:07 IDatabaseSettings (db'ye erişi,m)
        public CategoryService(IMapper mapper, IOptionSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString); // ** ConnectionString

            // IMongoDatabase --> MongoClient & IMongoCollection 
            var database = client.GetDatabase(databaseSettings.DatabaseName); // ** DatabaseName

            _categoryCollection = database.GetCollection<Models.Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            // 4:54 sonradan public yapacak
        }

        public async Task<Response<List<CategoryDTO>>> GetAllAsync()
        {
            // 7:33 / 19:07 Response<T>
            // _categoryCollection (IMongoCollection) --> Find
            // 8:26 get all --> => true (tüm kategorileri döner)
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            // return await _context.Set<T>().ToListAsync();

            // var categories2 = await _categoryCollection.GetAll().ToListAsync();

            return Response<List<CategoryDTO>>.Success(_mapper.Map<List<CategoryDTO>>(categories), 200);
        }

        public async Task<Response<CategoryDTO>> CreateAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Models.Category>(categoryDTO);
            await _categoryCollection.InsertOneAsync(category);

            return Response<CategoryDTO>.Success(_mapper.Map<CategoryDTO>(category), 200);
            // 14:48 / 19:07 200 (200 OK) --> get ve insert / 204 --> update ve delete
            //400 no authentication , 403 no authorization , 404 not found

        }

        public async Task<Response<CategoryDTO>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Models.Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDTO>.Fail("Category not found", 404); // ** 400 
            }

            return Response<CategoryDTO>.Success(_mapper.Map<CategoryDTO>(category), 200); // 200

            // ICategoryService
        }
    }
}
