using System.ComponentModel.DataAnnotations;

namespace CRUD1_M.DTOs
{
    public class RegionDTO
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
    }
}
