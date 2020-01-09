using LolProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect.Services
{
    public interface IRegionService
    {
        Task<IEnumerable<Region>> GetRegionsAsync(string orderBy, bool showChampions);
        Task<Region> GetRegionAsync(int id, bool showChampions);
        Task<Region> CreateRegionAsync(Region newRegion);
        Task<Region> UpdateRegionAsync(int id, Region newRegion);
        Task<bool> DeleteRegionAsync(int id);
    }
}
