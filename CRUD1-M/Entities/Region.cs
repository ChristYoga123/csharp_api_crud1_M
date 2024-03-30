using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD1_M.Entities
{
    public class Region
    {
        public int id { get; set; }

        public string name { get; set; }
        public List<Country> countries { get; set; }
    }
}
