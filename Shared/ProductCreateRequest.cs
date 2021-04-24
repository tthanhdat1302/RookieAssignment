using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Shared
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
         public decimal Price{get;set;}
        public string Description{get;set;}
        public string Image{get;set;}      
        public IFormFile ImageFile{get;set;}
        public double RatingAVG{get;set;}
        public int CategoryId{get;set;}
    }
}