using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarvelBO.Creators;
using MarvelBO.Notes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace MarvelBO.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<INotesPersister, NotesPersister>();
            services.AddScoped<INotesManager, NotesManager>();
            services.AddScoped<ICreatorsManager, CreatorsManager>();
            services.AddScoped<ICreatorsCache, CreatorsCache>();
            services.AddScoped<IMarvelClient, MarvelClient>();

            services.Configure<MarvelClientSettings>(Configuration.GetSection("MarvelClientSettings"));

            services.AddSingleton<IDatabase>(ConnectionMultiplexer.Connect("localhost").GetDatabase());
            services.AddSingleton<IServer>(ConnectionMultiplexer.Connect("localhost").GetServer("localhost", 6379));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
