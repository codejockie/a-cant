using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.May2020.Data.Context;
using Hahn.ApplicatonProcess.May2020.Data.Models;
using Hahn.ApplicatonProcess.May2020.Data.Repository;
using Hahn.ApplicatonProcess.May2020.Data.Repository.Base;
using Hahn.ApplicatonProcess.May2020.Data.Services;
using Hahn.ApplicatonProcess.May2020.Domain.Repositories;
using Hahn.ApplicatonProcess.May2020.Web.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.May2020.Web
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
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.WithOrigins("http://localhost:8080").AllowAnyHeader();
        });
      });

      services.AddControllers().AddFluentValidation();

      // In-Memory DB
      services.AddDbContext<AppDbContext>(c => c.UseInMemoryDatabase("InMem"));

      // Swagger UI
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "A-cant API",
          Version = "1.0.0",
          Description = "A simple .NET Core API",
          Contact = new OpenApiContact
          {
            Name = "John Kennedy",
            Email = string.Empty,
            Url = new Uri("https://github.com/codejockie"),
          },
          License = new OpenApiLicense
          {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
          }
        });

        // Enable examples
        c.ExampleFilters();

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });

      services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

      // Fluent Validation
      services.AddHttpClient<IValidator<ApplicantModel>, ApplicantValidator>();

      // Domain Layer
      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      services.AddScoped<IApplicantRepository, ApplicantRepository>();

      // Data Layer
      services.AddScoped<IApplicantService, ApplicantService>();

      services.AddAutoMapper(typeof(Startup));

      // In production, the Aurelia files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();
      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "A-cant API V1");
      });

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseStaticFiles();
      
      app.UseSpaStaticFiles();

      app.UseCors();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
        }
      });
    }
  }
}
