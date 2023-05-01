using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace People.Domain.Users.Entities;

/// <summary>
/// Custom application user entity.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// First name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Full name, concat of first name and last name.
    /// </summary>
    public string FullName => Saritasa.Tools.Common.Utils.StringUtils.JoinIgnoreEmpty(" ", FirstName, LastName);

    /// <summary>
    /// The date when user last logged in.
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Last token reset date. Before the date all generate login tokens are considered
    /// not valid. Must be in UTC format.
    /// </summary>
    public DateTime LastTokenResetAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Person's birthday.
    /// </summary>
    public DateOnly? Birthday { get; set; }

    /// <summary>
    /// Id of the branch this user belongs to.
    /// </summary>
    public int BranchId { get; set; }

    /// <summary>
    /// Branch this user belongs to.
    /// </summary>
    public Branch Branch { get; set; }

    /// <summary>
    /// If this user is inactive, specifies the date at which he was deactivated.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// User's attributes.
    /// </summary>
    public ICollection<AttributeValue> Attributes { get; set; } = new List<AttributeValue>();

    /// <summary>
    /// List of positions user takes.
    /// </summary>
    public ICollection<Position> Positions { get; set; } = new List<Position>();

    /// <summary>
    /// Compensations assigned to the user.
    /// </summary>
    public ICollection<Compensation> Compensations { get; set; } = new List<Compensation>();

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj == null || obj is not User)
        {
            return false;
        }

        var user = (User)obj;
        var compareResult = (this.FirstName == user.FirstName)
            && (this.LastName == user.LastName)
            && (this.Email == user.Email)
            && (this.BranchId == user.BranchId)
            && (this.Birthday == user.Birthday);
        return compareResult;
    }

    /// <summary>
    /// Copying main fields from provided user into current.
    /// </summary>
    /// <param name="source">Source user.</param>
    public void CopyFields(User source)
    {
        FirstName = source.FirstName;
        LastName = source.LastName;
        Birthday = source.Birthday;
        BranchId = source.BranchId;
        Email = source.Email;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, Birthday, BranchId, Email);
    }
}
