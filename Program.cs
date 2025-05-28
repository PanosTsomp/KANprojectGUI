using System;
using Avalonia;
using KANprojectGUI;
using Microsoft.Extensions.Configuration;

sealed class Program
{
    public static IConfiguration Configuration { get; private set; }

    [STAThread]
    public static void Main(string[] args)
    {
        // Setup configuration from environment variables and JSON
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables(); // allows ConnectionStrings__Default

        Configuration = configBuilder.Build();

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont().LogToTrace();
}
