using System.ComponentModel.DataAnnotations;

namespace CRUD1_M.DTOs
{
    public class RegisterDTO
    {
        public string? name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
