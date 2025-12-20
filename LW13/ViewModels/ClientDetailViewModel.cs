namespace CellularCompany.ViewModels;

/// <summary>
/// provides view model for client detail page
/// </summary>
[QueryProperty(nameof(Client), "Client")]
public partial class ClientDetailViewModel : ObservableObject
{
    private readonly SupabaseService _supabaseService;

    /// <summary>
    /// gets or sets the client being edited
    /// </summary>
    [ObservableProperty]
    private Client? client;

    /// <summary>
    /// gets or sets the full name
    /// </summary>
    [ObservableProperty]
    private string fullName = string.Empty;

    /// <summary>
    /// gets or sets the phone number
    /// </summary>
    [ObservableProperty]
    private string phone = string.Empty;

    /// <summary>
    /// gets or sets the email address
    /// </summary>
    [ObservableProperty]
    private string email = string.Empty;

    /// <summary>
    /// gets or sets the registration date
    /// </summary>
    [ObservableProperty]
    private DateTime registrationDate = DateTime.Now;

    /// <summary>
    /// gets a value indicating whether this is a new client
    /// </summary>
    public bool IsNewClient => Client == null;

    /// <summary>
    /// initializes a new instance of the client detail view model
    /// </summary>
    /// <param name="supabaseService">supabase service instance</param>
    public ClientDetailViewModel(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    /// <summary>
    /// handles client property changes
    /// </summary>
    /// <param name="value">new client value</param>
    partial void OnClientChanged(Client? value)
    {
        if (value != null)
        {
            FullName = value.FullName;
            Phone = value.Phone;
            Email = value.Email;
            RegistrationDate = value.RegistrationDate;
        }
    }

    /// <summary>
    /// saves the client to the database
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task SaveAsync()
    {
        try
        {
            await _supabaseService.InitializeAsync();

            if (IsNewClient)
            {
                var newClient = new Client
                {
                    FullName = FullName,
                    Phone = Phone,
                    Email = Email,
                    RegistrationDate = RegistrationDate
                };
                await _supabaseService.CreateClientAsync(newClient);
            }
            else
            {
                Client.FullName = FullName;
                Client.Phone = Phone;
                Client.Email = Email;
                Client.RegistrationDate = RegistrationDate;
                await _supabaseService.UpdateClientAsync(Client);
            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to save client: {ex.Message}", "OK");
        }
    }

    /// <summary>
    /// cancels the edit operation
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    [RelayCommand]
    public async Task CancelAsync() => await Shell.Current.GoToAsync("..");
}
