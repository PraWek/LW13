using Supabase;

namespace CellularCompany.Services;

/// <summary>
/// provides access to supabase database operations
/// </summary>
public class SupabaseService
{
    private readonly Supabase.Client _client;

    /// <summary>
    /// initializes a new instance of the supabase service
    /// </summary>
    public SupabaseService()
    {
        var url = "https://qxblqubrzmjjhajfapdi.supabase.co";
        var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InF4YmxxdWJyem1qamhhamZhcGRpIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjYyMTc4NTgsImV4cCI6MjA4MTc5Mzg1OH0.VY3QiZWtsAzNa-ptOsTkYqCXCVz2VyGSNye4uWVHD94";

        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        };

        _client = new Supabase.Client(url, key, options);
    }

    /// <summary>
    /// initializes the supabase client connection
    /// </summary>
    /// <returns>task representing the asynchronous operation</returns>
    public async Task InitializeAsync() => await _client.InitializeAsync();

    /// <summary>
    /// gets all clients from the database
    /// </summary>
    /// <returns>list of clients</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<List<Client>> GetClientsAsync()
    {
        var response = await _client.From<Client>().Get();
        return response.Models;
    }

    /// <summary>
    /// gets a client by identifier
    /// </summary>
    /// <param name="id">client identifier</param>
    /// <returns>client or null if not found</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Client?> GetClientByIdAsync(Guid id)
    {
        var response = await _client.From<Client>().Where(c => c.Id == id).Single();
        return response;
    }

    /// <summary>
    /// creates a new client in the database
    /// </summary>
    /// <param name="client">client to create</param>
    /// <returns>created client with generated identifier</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Client> CreateClientAsync(Client client)
    {
        var response = await _client.From<Client>().Insert(client);
        return response.Models.First();
    }

    /// <summary>
    /// updates an existing client in the database
    /// </summary>
    /// <param name="client">client to update</param>
    /// <returns>updated client</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Client> UpdateClientAsync(Client client)
    {
        var response = await _client.From<Client>().Update(client);
        return response.Models.First();
    }

    /// <summary>
    /// deletes a client from the database
    /// </summary>
    /// <param name="id">client identifier</param>
    /// <returns>task representing the asynchronous operation</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task DeleteClientAsync(Guid id) => await _client.From<Client>().Where(c => c.Id == id).Delete();

    /// <summary>
    /// gets all plans from the database
    /// </summary>
    /// <returns>list of plans</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<List<Plan>> GetPlansAsync()
    {
        var response = await _client.From<Plan>().Get();
        return response.Models;
    }

    /// <summary>
    /// gets a plan by identifier
    /// </summary>
    /// <param name="id">plan identifier</param>
    /// <returns>plan or null if not found</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Plan?> GetPlanByIdAsync(Guid id)
    {
        var response = await _client.From<Plan>().Where(p => p.Id == id).Single();
        return response;
    }

    /// <summary>
    /// creates a new plan in the database
    /// </summary>
    /// <param name="plan">plan to create</param>
    /// <returns>created plan with generated identifier</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Plan> CreatePlanAsync(Plan plan)
    {
        var response = await _client.From<Plan>().Insert(plan);
        return response.Models.First();
    }

    /// <summary>
    /// updates an existing plan in the database
    /// </summary>
    /// <param name="plan">plan to update</param>
    /// <returns>updated plan</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Plan> UpdatePlanAsync(Plan plan)
    {
        var response = await _client.From<Plan>().Update(plan);
        return response.Models.First();
    }

    /// <summary>
    /// deletes a plan from the database
    /// </summary>
    /// <param name="id">plan identifier</param>
    /// <returns>task representing the asynchronous operation</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task DeletePlanAsync(Guid id) => await _client.From<Plan>().Where(p => p.Id == id).Delete();

    /// <summary>
    /// gets all contracts from the database
    /// </summary>
    /// <returns>list of contracts</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<List<Contract>> GetContractsAsync()
    {
        var response = await _client.From<Contract>().Get();
        return response.Models;
    }

    /// <summary>
    /// gets a contract by identifier
    /// </summary>
    /// <param name="id">contract identifier</param>
    /// <returns>contract or null if not found</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Contract?> GetContractByIdAsync(Guid id)
    {
        var response = await _client.From<Contract>().Where(c => c.Id == id).Single();
        return response;
    }

    /// <summary>
    /// creates a new contract in the database
    /// </summary>
    /// <param name="contract">contract to create</param>
    /// <returns>created contract with generated identifier</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Contract> CreateContractAsync(Contract contract)
    {
        var response = await _client.From<Contract>().Insert(contract);
        return response.Models.First();
    }

    /// <summary>
    /// updates an existing contract in the database
    /// </summary>
    /// <param name="contract">contract to update</param>
    /// <returns>updated contract</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task<Contract> UpdateContractAsync(Contract contract)
    {
        var response = await _client.From<Contract>().Update(contract);
        return response.Models.First();
    }

    /// <summary>
    /// deletes a contract from the database
    /// </summary>
    /// <param name="id">contract identifier</param>
    /// <returns>task representing the asynchronous operation</returns>
    /// <exception cref="Exception">thrown when database operation fails</exception>
    public async Task DeleteContractAsync(Guid id) => await _client.From<Contract>().Where(c => c.Id == id).Delete();
}
