namespace People.Domain.Users.Entities;

/// <summary>
/// Contains information about a compensation for a user.
/// </summary>
public class Compensation
{
    /// <summary>
    /// Id of the compensation.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the associated user.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Associated user.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Compensation value.
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Period of the compensation.
    /// </summary>
    public CompensationPeriod Period { get; set; }

    /// <summary>
    /// Id of the currency.
    /// </summary>
    public int CurrencyId { get; set; }

    /// <summary>
    /// Currency for the <see cref="Value"/>.
    /// </summary>
    public Currency Currency { get; set; }

    /// <summary>
    /// Compensation start effective date.
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Id of the user who has created this compensation.
    /// </summary>
    public int CreatedById { get; set; }

    /// <summary>
    /// User who has created this compensation.
    /// </summary>
    public User CreatedBy { get; set; }

    /// <summary>
    /// Additional comments.
    /// </summary>
    public string Comments { get; set; }
}
