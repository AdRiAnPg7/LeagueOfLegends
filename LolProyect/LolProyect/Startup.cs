using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using LolProyect.Data;
using LolProyect.Data.Repositories;
using LolProyect.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LolProyect
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IChampionService, ChampionService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<ILolRepository, LolRepository>();

            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<LolDBContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LeagueOfLeguendsApiDatabase")
                    )
            );
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => {
                    options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader();
                });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
