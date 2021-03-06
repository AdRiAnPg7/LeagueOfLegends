﻿// <auto-generated />
using System;
using LolProyect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LolProyect.Data.Migrations
{
    [DbContext(typeof(LolDBContext))]
    [Migration("20200101232742_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LolProyect.Data.Entity.ChampionEntity", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImgBanner");

                    b.Property<string>("ImgCard");

                    b.Property<string>("ImgIcon");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("RegionId");

                    b.Property<string>("SafeLane")
                        .IsRequired();

                    b.Property<string>("Skills")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Champions");
                });

            modelBuilder.Entity("LolProyect.Data.Entity.RegionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImgBanner");

                    b.Property<string>("ImgCrsl");

                    b.Property<string>("ImgLogo");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("LolProyect.Data.Entity.ChampionEntity", b =>
                {
                    b.HasOne("LolProyect.Data.Entity.RegionEntity", "Region")
                        .WithMany("Champs")
                        .HasForeignKey("RegionId");
                });
#pragma warning restore 612, 618
        }
    }
}
