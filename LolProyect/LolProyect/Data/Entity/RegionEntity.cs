using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect.Data.Entity
{
    public class RegionEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImgLogo { get; set; }
        public string ImgBanner { get; set; }
        public string ImgCrsl { get; set; }
        public virtual ICollection<ChampionEntity> Champs { get; set; }
    }
}
