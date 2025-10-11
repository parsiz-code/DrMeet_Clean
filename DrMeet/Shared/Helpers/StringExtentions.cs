using static SkiaSharp.HarfBuzz.SKShaper;

namespace DrMeet.Api.Shared.Helpers
{
    public static class StringExtentions
    {
        public static string GetString(this List<string> LstStr) => string.Join(",", LstStr);
     
    }
}
