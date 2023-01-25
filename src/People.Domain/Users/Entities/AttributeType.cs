namespace People.Domain.Users.Entities;

/// <summary>
/// Specifies the type of data stored in attribute.
/// </summary>
public enum AttributeType
{
    /// <summary>
    /// Simple text value.
    /// </summary>
    Text = 0,

    /// <summary>
    /// Numeric value.
    /// </summary>
    Number = 1,

    /// <summary>
    /// Dropdown with a single option selected.
    /// </summary>
    DropDown = 2
}
