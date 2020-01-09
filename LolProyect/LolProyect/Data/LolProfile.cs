using AutoMapper;
using LolProyect.Data.Entity;
using LolProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect
{
    public class LolProfile : Profile
    {
        public LolProfile()
        {
            this.CreateMap<RegionEntity, Region>()
                .ReverseMap();

            this.CreateMap<ChampionEntity, Champion>()
                .ReverseMap();
        }
    }
}
