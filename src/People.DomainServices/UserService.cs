namespace People.DomainServices;

/// <summary>
/// User service.
/// </summary>
public class UserService
{
    /// <summary>
    /// Creates Gravatar img URL for given email.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <returns>URL.</returns>
    public static string CreateGravatarUrl(string email)
    {
        return $"http://www.gravatar.com/avatar/{HashService.CreateMD5(email).ToLower()}";
    }
}
