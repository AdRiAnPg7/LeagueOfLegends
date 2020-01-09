using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LolProyect.Data.Entity;
using LolProyect.Models;
using Microsoft.EntityFrameworkCore;

namespace LolProyect.Data.Repositories
{
    public class LolRepository : ILolRepository
    {
        private LolDBContext lolDBContext;

        public LolRepository(LolDBContext lolDBContext)
        {
            this.lolDBContext = lolDBContext;
        }
        public void AddRegionAsync(RegionEntity region)
        {
            var saveRegion= lolDBContext.Regions.Add(region);
        }

        public void CreateChampion(ChampionEntity champion)
        {
            lolDBContext.Entry(champion.Region).State = EntityState.Unchanged;
            lolDBContext.Champs.Add(champion);
        }

        public async Task DeleteChampion(int id)
        {
            var songeDeleted = await lolDBContext.Champs.SingleAsync(d => d.Id == id);
            lolDBContext.Champs.Remove(songeDeleted);
        }

        public async Task DeleteRegionAsync(int id)
        {
            var region = await lolDBContext.Regions.SingleAsync(a => a.Id == id);
            lolDBContext.Regions.Remove(region); 
        }

        public Task<ChampionEntity> GetChampionAsync(int id, bool showRegion = false)
        {
            IQueryable<ChampionEntity> query = lolDBContext.Champs;
            query = query.AsNoTracking();
            if (showRegion)
            {
                query = query.Include(b => b.Region);
            }
            query = query.AsNoTracking();
            return query.SingleAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<ChampionEntity>> GetChampions(int regionId)
        {
            IQueryable<ChampionEntity> query = lolDBContext.Champs;
            query = query.AsNoTracking();
            return await query.Where(b => b.Region.Id == regionId).ToArrayAsync();
        }

        public async Task<RegionEntity> GetRegionAsync(int id, bool showChamps = true)
        {
            IQueryable<RegionEntity> query = lolDBContext.Regions;

            if (showChamps)
            {
                query = query.Include(a => a.Champs);
            }

            return await query.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<RegionEntity>> GetRegions(string orderBy = "id", bool showChamps = true)
        {
            IQueryable<RegionEntity> query = lolDBContext.Regions;

            if (showChamps)
            {
                query = query.Include(a => a.Champs);
            }
            switch (orderBy)
            {
                case "id":
                    query = query.OrderBy(a => a.Id);
                    break;
                case "name":
                    query = query.OrderBy(a => a.Name);
                    break;
                default:
                    break;
            }

            return await query.ToArrayAsync();
        }

       
        public void UpdateChampion(ChampionEntity champion)
        {
            var champPut = lolDBContext.Champs.Single(c => c.Id == champion.Id);
            champPut.Name = champion.Name;
            champPut.Title = champion.Title;
            champPut.SafeLane = champion.SafeLane;
            champPut.Type = champion.Type;
            champPut.Description = champion.Description;
            champPut.Skills = champion.Skills;
            champPut.ImgBanner = champion.ImgBanner;
            champPut.ImgIcon = champion.ImgIcon;
            champPut.ImgCard = champion.ImgCard;
            champPut.Region = champion.Region;

        }

        public void UpdateRegionAsync(RegionEntity region)
        {
            lolDBContext.Regions.Update(region);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await lolDBContext.SaveChangesAsync()) > 0;
        }
        public void DetachEntity<l>(l entity) where l : class
        {
            lolDBContext.Entry(entity).State = EntityState.Detached;
        }

    }
}
