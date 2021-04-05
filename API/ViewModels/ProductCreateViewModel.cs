using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API.Models
{
    public class ProductCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
         public decimal Price{get;set;}
        public string Description{get;set;}
        public IFormFile Image{get;set;}

        [Required]
        public int CategoryId{get;set;}
    }
}