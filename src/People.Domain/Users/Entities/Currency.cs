namespace People.Domain.Users.Entities;

/// <summary>
/// Contains information about a currency.
/// </summary>
public class Currency
{
    /// <summary>
    /// Id of the currency.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Currency name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Short currency symbol.
    /// </summary>
    public string Symbol { get; set; }
}
