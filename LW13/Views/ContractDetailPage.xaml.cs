namespace CellularCompany.Views;

/// <summary>
/// represents the contract detail page
/// </summary>
public partial class ContractDetailPage : ContentPage
{
    /// <summary>
    /// initializes a new instance of the contract detail page
    /// </summary>
    /// <param name="viewModel">view model instance</param>
    public ContractDetailPage(ContractDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
