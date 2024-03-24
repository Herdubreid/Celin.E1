using Celin.Services;
using Celin.ViewModels;
using Celin.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Celin;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialOutlined");
            });

        /* Config */
        var a = Assembly.GetExecutingAssembly();
        var settings = $"{nameof(Celin)}.Resources.appsettings.json";
        using var stream = a.GetManifestResourceStream(settings)
            ?? throw new ArgumentNullException(nameof(a.GetManifestResourceStream));
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        builder.Configuration.AddConfiguration(config);

        /* Logger */
        var loggingSection = config.GetSection("Logging");
        builder.Services.AddLogging(log =>
        {
            log.AddConfiguration(loggingSection);
#if DEBUG
            log.AddDebug();
#endif
            log.AddConsole();
        });

        /* Services */
        builder.Services
            .AddSingleton<Host>();

        /* ViewModels */
        builder.Services
            .AddScoped<AddressBookViewModel>();

        /* Pages */
        builder.Services
            .AddScoped<AboutPage>()
            .AddScoped<AddressBookPage>();

        return builder.Build();
    }
}
