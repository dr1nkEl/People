namespace People.Web.ViewModels;

/// <summary>
/// Contains data for displaying an error information to user.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// Id of the request.
    /// </summary>
    public string RequestId { get; set; }

    /// <summary>
    /// Indicatesi f request id should be displayed to user.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
