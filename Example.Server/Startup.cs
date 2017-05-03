namespace Example.Server
{
    using System.IO;

    using AutoMapper;

    using Dapper;

    using Example.Server.Api;
    using Example.Server.Infrastructure.Data;
    using Example.Server.Services;
    using Example.Server.Settings;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using NLog.Extensions.Logging;
    using NLog.Web;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Dapper
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            // Add framework services.
            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services.AddMvc().AddJsonOptions(option =>
            {
                option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                option.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                option.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            });

            // Elm
            services.AddElm(options =>
            {
                options.Filter = (name, lelev) => lelev >= LogLevel.Debug;
            });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("assistance", new Info { Title = "Example API", Version = "v1" });
                options.DescribeAllEnumsAsStrings();    // Enum
            });

            // Components
            services.Configure<FileSettings>(Configuration.GetSection("FileSettings"));

            var connectionString = Configuration.GetConnectionString("Default");
            services.AddSingleton<IConnectionFactory>(
                new CallbackConnectionFactory(() => new SqliteConnection(connectionString)));

            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<ApiMappingProfile>();
            })));

            // Services
            services.AddSingleton<StorageService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConnectionFactory connectionFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.AddNLogWeb();

            app.UseElmPage();
            app.UseElmCapture();

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

            //if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/assistance/swagger.json", "Example API");
                });
            }

            var sql = File.ReadAllText("database.sql");
            using (var con = connectionFactory.CreateConnection())
            {
                con.Execute(sql);
            }
        }
    }
}
