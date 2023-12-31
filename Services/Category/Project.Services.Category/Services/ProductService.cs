﻿using AutoMapper;
using MongoDB.Driver;
using Project.Services.Category.DTOs;
using Project.Services.Category.Models;
using Project.Services.Category.OptionPatternSettings;
using Project.Shared.DTOs;

namespace Project.Services.Category.Services
{
    public class ProductService: IProductService
    { 
        private readonly IMongoCollection<Product> _productionCollection;
        private readonly IMongoCollection<Models.Category> _categoryCollection;

        private readonly IMapper _mapper;
        //**private readonly Mass.IPublishEndpoint _publishEndpoint;

        public ProductService(IMapper mapper, IOptionSettings optionSettings /*, Mass.IPublishEndpoint publishEndpoint */ )
        {
            var mongo_connection = new MongoClient(optionSettings.ConnectionString);

            var database = mongo_connection.GetDatabase(optionSettings.DatabaseName);

            _productionCollection = database.GetCollection<Product>(optionSettings.ProductCollectionName);

            _categoryCollection = database.GetCollection<Models.Category>(optionSettings.CategoryCollectionName); // ** production.Category = aw ... da kıllanılacak 

            _mapper = mapper;

            // ** _publishEndpoint = publishEndpoint;
        }

        public async Task<Response<List<ProductDTO>>> GetAllAsync()
        {
            var productions = await _productionCollection.Find(course => true).ToListAsync();

            if (productions.Any())
            {
                foreach (var production in productions)
                {
                    // include veya join kullanılamaz.. tek tek production'ların ilgili kategorisi alınır...
                    production.Category = await _categoryCollection.Find<Models.Category>(x => x.Id == production.CategoryId).FirstAsync(); // ** _categoryCollection kullanıldı , FirstAsync --> mutlaka olmalı
                }
            }
            else
            {
                productions = new List<Product>();
            }

            return Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(productions), 200);
        }

        public async Task<Response<ProductDTO>> GetByIdAsync(string id)
        {
            var course = await _productionCollection.Find<Product>(x => x.Id == id).FirstOrDefaultAsync(); // FirstOrDefaultAsync --> olmama opsiyonu var

            if (course == null)
            {
                return Response<ProductDTO>.Fail("Course not found", 404);
            }
            course.Category = await _categoryCollection.Find<Models.Category>(x => x.Id == course.CategoryId).FirstAsync();

            return Response<ProductDTO>.Success(_mapper.Map<ProductDTO>(course), 200);
        }

        //public async Task<Response<List<ProductDTO>>> GetAllByUserIdAsync(string userId)
        //{
        //    var courses = await _productionCollection.Find<Product>(x => x.UserId == userId).ToListAsync();

        //    if (courses.Any())
        //    {
        //        foreach (var course in courses)
        //        {
        //            course.Category = await _categoryCollection.Find<Models.Category>(x => x.Id == course.CategoryId).FirstAsync();
        //        }
        //    }
        //    else
        //    {
        //        courses = new List<Product>();
        //    }

        //    return Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(courses), 200);
        //}

        public async Task<Response<ProductDTO>> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            var newCourse = _mapper.Map<Product>(productCreateDTO);

            newCourse.CreatedTime = DateTime.Now;
            await _productionCollection.InsertOneAsync(newCourse);

            return Response<ProductDTO>.Success(_mapper.Map<ProductDTO>(newCourse), 200); // newCourse tekrar map lendi ..
        }

        public async Task<Response<NoContent>> UpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            var updateCourse = _mapper.Map<Product>(productUpdateDTO);

            var result = await _productionCollection.FindOneAndReplaceAsync(x => x.Id == productUpdateDTO.Id, updateCourse);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }

            //**await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent { CourseId = updateCourse.Id, UpdatedName = courseUpdateDto.Name });

            return Response<NoContent>.Success(204); // *** 204 no content
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _productionCollection.DeleteOneAsync(x => x.Id == id); // result: DeleteResult

            if (result.DeletedCount > 0) // result.DeletedCount 
            {
                return Response<NoContent>.Success(204); // *** 204
            }
            else
            {
                return Response<NoContent>.Fail("Course not found", 404);
            }
        }
    }
}
