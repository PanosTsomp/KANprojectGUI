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
        // Setup configuration from environment variables
        var configBuilder = new ConfigurationBuilder().AddEnvironmentVariables(); // allows to take connString from env variable

        Configuration = configBuilder.Build();

        // If we want to configure something we have to do it, before Avalonia gets initialized
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont().LogToTrace();
}
