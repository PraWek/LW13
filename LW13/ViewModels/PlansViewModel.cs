namespace CellularCompany.ViewModels;

/// <summary>
/// provides view model for plans list page
/// </summary>
public partial class PlansViewModel : ObservableObject
{
    private readonly SupabaseService _supabaseService;

    /// <summary>
    /// gets the collection of plans
    /// </summary>
    public ObservableCollection<Plan> Plans { get; } = new();

    /// <summary>
    /// gets or sets a value indicating whether data is loading
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// initializes a new instance of the plans view model
    /// </summary>
    /// <param name="supabaseService">supabase service instance</param>
    public PlansViewModel(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    /// <summary>
    /// loads plans from the database
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task LoadPlansAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            await _supabaseService.InitializeAsync();
            var plans = await _supabaseService.GetPlansAsync();

            Plans.Clear();
            foreach (var plan in plans)
                Plans.Add(plan);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load plans: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// navigates to add plan page
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task AddPlanAsync() => await Shell.Current.GoToAsync(nameof(PlanDetailPage));

    /// <summary>
    /// navigates to edit plan page
    /// </summary>
    /// <param name="plan">plan to edit</param>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task EditPlanAsync(Plan plan) => await Shell.Current.GoToAsync(nameof(PlanDetailPage), new Dictionary<string, object> { { "Plan", plan } });

    /// <summary>
    /// deletes a plan from the database
    /// </summary>
    /// <param name="plan">plan to delete</param>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task DeletePlanAsync(Plan plan)
    {
        try
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirm", $"Delete plan {plan.Name}?", "Yes", "No");
            if (!confirm) return;

            await _supabaseService.DeletePlanAsync(plan.Id);
            Plans.Remove(plan);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete plan: {ex.Message}", "OK");
        }
    }
}
