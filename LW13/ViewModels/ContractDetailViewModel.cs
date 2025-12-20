namespace CellularCompany.ViewModels;

/// <summary>
/// provides view model for contract detail page
/// </summary>
[QueryProperty(nameof(Contract), "Contract")]
public partial class ContractDetailViewModel : ObservableObject
{
    private readonly SupabaseService _supabaseService;

    /// <summary>
    /// gets or sets the contract being edited
    /// </summary>
    [ObservableProperty]
    private Contract? contract;

    /// <summary>
    /// gets or sets the selected client
    /// </summary>
    [ObservableProperty]
    private Client? selectedClient;

    /// <summary>
    /// gets or sets the selected plan
    /// </summary>
    [ObservableProperty]
    private Plan? selectedPlan;

    /// <summary>
    /// gets or sets the contract number
    /// </summary>
    [ObservableProperty]
    private string contractNumber = string.Empty;

    /// <summary>
    /// gets or sets the start date
    /// </summary>
    [ObservableProperty]
    private DateTime startDate = DateTime.Now;

    /// <summary>
    /// gets or sets the end date
    /// </summary>
    [ObservableProperty]
    private DateTime? endDate;

    /// <summary>
    /// gets or sets the contract status
    /// </summary>
    [ObservableProperty]
    private string status = "active";

    /// <summary>
    /// gets the collection of available clients
    /// </summary>
    public ObservableCollection<Client> Clients { get; } = new();

    /// <summary>
    /// gets the collection of available plans
    /// </summary>
    public ObservableCollection<Plan> Plans { get; } = new();

    /// <summary>
    /// gets a value indicating whether this is a new contract
    /// </summary>
    public bool IsNewContract => Contract == null;

    /// <summary>
    /// initializes a new instance of the contract detail view model
    /// </summary>
    /// <param name="supabaseService">supabase service instance</param>
    public ContractDetailViewModel(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
        LoadDataAsync();
    }

    /// <summary>
    /// loads clients and plans from the database
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    private async Task LoadDataAsync()
    {
        try
        {
            await _supabaseService.InitializeAsync();

            var clients = await _supabaseService.GetClientsAsync();
            foreach (var client in clients)
                Clients.Add(client);

            var plans = await _supabaseService.GetPlansAsync();
            foreach (var plan in plans)
                Plans.Add(plan);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
        }
    }

    /// <summary>
    /// handles contract property changes
    /// </summary>
    /// <param name="value">new contract value</param>
    partial void OnContractChanged(Contract? value)
    {
        if (value != null)
        {
            ContractNumber = value.ContractNumber;
            StartDate = value.StartDate.ToDateTime(TimeOnly.MinValue);
            EndDate = value.EndDate?.ToDateTime(TimeOnly.MinValue);
            Status = value.Status;

            SelectedClient = Clients.FirstOrDefault(c => c.Id == value.ClientId);
            SelectedPlan = Plans.FirstOrDefault(p => p.Id == value.PlanId);
        }
    }

    /// <summary>
    /// saves the contract to the database
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task SaveAsync()
    {
        try
        {
            if (SelectedClient == null || SelectedPlan == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a client and plan", "OK");
                return;
            }

            await _supabaseService.InitializeAsync();

            if (IsNewContract)
            {
                var newContract = new Contract
                {
                    ClientId = SelectedClient.Id,
                    PlanId = SelectedPlan.Id,
                    ContractNumber = ContractNumber,
                    StartDate = DateOnly.FromDateTime(StartDate),
                    EndDate = EndDate.HasValue ? DateOnly.FromDateTime(EndDate.Value) : null,
                    Status = Status
                };
                await _supabaseService.CreateContractAsync(newContract);
            }
            else
            {
                Contract.ClientId = SelectedClient.Id;
                Contract.PlanId = SelectedPlan.Id;
                Contract.ContractNumber = ContractNumber;
                Contract.StartDate = DateOnly.FromDateTime(StartDate);
                Contract.EndDate = EndDate.HasValue ? DateOnly.FromDateTime(EndDate.Value) : null;
                Contract.Status = Status;
                await _supabaseService.UpdateContractAsync(Contract);
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save contract: {ex.Message}", "OK");
        }
    }

    /// <summary>
    /// cancels the edit operation
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task CancelAsync() => await Shell.Current.GoToAsync("..");
}
