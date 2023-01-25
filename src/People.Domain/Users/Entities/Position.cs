namespace People.Domain.Users.Entities;

/// <summary>
/// Position of an employee in the company.
/// Essentially it means what person does in the company.
/// </summary>
public class Position
{
    /// <summary>
    /// Id of the position.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Posotion name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Parent position.
    /// </summary>
    public Position ParentPosition { get; set; }

    /// <summary>
    /// List of child positions.
    /// </summary>
    public ICollection<Position> ChildPositions { get; set; } = new List<Position>();

    /// <summary>
    /// List of users within this position.
    /// </summary>
    public ICollection<User> Users { get; set; } = new List<User>();
}
