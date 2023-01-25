namespace People.Domain.Users.Entities;

/// <summary>
/// Contains information for granually specifying hierarchy of people.
/// </summary>
public class UserPositionHierarchy
{
    /// <summary>
    /// Id of the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the parent user.
    /// </summary>
    public int ParentUserId { get; set; }

    /// <summary>
    /// Parent user.
    /// </summary>
    public User ParentUser { get; set; }

    /// <summary>
    /// Id of the child user.
    /// </summary>
    public int ChildUserId { get; set; }

    /// <summary>
    /// Child user. He reports to the parent user.
    /// </summary>
    public User ChildUser { get; set; }
}
