using System.Text;

namespace Demo_shopping.Helpers
{
    public static class StringExtensions
    {
        public static string ToSlug(this string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalized)
            {
                if (char.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            string result = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            result = result.ToLower();
            result = result.Replace(" ", "-");
            result = System.Text.RegularExpressions.Regex.Replace(result, @"[^a-z0-9\-]", "");

            return result;
        }
    }
}
