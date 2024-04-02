using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Repositories.Impl;

namespace CRUD1_M.Services.Impl
{
    public class CountryServiceImpl : GenericServices<Country, int, CountryDTO>
    {
        private CountryRepositoryImpl countryRepository;
        public CountryServiceImpl(CountryRepositoryImpl countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public List<Country> findAll()
        {
            return countryRepository.findAll();
        }
        public Country findById(int id)
        {
            Country country = countryRepository.findById(id);
            return country != null ? country : null;
        }
        public Country create(CountryDTO dto)
        {
            Country country = new Country();
            country.name = dto.name;
            Region region = new Region();
            region.id = dto.regionId;
            country.region = region;
            return countryRepository.create(country);
        }

        public Country update(int id, CountryDTO dto)
        {
            Country country = countryRepository.findById(id);
            if (country == null)
            {
                return null;
            }
            country.name = dto.name;
            Region region = new Region();
            region.id = dto.regionId;
            country.region = region;
            return countryRepository.update(country);
        }

        public Country delete(int id)
        {
            Country country = countryRepository.findById(id);
            if (country == null)
            {
                return null;
            }
            return countryRepository.delete(country);
        }
    }
}
