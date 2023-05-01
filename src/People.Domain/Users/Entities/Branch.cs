namespace People.Domain.Users.Entities;

/// <summary>
/// Information about a company branch / office.
/// </summary>
public class Branch
{
    /// <summary>
    /// Id of the branch.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the branch.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Id of the user who is head of the branch.
    /// </summary>
    public int? DirectorId { get; set; }

    /// <summary>
    /// Director of the branch.
    /// </summary>
    public User Director { get; set; }
}
