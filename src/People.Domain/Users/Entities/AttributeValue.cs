namespace People.Domain.Users.Entities;

/// <summary>
/// Contains value of an attribute.
/// </summary>
public class AttributeValue
{
    /// <summary>
    /// Id of the value.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the attribute.
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// Associated attribute.
    /// </summary>
    public UserAttribute Attribute { get; set; }

    /// <summary>
    /// Id of the associated user.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Associated user.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Stored attribute value.
    /// </summary>
    public string Value { get; set; }
}
