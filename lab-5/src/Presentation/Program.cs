using Itmo.ObjectOrientedProgramming.Lab5.Application.DependencyInjection;
using Itmo.ObjectOrientedProgramming.Lab5.MemoryStorage.Setup;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddApplicationServices();

        builder.Services.AddInMemoryStorage();

        builder.Services.AddErrorMapping();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}