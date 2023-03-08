using JavidElectronics.Infrastructure.Configurations.Extentions;

namespace JavidElectronics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Register services (IoC container)
            builder.Services.ConfigureServices(builder.Configuration);

            //setup
            var app = builder.Build();

            //Configuration of middleware pipeline
            app.ConfigureMiddlewarePipeline();

            //setup
            app.Run();
        }
    }
}