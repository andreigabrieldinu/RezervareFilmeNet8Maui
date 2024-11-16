using CommunityToolkit.Maui.Markup;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using RezervareFilmeNet8.Pages;
using Xceed.Maui.Toolkit;

namespace RezervareFilmeNet8
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>().UseXceedMauiToolkit(FluentDesignAccentColor.DarkLily).UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiApp<App>().UseMauiCommunityToolkitMarkup();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LocalDbService>();
            builder.Services.AddTransient<MoviesPage>();
            builder.Services.AddTransient<RoomsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
