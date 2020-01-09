using LolProyect.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LolProyect
{
    public class LolDBContext : DbContext
    {
        public LolDBContext(DbContextOptions<LolDBContext> wind)
            : base(wind)

        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RegionEntity>().ToTable("Regions");
            modelBuilder.Entity<RegionEntity>().HasMany(a => a.Champs).WithOne(b => b.Region);
            modelBuilder.Entity<RegionEntity>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ChampionEntity>().ToTable("Champions");
            modelBuilder.Entity<ChampionEntity>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ChampionEntity>().HasOne(b => b.Region).WithMany(a => a.Champs);
        }

        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<ChampionEntity> Champs { get; set; }
    }
}
