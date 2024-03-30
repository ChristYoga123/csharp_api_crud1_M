using CRUD1_M.DTOs;
using CRUD1_M.Entities;
using CRUD1_M.Repositories.Impl;

namespace CRUD1_M.Services.Impl
{
    public class RegionServiceImpl : GenericServices<Region, int, RegionDTO>
    {
        private RegionRepositoryImpl regionRepository;
        public RegionServiceImpl(RegionRepositoryImpl regionRepository)
        {
            this.regionRepository = regionRepository;
        }
        public List<Region> findAll()
        {
            return regionRepository.findAll();
        }
        public Region findById(int id)
        {
            Region region = regionRepository.findById(id);
            return region != null ? region : null;
        }
        public Region create(RegionDTO dto)
        {
            Region region = new Region();
            region.name = dto.name;
            return regionRepository.create(region);
        }
        public Region update(int id, RegionDTO dto)
        {
            Region region = new Region();
            region.id = id;
            region.name = dto.name;
            return regionRepository.update(region);
        }
        public Region delete(int id)
        {
            Region region = new Region();
            region.id = id;
            return regionRepository.delete(region);
        }
    }
}
