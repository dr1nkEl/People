namespace People.Domain.Users.Entities;

/// <summary>
/// Specifies some custom information about an employee.
/// </summary>
public class UserAttribute
{
    /// <summary>
    /// Id of the attribute.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Attribute name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Type of the data that will be stored for the attribute.
    /// </summary>
    public AttributeType AttributeType { get; set; }

    /// <summary>
    /// Options available for selection for this attribute.
    /// Only used when <see cref="AttributeType"/> is dropdown.
    /// </summary>
    public ICollection<AttributeOption> AttributeOptions { get; set; } = new List<AttributeOption>();

    /// <summary>
    /// Roles that are allowed to modify the attribute value.
    /// </summary>
    public ICollection<AppIdentityRole> AllowEditRoles { get; set; } = new List<AppIdentityRole>();

    /// <summary>
    /// Indicates if user is allowed to modify their own attribute.
    /// </summary>
    public bool AllowEditSelf { get; set; }

    /// <summary>
    /// Roles that are allowed to view the attribute.
    /// </summary>
    public ICollection<AppIdentityRole> AllowViewRoles { get; set; } = new List<AppIdentityRole>();

    /// <summary>
    /// Indicates if user is allowed to see their own attribute.
    /// </summary>
    public bool AllowViewSelf { get; set; }

    /// <summary>
    /// All existing values for this attribute.
    /// </summary>
    public ICollection<AttributeValue> Values { get; set; } = new List<AttributeValue>();

    /// <summary>
    /// Deleted at.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}
