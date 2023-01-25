namespace People.UseCases.Common.Dtos.Administration;

/// <summary>
/// Permission Dto.
/// </summary>
public record PermissionDto
{
    /// <summary>
    /// Permission name.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Permission status for role/user owner.
    /// </summary>
    public bool IsEnable { get; init; }
}
