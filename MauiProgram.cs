using Microsoft.Extensions.Logging;
using RezervareFilme;
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
                .UseMauiApp<App>().UseXceedMauiToolkit(FluentDesignAccentColor.DarkLily)
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LocalDbService>();

            builder.Services.AddTransient<MoviesPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
