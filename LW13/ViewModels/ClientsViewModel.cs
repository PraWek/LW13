namespace CellularCompany.ViewModels;

/// <summary>
/// provides view model for clients list page
/// </summary>
public partial class ClientsViewModel : ObservableObject
{
    private readonly SupabaseService _supabaseService;

    /// <summary>
    /// gets the collection of clients
    /// </summary>
    public ObservableCollection<Client> Clients { get; } = new();

    /// <summary>
    /// gets or sets a value indicating whether data is loading
    /// </summary>
    [ObservableProperty]
    private bool isLoading;

    /// <summary>
    /// initializes a new instance of the clients view model
    /// </summary>
    /// <param name="supabaseService">supabase service instance</param>
    public ClientsViewModel(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    /// <summary>
    /// loads clients from the database
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task LoadClientsAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            await _supabaseService.InitializeAsync();
            var clients = await _supabaseService.GetClientsAsync();

            Clients.Clear();
            foreach (var client in clients)
                Clients.Add(client);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load clients: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// navigates to add client page
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task AddClientAsync() => await Shell.Current.GoToAsync(nameof(ClientDetailPage));

    /// <summary>
    /// navigates to edit client page
    /// </summary>
    /// <param name="client">client to edit</param>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task EditClientAsync(Client client) => await Shell.Current.GoToAsync(nameof(ClientDetailPage), new Dictionary<string, object> { { "Client", client } });

    /// <summary>
    /// deletes a client from the database
    /// </summary>
    /// <param name="client">client to delete</param>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task DeleteClientAsync(Client client)
    {
        try
        {
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirm", $"Delete client {client.FullName}?", "Yes", "No");
            if (!confirm) return;

            await _supabaseService.DeleteClientAsync(client.Id);
            Clients.Remove(client);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete client: {ex.Message}", "OK");
        }
    }
}
