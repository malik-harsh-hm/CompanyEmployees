using CompanyEmployees.Extensions;
using CompanyEmployees.Extentions;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace CompanyEmployees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            // ------------------------Add services to IServiceCollection-------------------
            // Custom extension methods for IServiceCollection
            builder.Services.ConfigureCors();
            builder.Services.ConfigureResponseCaching();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureIdentity();
            builder.Services.ConfigureJWT(builder.Configuration);
            // To enable our custom error responses from the actions
            builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            // Controllers
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly); // registers only the controllers in Presentation Layer

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Automapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            // ------------------------Build the WebApplication-------------------
            WebApplication? app = builder.Build();                          // Build method builds the WebApplication and registers all the services added with IOC.
            // Global error handling
            var logger = app.Services.GetRequiredService<ILoggerManager>(); // So, we have to extract the ILoggerManager service as its injected after build
            app.ConfigureExceptionHandler(logger);
            // ------------------------Configure the HTTP request pipeline------------------------
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors("MyCorsPolicy");
            app.UseResponseCaching();
            app.UseAuthentication();                        // Identity
            app.UseAuthorization();
            app.MapControllers();                           // adds the endpoints from controller actions to the IEndpointRouteBuilder
            app.Run();                                      // that runs the application
        }
    }
}
