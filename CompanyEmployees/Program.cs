using CompanyEmployees.Extentions;
using NLog;

namespace CompanyEmployees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            // ------------------------Add services to the container.-------------------

            // Custom extension methods for IServiceCollection
            builder.Services.ConfigureCors();
            builder.Services.ConfigureIISIntegration();
            builder.Services.ConfigureLoggerService();
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            // Controllers
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly); // registers only the controllers in Presentation Layer
            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Automapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            WebApplication? app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyCorsPolicy");
            app.UseAuthorization();
            app.MapControllers(); // adds the endpoints from controller actions to the IEndpointRouteBuilder
            app.Run(); // that runs the application
        }
    }
}
