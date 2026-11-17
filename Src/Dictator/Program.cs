using Dictator.ConsoleInterface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dictator;

class Program
{
    static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        Game game = host.Services.GetRequiredService<Game>();

        game.Start();
    }

    /// <summary>
    ///     Creates a new instance of the host builder with all the required services added to the container.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args).ConfigureServices((_, services) => services
            .AddDictatorGame()
            .AddScoped<IConsoleService, ConsoleService>()
        );
}