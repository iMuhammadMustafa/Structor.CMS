using System.Globalization;

namespace PostsService.Infrastructure.Extentions;

public static class StringExtension
{
    public static string ToTitleCase(this string value)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
    }
}
