namespace People.Web.ViewModels.User;

/// <summary>
/// Change password view model.
/// </summary>
public record ChangePasswordViewModel
{
    /// <summary>
    /// Old password of user.
    /// </summary>
    public string OldPassword { get; set; }

    /// <summary>
    /// New password of user to be set.
    /// </summary>
    public string NewPassword { get; set; }
}
