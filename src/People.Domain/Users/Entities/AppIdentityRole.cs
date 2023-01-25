using Microsoft.AspNetCore.Identity;

namespace People.Domain.Users.Entities;

/// <summary>
/// Custom application identity role.
/// </summary>
public class AppIdentityRole : IdentityRole<int>
{
    /// <summary>
    /// Id of inherited role.
    /// If user has the current role, it would mean that he also has an inherited role.
    /// </summary>
    public int? InheritedRoleId { get; set; }

    /// <summary>
    /// Inherited role.
    /// If user has the current role, it would mean that he also has an inherited role.
    /// </summary>
    public AppIdentityRole InheritedRole { get; set; }

    /// <summary>
    /// Id of the CRM role.
    /// When syncing data from CRM, users will automatically be granted the current role if they have the scpecified role in CRM.
    /// </summary>
    public string CrmRoleId { get; set; }

    /// <summary>
    /// Attributes that can be viewed by the current role.
    /// </summary>
    public ICollection<UserAttribute> ViewableAttributes { get; set; } = new List<UserAttribute>();

    /// <summary>
    /// Attributes that can be edited by the current role.
    /// </summary>
    public ICollection<UserAttribute> EditableAttributes { get; set; } = new List<UserAttribute>();
}
