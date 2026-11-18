using Microsoft.Extensions.Logging;
namespace NekoSpot
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<NekoListPageViewModel>();
            builder.Services.AddSingleton<NekoListPage>();
            builder.Services.AddTransient<NekoEditPageViewModel>();
            builder.Services.AddTransient<NekoEditPage>();
            builder.Services.AddTransient<NekoDetailsPage>();
            builder.Services.AddTransient<NekoDetailsPageViewModel>();
            builder.Services.AddTransient<NekoMapPage>();
            builder.Services.AddSingleton<ICatDatabase, SQLiteCatDatabase>();
            builder.Services.AddTransient<NekoMapPage>();
            builder.Services.AddTransient<NekoMapViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
