using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        // GetAll(List All)
        Task<Response<List<CategoryDto>>> GetAllAsync();
        // Create
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        //// Update
        //Task<Response<CategoryDto>> UpdateAsync(CategoryDto categoryDto);
        //// Delete
        //Task<Response<CategoryDto>> DeleteAsync(CategoryDto categoryDto);
        // Find 
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
