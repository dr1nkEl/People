namespace People.Domain.Users.Entities;

/// <summary>
/// Period for which compensation is calculated.
/// </summary>
public enum CompensationPeriod
{
    /// <summary>
    /// Compensation is the value per month.
    /// </summary>
    Monthly = 0,

    /// <summary>
    /// Compensation is the value per year.
    /// </summary>
    Yearly = 1,
}
