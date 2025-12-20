using Postgrest.Attributes;
using Postgrest.Models;

namespace CellularCompany.Models;

/// <summary>
/// represents a client in the cellular company system
/// </summary>
[Table("clients")]
public class Client : BaseModel
{
    /// <summary>
    /// gets or sets the unique identifier
    /// </summary>
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// gets or sets the full name
    /// </summary>
    [Column("full_name")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the phone number
    /// </summary>
    [Column("phone")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the email address
    /// </summary>
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the registration date
    /// </summary>
    [Column("registration_date")]
    public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// gets or sets the creation timestamp
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
