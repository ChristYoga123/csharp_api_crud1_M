using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace CRUD1_M.Controllers.Impl
{
    [ApiController]
    [Route("api/regions/")]
    public class RegionController : Controller, GenericController<Region, int, RegionDTO>
    {
        private string credentials;
        private RegionServiceImpl regionService;
        public RegionController(IConfiguration configuration)
        {
            this.credentials = configuration.GetConnectionString("paa");
            this.regionService = new RegionServiceImpl(new Repositories.Impl.RegionRepositoryImpl(new Utils.DbUtil(this.credentials)));
        }
        [HttpGet]
        public ActionResult<Region> findAll()
        {
            return Ok(this.regionService.findAll());
        } 
        [HttpGet("{id}")]
        public ActionResult<Region> findById(int id)
        {
            if(this.regionService.findById(id) == null)
            {
                return NotFound();
            }
            return Ok(this.regionService.findById(id));
        }
        [HttpPost]
        public ActionResult<Region> create(RegionDTO dto)
        {
            return Ok(this.regionService.create(dto));
        }
        [HttpPut("{id}")]
        public ActionResult<Region> update(int id, RegionDTO dto)
        {

            Region region = this.regionService.findById(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(this.regionService.update(id, dto));
        }
        [HttpDelete("{id}")]
        public ActionResult<Region> delete(int id)
        {
            Region region = this.regionService.findById(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(this.regionService.delete(id));
        }
    }
}
