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
    public class ChampionService : IChampionService
    {
        private ILolRepository lolRepository;
        private readonly IMapper mapper;
        public ChampionService(ILolRepository lolRepository, IMapper mapper)
        {
            this.lolRepository = lolRepository;
            this.mapper = mapper;
        }
        public async Task<Champion> CreateChampionAsync(int regionId, Champion champion)
        {
            if (champion.RegionId != null && regionId != champion.RegionId)
            {
                throw new InvalidOperationException("URL region id and regiontId should be equal");
            }
            champion.RegionId = regionId;

            var regionEntity = await ValidateRegionId(regionId);

            var champEntity = mapper.Map<ChampionEntity>(champion);
            champEntity.Region = regionEntity;


            lolRepository.CreateChampion(champEntity);
            if (await lolRepository.SaveChangesAsync())
            {
                return mapper.Map<Champion>(champEntity);
            }
            throw new Exception("There were an error with the DB");
        }

        public async Task<bool> DeleteChampion(int regionId, int id)
        {
            var val = await ValidateRegionId(regionId);
            await lolRepository.DeleteChampion(id);
            if (await lolRepository.SaveChangesAsync())
                return true;
            return false;
        }

        public async Task<Champion> GetChampionAsync(int regionId, int id)
        {
            await ValidateRegionId(regionId);
            var champEntity = await lolRepository.GetChampionAsync(id);
            return mapper.Map<Champion>(champEntity);
        }

        public async Task<IEnumerable<Champion>> GetChampions(int regionId)
        {
            var res = await lolRepository.GetChampions(regionId);
            var championes = mapper.Map<IEnumerable<Champion>>(res);
            foreach (Champion d in championes)
            {
                d.RegionId = regionId;
            }
            return championes;
        }

        public async Task<Champion> UpdateChampionAsync(int regionId, int id, Champion champion)
        {
            var region = await ValidateRegionId(regionId);
            if (id != champion.Id)
            {
                throw new Exception("Id of the Champion in URL needs to be the same that the object");
            }
            if (regionId != region.Id)
            {
                throw new Exception("The id of Artist isn't correct");
            }

            champion.Id = id;
            var cacionEntity = mapper.Map<ChampionEntity>(champion);
            lolRepository.UpdateChampion(cacionEntity);
            if (await lolRepository.SaveChangesAsync())
            {
                return mapper.Map<Champion>(cacionEntity);
            }

            throw new Exception("There were an error with the DB");
        }
        private async Task<RegionEntity> ValidateRegionId(int id)
        {
            var artista = await lolRepository.GetRegionAsync(id);
            if (artista == null)
            {
                throw new NotFoundException($"cannot found artista with id {id}");
            }
            lolRepository.DetachEntity(artista);
            return artista;
        }

        private async Task<bool> ValidateRegionAndChamp(int artistaId, int championId)
        {

            var artista = await lolRepository.GetRegionAsync(artistaId);
            if (artista == null)
            {
                throw new NotFoundException($"cannot found artista with id {artistaId}");
            }

            var champ = await lolRepository.GetChampionAsync(championId, true);
            if (champ == null || champ.Region.Id != artistaId)
            {
                throw new NotFoundException($"Songe not found with id {championId} for Artttist {artistaId}");
            }

            return true;
        }
    }
}
