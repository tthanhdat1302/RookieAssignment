using System.Collections.Generic;

namespace API.Models
{
    public class Product
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public decimal Price{get;set;}
        public string Description{get;set;}
        public List<ProductImage> Image{get;set;}
        public int CategoryId{get;set;}
        public Category Category{get;set;}



    }
}