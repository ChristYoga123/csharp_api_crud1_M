using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD1_M.Controllers.Impl
{
    [ApiController]
    [Route("api/countries/")]
    public class CountryController : Controller,  GenericController<Country, int, CountryDTO>
    {
        private CountryServiceImpl countryService;
        public string credentials;

        public CountryController(IConfiguration configuration)
        {
            this.credentials = configuration.GetConnectionString("paa");
            this.countryService = new CountryServiceImpl(new Repositories.Impl.CountryRepositoryImpl(new Utils.DbUtil(this.credentials)));
        }

        [HttpGet, Authorize]
        public ActionResult<Country> findAll()
        {
            return Ok(this.countryService.findAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Country> findById(int id)
        {
            if (this.countryService.findById(id) == null)
            {
                return NotFound();
            }
            return Ok(this.countryService.findById(id));
        }

        [HttpPost]
        public ActionResult<Country> create(CountryDTO dto)
        {
            return Ok(this.countryService.create(dto));
        }

        [HttpPut("{id}")]
        public ActionResult<Country> update(int id, CountryDTO dto)
        {
            Country country = this.countryService.findById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(this.countryService.update(id, dto));
        }

        [HttpDelete("{id}")]
        public ActionResult<Country> delete(int id)
        {
            Country country = this.countryService.findById(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(this.countryService.delete(id));
        }
    }
}
