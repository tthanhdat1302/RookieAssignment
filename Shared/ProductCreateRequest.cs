using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Shared
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
         public decimal Price{get;set;}
        public string Description{get;set;}
        public string Image{get;set;}
        [Required]
        public IFormFile ImageFile{get;set;}
        public double RatingAVG{get;set;}

        [Required]
        public int CategoryId{get;set;}
    }
}