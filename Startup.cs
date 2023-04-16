using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using PruebaApiThales.Business;
using PruebaApiThales.Business.Interface;
using PruebaApiThales.Models;
using PruebaApiThales.Repository;
using PruebaApiThales.Repository.Interface;
using System.Configuration;
using System.Globalization;
using System.Reflection;

namespace PruebaApiThales
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            string? nombreEnsamblado = Assembly.GetExecutingAssembly().GetName().Name;
            string abreviacionAmbiente = Configuration.GetSection("RunWithConfiguration")["AbbreviationEnvironment"].ToLower();

            services.AddCors();
            services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = nombreEnsamblado, Version = "v1" });
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string xmlFile = $"{nombreEnsamblado}.xml";
                string xmlPath = Path.Combine(baseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Settings to Database and other configurations
            string nameConnectionString = String.Format("{0}{1}", CultureInfo.InvariantCulture.TextInfo.ToTitleCase(abreviacionAmbiente), "ConnectionString");
            string? connectionString = Configuration.GetSection("ConnectionSqlServer")[nameConnectionString];
            services.AddDbContext<ConexionBDContext>(options => options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            services.AddHttpClient("HttpClient", client =>
            {
                client.Timeout = TimeSpan.FromMinutes(30);
            });
            services.AddHttpContextAccessor();

            // Singlenton Pattern. This instance is created to be reused through of the application
            services.AddSingleton(Configuration.GetValue<string>("SalaryByTime"));

            // Dependency injection, using business and repository interfaces .
            #region Scopeds 
            //Employee
            services.AddScoped<IEmployeeBL, EmployeeBL>();
            services.AddScoped<IEmployeeRE, EmployeeRE>();            
            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string? nombreEnsamblado = Assembly.GetExecutingAssembly().GetName().Name;
            string? abreviacionAmbiente = Configuration?.GetSection("RunWithConfiguration")?["AbbreviationEnvironment"].ToLower();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(
                string.Format("{0}{1}", abreviacionAmbiente == "local" ? string.Empty : "/employee-" + abreviacionAmbiente, "/swagger/v1/swagger.json"),
                string.Format("{0} v1", nombreEnsamblado))
            );

            app.UseCors(options =>
            {
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
            });

            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseFileServer(enableDirectoryBrowsing: true);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();

        }
    }
}
