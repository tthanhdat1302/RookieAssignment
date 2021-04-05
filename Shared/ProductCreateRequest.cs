using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
         public decimal Price{get;set;}
        public string Description{get;set;}
        public byte[] Image{get;set;}

        [Required]
        public int CategoryId{get;set;}
    }
}