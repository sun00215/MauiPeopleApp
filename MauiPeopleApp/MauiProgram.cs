using Microsoft.Extensions.Logging;
using MauiPeopleApp.Services;
using MauiPeopleApp.ViewModels;
using MauiPeopleApp.Views;

namespace MauiPeopleApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services
        builder.Services.AddSingleton<PersonService>();
        
        // Register ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<PersonListViewModel>();
        builder.Services.AddTransient<PersonDetailViewModel>();
        
        // Register Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<PersonListPage>();
        builder.Services.AddTransient<PersonDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}