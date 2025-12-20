namespace CellularCompany.Views;

/// <summary>
/// represents the plan detail page
/// </summary>
public partial class PlanDetailPage : ContentPage
{
    /// <summary>
    /// initializes a new instance of the plan detail page
    /// </summary>
    /// <param name="viewModel">view model instance</param>
    public PlanDetailPage(PlanDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
