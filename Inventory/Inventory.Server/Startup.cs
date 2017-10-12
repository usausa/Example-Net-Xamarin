namespace Inventory.Server
{
    using System.Data.SqlClient;

    using AutoMapper;

    using Inventory.Server.Api;
    using Inventory.Server.Services;
    using Inventory.Server.Settings;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using NLog.Extensions.Logging;
    using NLog.Web;

    using Smart.AspNetCore.Filters;
    using Smart.Data;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            env.ConfigureNLog("nlog.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services.AddExceptionLogging();
            services.AddTimeLogging(options =>
            {
                options.Thresold = 5000;
            });

            services
                .AddMvc(options =>
                {
                    options.Filters.AddExceptionLogging();
                    options.Filters.AddTimeLogging();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("inventory", new Info { Title = "Inventory API", Version = "v1" });
                options.DescribeAllEnumsAsStrings();    // Enum
            });

            // Components
            services.Configure<FileSettings>(Configuration.GetSection("FileSettings"));

            var connectionString = Configuration.GetConnectionString("Default");
            services.AddSingleton<IConnectionFactory>(
                new CallbackConnectionFactory(() => new SqlConnection(connectionString)));

            services.AddSingleton<StorageService>();

            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<ApiMappingProfile>();
            })));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, ILoggerFactory loggerFactory)
        {
            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStopping);
            lifetime.ApplicationStopped.Register(OnStopped);

            loggerFactory.AddNLog();

            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Storage}/{action=Index}/{id?}");
            });

            // Swagger
            //if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/inventory/swagger.json", "Inventory API");
                });
            }
        }

        private void OnStarted()
        {
            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
        }
    }
}
