using System.ComponentModel.DataAnnotations;

namespace CRUD1_M.DTOs
{
    public class RegionDTO
    {
        [Required]
        public string name { get; set; }
    }
}
