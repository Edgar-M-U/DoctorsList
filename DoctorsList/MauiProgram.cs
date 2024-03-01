using CommunityToolkit.Maui;
using DoctorsList.MVVM.MODEL;
using DoctorsList.Repositories;
using Microsoft.Extensions.Logging;
using Location = DoctorsList.MVVM.MODEL.Location;

namespace DoctorsList
{
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
                });

            builder.Services.AddSingleton<LoginRepository>();
            builder.Services.AddSingleton<BaseRepository<Root>>();
            builder.Services.AddSingleton<BaseRepository<Result>>();
            builder.Services.AddSingleton<BaseRepository<Name>>();
            builder.Services.AddSingleton<BaseRepository<Location>>();
            builder.Services.AddSingleton<BaseRepository<Picture>>();
            builder.Services.AddSingleton<BaseRepository<Street>>();
            builder.Services.AddSingleton<BaseRepository<Coordinates>>();
            builder.Services.AddSingleton<BaseRepository<Timezone>>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
