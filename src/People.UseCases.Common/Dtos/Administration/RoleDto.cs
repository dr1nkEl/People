namespace People.UseCases.Common.Dtos.Administration;

/// <summary>
/// Dto for role.
/// </summary>
public record RoleDto
{
    /// <summary>
    /// Role id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Role name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Permissions for this role.
    /// </summary>
    public IEnumerable<PermissionDto> Permissions { get; init; }
}
