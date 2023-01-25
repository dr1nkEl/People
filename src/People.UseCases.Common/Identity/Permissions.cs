namespace People.UseCases.Common.Identity;

/// <summary>
/// Permissions in our application.
/// </summary>
public static class Permissions
{
    /// <summary>
    /// Add User claim value.
    /// </summary>
    public const string AddUser = "Add User";

    /// <summary>
    /// Generate reports claim value.
    /// </summary>
    public const string GenerateReports = "Generate Reports";

    /// <summary>
    /// Management claim value.
    /// </summary>
    public const string Management = "Management";

    /// <summary>
    /// Get all permissions in the app.
    /// </summary>
    /// <returns>Permissions enumerable.</returns>
    public static IEnumerable<string> GetAllPermissions()
    {
        var permissions = new List<string>()
        {
            AddUser,
            GenerateReports,
            Management
        };

        return permissions;
    }
}
