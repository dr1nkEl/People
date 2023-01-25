namespace People.Domain.Reviews.Entities;

/// <summary>
/// Specifies review type, like annual, semi-annual, 30-day, etc.
/// </summary>
public class ReviewType
{
    /// <summary>
    /// Id of the type.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the type.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Interval.
    /// </summary>
    public Interval? Interval { get; set; }

    /// <summary>
    /// Interval amount.
    /// </summary>
    public int? IntervalAmount { get; set; }
}
