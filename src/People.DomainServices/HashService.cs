namespace People.DomainServices;

/// <summary>
/// Hash service.
/// </summary>
public class HashService
{
    /// <summary>
    /// Create MD5 hash string.
    /// </summary>
    /// <param name="input">Input.</param>
    /// <returns>MD5 hash.</returns>
    public static string CreateMD5(string input)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();

        var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}
