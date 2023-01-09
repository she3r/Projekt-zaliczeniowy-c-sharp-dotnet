using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Catalog.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json;
using ProjektZaliczeniowy.Repositories;

namespace ProjektZaliczeniowy
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // serializuje kazdy guid w bazie danych na string
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String)); // serializuje kazda date w bazie danych na string
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();    
            // singletony
            services.AddSingleton<IMongoClient>(provider =>
            {
                // wez ustawienia z appsettings.json (27017 to port w dockerze)
                return new MongoClient(mongoDbSettings.ConnectionString);
            }); 
            // singleton powoduje ze id z repozytorium pozostaja te same czyli z kazdym requestem nie tworzy sie nowa kolekcja
            services.AddSingleton<IScoresRepository, MongoDbScoresRepository>();
            services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();
            
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
                // nie chcemy usuwac sufiksow w runtimie bo wykorzystujemy nameof 
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjektZaliczeniowy", Version = "v1" });
            });
            services.AddHealthChecks()
                .AddMongoDb(mongoDbSettings.ConnectionString,name:"mongodb",timeout: TimeSpan.FromSeconds(3), tags: new[]{"ready"});
            // services.AddSingleton<IItemsRepository,InMemItemsRepository>(); // opcja testowa bez bazy danych

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjektZaliczeniowy v1"));
            }

            if (env.IsDevelopment())
            {
                app.UseHttpsRedirection();
                
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                // {
                //     Predicate = check => check.Tags.Contains("ready"),   // tylko dla health checkow z tagiem ready
                //     ResponseWriter = async (context, report) =>
                //     {
                //         var healthView = new
                //         {
                //             status = report.Status.ToString(),
                //             checks = report.Entries.Select(entry =>
                //                 new
                //                 {
                //                     name = entry.Key,
                //                     status = entry.Value.Status.ToString(),
                //                     exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                //                     duration = entry.Value.Duration.ToString()
                //                 })
                //         };
                //         var result = JsonSerializer.Serialize(healthView);
                //
                //         context.Response.ContentType = MediaTypeNames.Application.Json;
                //         await context.Response.WriteAsync(result);
                //     }
                // });
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains("ready")
                });
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                    Predicate = (_) => false
                });
                

            });
        }
    }
}