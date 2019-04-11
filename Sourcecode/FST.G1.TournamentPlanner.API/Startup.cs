using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FST.G1.TournamentPlanner.API.Contracts;
using FST.G1.TournamentPlanner.API.Repositories;
using FST.G1.TournamentPlanner.DB.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FST.G1.TournamentPlanner.API.Services;

namespace FST.G1.TournamentPlanner.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("https://fstg1tournamentplannerapi.azurewebsites.net");
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddDbContext<PlannerContext>(o => o.UseSqlServer(Configuration["sqlconnection:connectionString"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
