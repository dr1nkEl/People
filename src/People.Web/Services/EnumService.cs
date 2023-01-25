using People.Web.ViewModels;

namespace People.Web.Services;

/// <summary>
/// Enum service.
/// </summary>
public class EnumService
{
    /// <summary>
    /// Parse enum function.
    /// </summary>
    /// <typeparam name="T">Enum type to be parsed.</typeparam>
    /// <returns>Parsed enum values.</returns>
    public static IEnumerable<OptionViewModel> ParseEnum<T>() where T : struct, Enum
    {
        var values = Enum.GetValues<T>()
            .Select(value => new OptionViewModel
            {
                Text = value.ToString(),
                Value = Convert.ToInt32(value)
            });
        return values;
    }
}
