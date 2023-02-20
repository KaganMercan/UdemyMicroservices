using FreeCourse.Services.Catalog.Models;
using System;

namespace FreeCourse.Services.Catalog.Dtos
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static implicit operator CategoryDto(Category v)
        {
            throw new NotImplementedException();
        }
    }
}
