namespace CellularCompany;

/// <summary>
/// represents the application shell for navigation
/// </summary>
public partial class AppShell : Shell
{
    /// <summary>
    /// initializes a new instance of the app shell
    /// </summary>
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ClientDetailPage), typeof(ClientDetailPage));
        Routing.RegisterRoute(nameof(PlanDetailPage), typeof(PlanDetailPage));
        Routing.RegisterRoute(nameof(ContractDetailPage), typeof(ContractDetailPage));
    }
}
