using Postgrest.Attributes;
using Postgrest.Models;

namespace CellularCompany.Models;

/// <summary>
/// represents a contract in the cellular company system
/// </summary>
[Table("contracts")]
public class Contract : BaseModel
{
    /// <summary>
    /// gets or sets the unique identifier
    /// </summary>
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// gets or sets the client identifier
    /// </summary>
    [Column("client_id")]
    public Guid ClientId { get; set; }

    /// <summary>
    /// gets or sets the plan identifier
    /// </summary>
    [Column("plan_id")]
    public Guid PlanId { get; set; }

    /// <summary>
    /// gets or sets the contract number
    /// </summary>
    [Column("contract_number")]
    public string ContractNumber { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the start date
    /// </summary>
    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// gets or sets the end date
    /// </summary>
    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// gets or sets the contract status
    /// </summary>
    [Column("status")]
    public string Status { get; set; } = "active";

    /// <summary>
    /// gets or sets the creation timestamp
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
