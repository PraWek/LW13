namespace CellularCompany.ViewModels;

/// <summary>
/// provides view model for contracts list page
/// </summary>
public partial class ContractsViewModel : ObservableObject
{
    private readonly SupabaseService _supabaseService;

    /// <summary>
    /// gets the collection of contracts
    /// </summary>
    public ObservableCollection<Contract> Contracts { get; } = new();

    /// <summary>
    /// gets or sets a value indicating whether data is loading
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// initializes a new instance of the contracts view model
    /// </summary>
    /// <param name="supabaseService">supabase service instance</param>
    public ContractsViewModel(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    /// <summary>
    /// loads contracts from the database
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task LoadContractsAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            await _supabaseService.InitializeAsync();
            var contracts = await _supabaseService.GetContractsAsync();

            Contracts.Clear();
            foreach (var contract in contracts)
                Contracts.Add(contract);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load contracts: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// navigates to add contract page
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task AddContractAsync() => await Shell.Current.GoToAsync(nameof(ContractDetailPage));

    /// <summary>
    /// navigates to edit contract page
    /// </summary>
    /// <param name="contract">contract to edit</param>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task EditContractAsync(Contract contract) => await Shell.Current.GoToAsync(nameof(ContractDetailPage), new Dictionary<string, object> { { "Contract", contract } });

    /// <summary>
    /// deletes a contract from the database
    /// </summary>
    /// <param name="contract">contract to delete</param>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task DeleteContractAsync(Contract contract)
    {
        try
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirm", $"Delete contract {contract.ContractNumber}?", "Yes", "No");
            if (!confirm) return;

            await _supabaseService.DeleteContractAsync(contract.Id);
            Contracts.Remove(contract);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete contract: {ex.Message}", "OK");
        }
    }
}
