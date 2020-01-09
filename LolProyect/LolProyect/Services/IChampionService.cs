using LolProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect.Services
{
    public interface IChampionService
    {
        Task<IEnumerable<Champion>> GetChampions(int regionId);
        Task<Champion> GetChampionAsync(int regionId, int id);
        Task<Champion> CreateChampionAsync(int regionId, Champion champion);
        Task<Champion> UpdateChampionAsync(int regionId, int id, Champion champion);
        Task<bool> DeleteChampion(int regionId, int id);
    }
}
