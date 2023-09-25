using MauiApp1.Data;
using MauiApp1.Services;
using Microsoft.Extensions.Logging;

namespace MauiApp1;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<Database>();
        builder.Services.AddSingleton<IContinentService, ContinentService>();
        builder.Services.AddSingleton<ISkillService, SkillService>();
        builder.Services.AddSingleton<ISystemTypeService, SystemTypeService>();
        return builder.Build();
	}
}
