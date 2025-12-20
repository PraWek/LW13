namespace CellularCompany.Views;

/// <summary>
/// represents the contracts list page
/// </summary>
public partial class ContractsPage : ContentPage
{
    private readonly ContractsViewModel _viewModel;

    /// <summary>
    /// initializes a new instance of the contracts page
    /// </summary>
    /// <param name="viewModel">view model instance</param>
    public ContractsPage(ContractsViewModel viewModel)
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
        await _viewModel.LoadContractsCommand.ExecuteAsync(null);
    }
}
