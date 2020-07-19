using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiPrimerWebApiM3.Contexts;
using MiPrimerWebApiM3.Helpers;

namespace MiPrimerWebApiM3
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
            /* Configuración del filtros de acción */
            services.AddScoped<MiFiltroDeAccion>();

            /* Configuración para manejo de filtros cache */
            services.AddResponseCaching();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /* Configuración para manejo de filtros de excepción */
            services.AddMvc(options =>
            {
                options.Filters.Add(new MiFiltroDeExcepcion());
            });

            /* Configuración para corregir errores de referencias ciclicas en las relaciones de las entities
               como pasa con las entidades Libro y Autor.
             */
            services.AddControllers()
                .AddNewtonsoftJson(options => 
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            /* Configuración para manejo de filtros cache */
            app.UseResponseCaching();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
