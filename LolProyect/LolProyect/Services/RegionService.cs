using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LolProyect.Data.Entity;
using LolProyect.Data.Repositories;
using LolProyect.Exceptions;
using LolProyect.Models;

namespace LolProyect.Services
{
    public class RegionService : IRegionService
    {
        private ILolRepository lolRepository;
        private readonly IMapper mapper;
        public RegionService(ILolRepository lolRepository, IMapper mapper)
        {
            this.lolRepository = lolRepository;
            this.mapper = mapper;
        }
        public async Task<Region> CreateRegionAsync(Region newRegion)
        {
            var regionentity = mapper.Map<RegionEntity>(newRegion);

            lolRepository.AddRegionAsync(regionentity);
            if (await lolRepository.SaveChangesAsync())
            {
                return mapper.Map<Region>(regionentity);
            }

            throw new Exception("There were an error with the DB");
        }
        
        public async Task<bool> DeleteRegionAsync(int id)
        {
            await ValidateRegion(id);
            await lolRepository.DeleteRegionAsync(id);
            if (await lolRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<Region> GetRegionAsync(int id, bool showChampions)
        {
            var regionEntity = await lolRepository.GetRegionAsync(id, showChampions);

            if (regionEntity == null)
            {
                throw new NotFoundException("region not found");
            }

            return mapper.Map<Region>(regionEntity);
        }

        public async Task<IEnumerable<Region>> GetRegionsAsync(string orderBy, bool showChampions)
        {
            orderBy = orderBy.ToLower();
            if (!allowedOrderByQueries.Contains(orderBy))
            {
                throw new InvalidOperationException($"Invalid \" {orderBy} \" orderBy query param. The allowed values are {string.Join(",", allowedOrderByQueries)}");
            }

            var regionEntities = await lolRepository.GetRegions(orderBy, showChampions);
            return mapper.Map<IEnumerable<Region>>(regionEntities);
        }

        public async Task<Region> UpdateRegionAsync(int id, Region newRegion)
        {
            if (id != newRegion.Id)
            {
                throw new InvalidOperationException("URL id needs to be the same as Author id");
            }
            await ValidateRegion(id);

            newRegion.Id = id;
            var regionEntity = mapper.Map<RegionEntity>(newRegion);
            lolRepository.UpdateRegionAsync(regionEntity);
            if (await lolRepository.SaveChangesAsync())
            {
                return mapper.Map<Region>(regionEntity);
            }

            throw new Exception("There were an error with the DB");
        }

        private HashSet<string> allowedOrderByQueries = new HashSet<string>()
        {
            "id",
            "name",
        };
        private async Task ValidateRegion(int id)
        {
            var author = await lolRepository.GetRegionAsync(id);
            if (author == null)
            {
                throw new NotFoundException("invalid region to delete");
            }
            lolRepository.DetachEntity(author);
        }
    }
}
