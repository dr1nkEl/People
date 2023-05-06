using People.Domain.Users.Entities;

namespace People.Web.ViewModels.User;

/// <summary>
/// Create user view model.
/// </summary>
public record CreateUserViewModel
{
    /// <summary>
    /// Email of user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// First name of user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Last name of user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Birthdate of user.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Password for user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Id of the branch user will be atached to.
    /// </summary>
    public int BranchId { get; set; }

    /// <summary>
    /// Branches.
    /// </summary>
    public IEnumerable<Branch> Branches { get; set; }
}
