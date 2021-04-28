using AutoMapper;
using GreenFlux.API.Models;
using GreenFlux.DataAccess.Base;
using GreenFlux.DataAccess.EF;
using GreenFlux.Service;
using GreenFlux.Service.Base;
using GreenFlux.Service.Base.Validators;
using GreenFlux.Service.Tools;
using GreenFlux.Service.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unity;

namespace GreenFlux.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Configure Unity container
        public void ConfigureContainer(IUnityContainer container)
        {
            //Tools
            container.RegisterSingleton<IConnectorKeyGenerator,ConnectorKeyGenerator>();
            container.RegisterSingleton<ISuggester, Suggester>();
            //Register validators
            container.RegisterType<ICommonValidator, CommonValidator>();
            container.RegisterType<IGroupValidator, GroupValidator>();
            container.RegisterType<IChargeStationValidator, ChargeStationValidator>();
            container.RegisterType<IConnectorValidator, ConnectorValidator>();
            //Regiters services
            container.RegisterType<IGroupService, GroupService>();
            container.RegisterType<IChargeStationService, ChargeStationService>();
            container.RegisterType<IConnectorService, ConnectorService>();
            //Register repositories
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            //Register unitOfWork
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            //Register AutoMappder
            var mapperConfig = AutoMapperConfiguration.Initialize().CreateMapper();
            container.RegisterInstance<IMapper>(mapperConfig);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDataAccessServices();
           
            services.AddControllers();

            services.AddSwaggerGen();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {         
                var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
