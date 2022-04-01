using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using WebApi.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApi;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using IntergrationA.Services.TaskService;
using IntergrationA_Update.Services.Masterdetails;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();            
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Middleware API",
        Version = "v1.0.2"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuth, Auth>();
            services.AddHostedService<RunAsyncTask>();
            services.AddTransient<IGetmasterdata,Getmasterdata>();


            var configurationSection = Configuration.GetSection("con:dbcon");
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(configurationSection.Value));
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if(!env.IsDevelopment())
            {
            app.UseHttpsRedirection();
            }
            loggerFactory.AddFile("Logs/IntegrationAPILog-{Date}.txt");
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
