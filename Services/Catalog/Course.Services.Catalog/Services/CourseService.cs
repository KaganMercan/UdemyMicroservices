using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        // Get All Async
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();
            if (courses.Any())
            {
                foreach(var course in courses) {
                    course.Category =  await _categoryCollection.Find<Category>(x => x.Id == course.Id).FirstOrDefaultAsync();          
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);
        }

        // Find By Id (Userin sadece, 1 tane kayıtlı kursu olabilir.)
        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if(course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            return GetAllByUserIdAsync(userId, _categoryCollection);
        }

        // Get all courses by user id. (Bir user'ın birden fazla kursu olabilir.)
        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId, IMongoCollection<Category> _categoryCollection)
        {
            var courses = await _courseCollection.Find<Course>(x => x.Id == userId).ToListAsync();
            if (courses.Any())
            {
                foreach(var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == userId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }
        // Create new course data.
        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        // Update course data.
        public async Task<Response<NoContent>>UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);
            if(result == null)
            {
                return Response<NoContent>.Fail("Course not found.", 404);
            }
            return Response<NoContent>.Success(204);
        }
        // Delete course data.
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            // Delete id'yi bulduğunda gerçekleşicek.
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
            // Eğer id'yi bulamadıysa result null gelir.
            if (result.DeletedCount > 0)
            {
                // Eğer deletedCount 0'dan büyükse silmiştir. Burada response'u success döndürebiliriz.
                return Response<NoContent>.Success(204);
            }
            else
            {
                // Burada response olarak id'yi bulamadığından dolayı "böyle bir course datası bulunamadı infosu bastırılabilir".
                // Course datasını bastırmaya gerek yok çünkü, zaten delete olduysa bize statusun dönmesi yetecektir.
                return Response<NoContent>.Fail("Course not found.", 404);
            }
        }
    }
}
