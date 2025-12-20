namespace CellularCompany.Views;

/// <summary>
/// represents the clients list page
/// </summary>
public partial class ClientsPage : ContentPage
{
    private readonly ClientsViewModel _viewModel;

    /// <summary>
    /// initializes a new instance of the clients page
    /// </summary>
    /// <param name="viewModel">view model instance</param>
    public ClientsPage(ClientsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    /// <summary>
    /// handles page appearing event
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadClientsCommand.ExecuteAsync(null);
    }
}
