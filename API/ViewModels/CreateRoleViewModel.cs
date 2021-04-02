using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName{get;set;}
    }
}