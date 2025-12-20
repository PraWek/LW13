namespace CellularCompany.Views;

/// <summary>
/// represents the client detail page
/// </summary>
public partial class ClientDetailPage : ContentPage
{
    /// <summary>
    /// initializes a new instance of the client detail page
    /// </summary>
    /// <param name="viewModel">view model instance</param>
    public ClientDetailPage(ClientDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
