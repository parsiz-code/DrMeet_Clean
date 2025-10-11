using System.Text;
using System.Text.RegularExpressions;

namespace DrMeet.Api.Shared.Extensions;

public static class StringExtensions
{

    public static bool HasValue(this string? str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    public static string ValidateOrGenerateHex24(this string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length != 24 ||
            !Regex.IsMatch(input, "^[a-fA-F0-9]{24}$"))
        {
            // تولید رشته‌ی رندوم هگزادسیمال
            var random = new Random();
            var sb = new StringBuilder();
            const string hexChars = "abcdef0123456789";

            for (int i = 0; i < 24; i++)
            {
                sb.Append(hexChars[random.Next(hexChars.Length)]);
            }

            return sb.ToString(); // خروجی جایگزین معتبر
        }

        return input; // اگر معتبر بود، همون ورودی رو برمی‌گردونه
    }
    public static string CalculateReadTime(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "0:00";

        int textLength = text.Length;
        double rawMinutes = (double)textLength / 200;

        int minutes = (int)rawMinutes;
        int seconds = (int)Math.Round((rawMinutes - minutes) * 60);

        return $"{minutes}:{seconds:D2}";
    }
    public static string TrimEdges(this string input)
    {
        if (string.IsNullOrEmpty(input) || input.Length < 2)
            return string.Empty;

        return input.Substring(1, input.Length - 2);
    }

}