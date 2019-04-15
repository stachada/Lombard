using AutoMapper;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using Lombard.DAL;
using Lombard.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LombardAPI
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
            services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();

            services.AddTransient<SeedDatabase>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IItemsRepository, ItemsRepository>();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped<ITransactionsService, TransactionsService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<IItemService, ItemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedDatabase seeder)
        {
            if (env.IsDevelopment())
            {
                seeder.Seed();
                app.UseDeveloperExceptionPage();
                
            }

            app.UseMvc();
        }
    }
}
