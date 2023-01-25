namespace People.Domain.Users.Entities;

/// <summary>
/// One of the possible selection options for an attribute.
/// </summary>
public class AttributeOption
{
    /// <summary>
    /// Option id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the option.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Id of the associated attribute.
    /// </summary>
    public int AttributeId { get; set; }

    /// <summary>
    /// Associated attribute.
    /// </summary>
    public UserAttribute Attribute { get; set; }
}
