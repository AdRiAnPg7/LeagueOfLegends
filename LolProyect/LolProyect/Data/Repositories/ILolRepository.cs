using LolProyect.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect.Data.Repositories
{
    public interface ILolRepository
    {
        //
        Task<RegionEntity> GetRegionAsync(int id, bool showChamps= true);
        Task<IEnumerable<RegionEntity>> GetRegions(string orderBy = "id", bool showChamps= true);
        Task DeleteRegionAsync(int id);
        void UpdateRegionAsync(RegionEntity region);
        void AddRegionAsync(RegionEntity region);

        //
        Task<IEnumerable<ChampionEntity>> GetChampions(int regionId);
        Task<ChampionEntity> GetChampionAsync(int id, bool showRegion= false);
        void CreateChampion(ChampionEntity champion);
        void UpdateChampion(ChampionEntity champion);
        Task DeleteChampion(int id);


        void DetachEntity<l>(l entity) where l : class;
        Task<bool> SaveChangesAsync();
    }
}
