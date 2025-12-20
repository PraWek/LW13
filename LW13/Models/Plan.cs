using Postgrest.Attributes;
using Postgrest.Models;

namespace CellularCompany.Models;

/// <summary>
/// represents a tariff plan in the cellular company system
/// </summary>
[Table("plans")]
public class Plan : BaseModel
{
    /// <summary>
    /// gets or sets the unique identifier
    /// </summary>
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// gets or sets the plan name
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the plan description
    /// </summary>
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the monthly price
    /// </summary>
    [Column("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// gets or sets the included minutes
    /// </summary>
    [Column("minutes")]
    public int Minutes { get; set; }

    /// <summary>
    /// gets or sets the included internet in gigabytes
    /// </summary>
    [Column("internet_gb")]
    public int InternetGb { get; set; }

    /// <summary>
    /// gets or sets the included SMS messages
    /// </summary>
    [Column("sms")]
    public int Sms { get; set; }

    /// <summary>
    /// gets or sets the plan status
    /// </summary>
    [Column("status")]
    public string Status { get; set; } = "active";

    /// <summary>
    /// gets or sets the creation timestamp
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
