namespace CellularCompany;

/// <summary>
/// provides the entry point for the MAUI application
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// creates and configures the MAUI application
    /// </summary>
    /// <returns>configured MAUI app instance</returns>
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

        builder.Services.AddSingleton<SupabaseService>();

        builder.Services.AddSingleton<ClientsPage>();
        builder.Services.AddSingleton<ClientsViewModel>();
        builder.Services.AddTransient<ClientDetailPage>();
        builder.Services.AddTransient<ClientDetailViewModel>();

        builder.Services.AddSingleton<PlansPage>();
        builder.Services.AddSingleton<PlansViewModel>();
        builder.Services.AddTransient<PlanDetailPage>();
        builder.Services.AddTransient<PlanDetailViewModel>();

        builder.Services.AddSingleton<ContractsPage>();
        builder.Services.AddSingleton<ContractsViewModel>();
        builder.Services.AddTransient<ContractDetailPage>();
        builder.Services.AddTransient<ContractDetailViewModel>();

        return builder.Build();
    }
}
