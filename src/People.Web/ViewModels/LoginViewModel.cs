using System.ComponentModel.DataAnnotations;

namespace People.Web.ViewModels;

/// <summary>
/// View model for login page.
/// </summary>
public record LoginViewModel
{
    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}
