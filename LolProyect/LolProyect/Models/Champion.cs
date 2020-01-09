using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect.Models
{
    public class Champion
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SafeLane { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Skills { get; set; }
        public string ImgBanner { get; set; }
        public string ImgIcon { get; set; }
        public string ImgCard { get; set; }
        public int? RegionId { get; set; }

    }
}
