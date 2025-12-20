namespace CellularCompany.Views;

/// <summary>
/// represents the plans list page
/// </summary>
public partial class PlansPage : ContentPage
{
    private readonly PlansViewModel _viewModel;

    /// <summary>
    /// initializes a new instance of the plans page
    /// </summary>
    /// <param name="viewModel">view model instance</param>
    public PlansPage(PlansViewModel viewModel)
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
        await _viewModel.LoadPlansCommand.ExecuteAsync(null);
    }
}
