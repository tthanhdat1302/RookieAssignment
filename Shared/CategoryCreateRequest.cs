using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}