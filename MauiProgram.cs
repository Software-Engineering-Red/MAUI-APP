using UndacApp.Data;
using UndacApp.Services;
using Microsoft.Extensions.Logging;
using UndacApp.Services.Accommodation;

namespace UndacApp;

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
		builder.Services.AddSingleton<Database>();
        builder.Services.AddSingleton<ISecurityAlertService, SecurityAlertService>();
        builder.Services.AddSingleton<IContinentService, ContinentService>();
		builder.Services.AddSingleton<ISkillService, SkillService>();
		builder.Services.AddSingleton<IAlertTypeService, AlertTypeService>();
		builder.Services.AddSingleton<IBuildingTypeService, BuildingTypeService>();
		builder.Services.AddSingleton<IRoleService, RoleService>();
		builder.Services.AddSingleton<IOrganisationService, OrganisationService>();
		builder.Services.AddSingleton<IOperationalTeamStatusService, OperationalTeamStatusService>();
		builder.Services.AddSingleton<IRotaService, RotaService>();
		builder.Services.AddSingleton<IOrderStatusService, OrderStatusService>();
        builder.Services.AddSingleton<IPersonService, PersonService>();
        builder.Services.AddSingleton<ISpecialistRequestService, SpecialistRequestService>();
        builder.Services.AddSingleton<ILogisticsService, LogisticsService>();
        builder.Services.AddSingleton<IResourceTypeService, ResourceTypeService>();
        builder.Services.AddSingleton<IResourceService, ResourceService>();
        builder.Services.AddSingleton<IVolunteerService, VolunteerService>();

        builder.Services.AddSingleton<IMethodologicalDocumentationService, MethodologicalDocumentationService>();    
        
        builder.Services.AddSingleton<ICalendarEventService, CalendarEventService>();
        builder.Services.AddSingleton<INeedService, NeedService>();

		builder.Services.AddSingleton<IOperationService, OperationService>();
        builder.Services.AddSingleton<IMethodologicalDocumentationService, MethodologicalDocumentationService>();


#if DEBUG
		builder.Logging.AddDebug();
#endif


        return builder.Build();
    }
}
