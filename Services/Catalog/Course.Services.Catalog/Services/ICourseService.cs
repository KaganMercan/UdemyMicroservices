using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        // READ
        Task<Response<List<CourseDto>>> GetAllAsync();
        // READ - With ID
        Task<Response<CourseDto>> GetByIdAsync(string id);
        // READ - With UserID
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        // CREATE
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        // UPDATE
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        // DELETE
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
