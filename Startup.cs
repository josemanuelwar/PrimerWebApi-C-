using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
namespace WebAPIAutores
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            this.Configuration = configuration;
   
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services){
            services.AddControllers().AddJsonOptions(x => 
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // using Microsoft.EntityFrameworkCore;
            services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();    
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment evn){
            // Configure the HTTP request pipeline.
            if (evn.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}