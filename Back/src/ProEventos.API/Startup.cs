using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProEventos.API.Data;

namespace ProEventos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) //Será injetado de um dos 2 appsettings
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            ); /*Remete a string de conexão do banco de dados configurada no appsettings */

            services.AddControllers(); //Vai retornar o controler
            services.AddCors(); // Permissão de requisição
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting(); //Vai retornar o controler da rota que eu colocar

            app.UseAuthorization();

            app.UseCors(cors => cors.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin()); //Dado um acesso eu devolvo isto

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); //Vai retornar o controler da rota que eu colocar e retornar determinado end point que eu colocar
        }
    }
}
