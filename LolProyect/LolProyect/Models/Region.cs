using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect.Models
{
    public class Region
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImgLogo { get; set; }
        public string ImgBanner { get; set; }
        public string ImgCrsl { get; set; }
        public IEnumerable<Champion> champions { get; set; }
    }
}
