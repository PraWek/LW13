namespace CellularCompany.ViewModels;

/// <summary>
/// provides view model for plan detail page
/// </summary>
[QueryProperty(nameof(Plan), "Plan")]
public partial class PlanDetailViewModel : ObservableObject
{
    private readonly SupabaseService _supabaseService;

    /// <summary>
    /// gets or sets the plan being edited
    /// </summary>
    [ObservableProperty]
    private Plan? plan;

    /// <summary>
    /// gets or sets the plan name
    /// </summary>
    [ObservableProperty]
    private string name = string.Empty;

    /// <summary>
    /// gets or sets the plan description
    /// </summary>
    [ObservableProperty]
    private string description = string.Empty;

    /// <summary>
    /// gets or sets the monthly price
    /// </summary>
    [ObservableProperty]
    private string price = "0";

    /// <summary>
    /// gets or sets the included minutes
    /// </summary>
    [ObservableProperty]
    private string minutes = "0";

    /// <summary>
    /// gets or sets the included internet in gigabytes
    /// </summary>
    [ObservableProperty]
    private string internetGb = "0";

    /// <summary>
    /// gets or sets the included sms messages
    /// </summary>
    [ObservableProperty]
    private string sms = "0";

    /// <summary>
    /// gets or sets the plan status
    /// </summary>
    [ObservableProperty]
    private string status = "active";

    /// <summary>
    /// gets a value indicating whether this is a new plan
    /// </summary>
    public bool IsNewPlan => Plan == null;

    /// <summary>
    /// initializes a new instance of the plan detail view model
    /// </summary>
    /// <param name="supabaseService">supabase service instance</param>
    public PlanDetailViewModel(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    /// <summary>
    /// handles plan property changes
    /// </summary>
    /// <param name="value">new plan value</param>
    partial void OnPlanChanged(Plan? value)
    {
        if (value != null)
        {
            Name = value.Name;
            Description = value.Description;
            Price = value.Price.ToString();
            Minutes = value.Minutes.ToString();
            InternetGb = value.InternetGb.ToString();
            Sms = value.Sms.ToString();
            Status = value.Status;
        }
    }

    /// <summary>
    /// saves the plan to the database
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task SaveAsync()
    {
        try
        {
            await _supabaseService.InitializeAsync();

            if (IsNewPlan)
            {
                var newPlan = new Plan
                {
                    Name = Name,
                    Description = Description,
                    Price = decimal.Parse(Price),
                    Minutes = int.Parse(Minutes),
                    InternetGb = int.Parse(InternetGb),
                    Sms = int.Parse(Sms),
                    Status = Status
                };
                await _supabaseService.CreatePlanAsync(newPlan);
            }
            else
            {
                Plan.Name = Name;
                Plan.Description = Description;
                Plan.Price = decimal.Parse(Price);
                Plan.Minutes = int.Parse(Minutes);
                Plan.InternetGb = int.Parse(InternetGb);
                Plan.Sms = int.Parse(Sms);
                Plan.Status = Status;
                await _supabaseService.UpdatePlanAsync(Plan);
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save plan: {ex.Message}", "OK");
        }
    }

    /// <summary>
    /// cancels the edit operation
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task CancelAsync() => await Shell.Current.GoToAsync("..");
}
