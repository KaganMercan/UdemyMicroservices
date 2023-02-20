using FreeCourse.Services.Catalog.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Services.Catalog.Dtos
{
    public class CategoryDto
    {
       
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
