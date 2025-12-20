namespace CellularCompany;

/// <summary>
/// represents the main application class
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// initializes a new instance of the application
    /// </summary>
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
