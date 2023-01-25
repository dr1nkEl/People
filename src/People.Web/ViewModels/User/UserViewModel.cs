namespace People.Web.ViewModels.User;

/// <inheritdoc cref="Domain.Users.Entities.User"/>
public record UserViewModel
{
    /// <summary>
    ///  Id.
    /// </summary>
    public int Id { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.FirstName"/>
    public string FirstName { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.LastName"/>
    public string LastName { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.FullName"/>
    public string FullName => Saritasa.Tools.Common.Utils.StringUtils.JoinIgnoreEmpty(" ", FirstName, LastName);

    /// <inheritdoc cref="Domain.Users.Entities.User.BranchId"/>
    public int BranchId { get; init; }

    /// <inheritdoc cref="Domain.Users.Entities.User.Branch"/>
    public BranchViewModel Branch { get; init; }

    /// <summary>
    /// Avatar.
    /// </summary>
    public string Avatar { get; init; }
}
