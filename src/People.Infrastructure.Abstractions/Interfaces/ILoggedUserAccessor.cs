namespace People.Infrastructure.Abstractions.Interfaces;

/// <summary>
/// Logged user accessor routines.
/// </summary>
public interface ILoggedUserAccessor
{
    /// <summary>
    /// Get current logged user identifier.
    /// </summary>
    /// <returns>Current user identifier.</returns>
    int GetCurrentUserId();

    /// <summary>
    /// Return true if there is any user authenticated.
    /// </summary>
    /// <returns>Returns <c>true</c> if there is authenticated user.</returns>
    bool IsAuthenticated();

    /// <summary>
    /// Check the user for the presence of a specific claim.
    /// </summary>
    /// <param name="claimTypes">Claim type.</param>
    /// <param name="claimValue">Claim value.</param>
    /// <returns>Returns <c>true</c> if user has specific claim.</returns>
    bool HasClaim(string claimTypes, string claimValue);
}
