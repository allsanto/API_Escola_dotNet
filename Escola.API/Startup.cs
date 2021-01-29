using AutoMapper;
using Dapper;
using Escola.API.Business;
using Escola.API.Data.Repositories;
using Escola.API.Domain.Models.Request;
using Escola.API.Filters;
using Escola.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using static Escola.API.Filters.ValidateModelFilter;

namespace Escola.API
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
            #region :: Swagger ::
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Escola API",
                    Version = "v1"
                });

                // Comentarios Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                conf.IncludeXmlComments(xmlPath);

            });


            #endregion

            #region :: Dapper ::
            // Toda vez que for instaciado um determinado scopo o ADDSCOPED instacia a classe para o scopo
            services.AddScoped<AlunoRepository>();
            services.AddScoped<UnidadeRepository>();
            services.AddScoped<ProfessorRepository>();
            DefaultTypeMap.MatchNamesWithUnderscores = true; // Solução endeter colunas com caracteres
            #endregion

            #region :: Automapper ::
            services.AddAutoMapper(new Action<IMapperConfigurationExpression>(x => { }), typeof(Startup));

            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region :: FluentValidation ::
            services.AddMvc(options => { options.Filters.Add(typeof(ValidateModelAttibute)); }).AddFluentValidation();

            services.AddScoped<IValidator<AlunoRequest>, AlunoValidator>(); // Depois ver quando é chamado o validator
            #endregion

            services.AddMvc();
            services.AddControllers();

            #region Business
            services.AddTransient<AlunoBL>();
            services.AddTransient<UnidadeBL>();
            services.AddTransient<ProfessorBL>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region :: Swagger ::
            app.UseSwagger();
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "Escola API");
            });
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
