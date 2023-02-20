using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // ActionResult type, çeşitli HTTP status code'larını temsil ediyor.
        // IActionResult bir interface olarak, birden fazla data type'ın return edilmesinde kullanılıyor.
        // Örneğin, bir amacımız bir grup dataya erişmek olsun controller aracılığı ile. Bu durumda, grup datanın sayısı
        //  0'dan büyük ise IActionResult type ile status OK olarak dönebilir.

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        // LIST ENDPOINT - With id.
        // Endpoint -> course/4 -> id'si 4 olan datayı getirir. Aşağıda parametre pass'lenmeli, id verilmeli.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        // LIST ALL ENDPOINT - With user id.
        // Bu endpoint de aynı şekilde id alıyor. Eğer course/4 gibi bir tip gönderirsek yukarıdaki yapı da call olabilir.
        // bu nedenle buradaki yol değişik olmalı.
        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(response);
        }

        // CREATE ENDPOINT
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(response);
        }

        // UPDATE ENDPOINT
        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateAsync(courseUpdateDto);
            return CreateActionResultInstance(response);
        }

        // DELETE ENDPOINT
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
