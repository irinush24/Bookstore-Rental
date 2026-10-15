using System.Globalization;
using System.Text;

namespace Capstone.Core.Tools;

public class MySearchMatcher
{
    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }

    public static Boolean Matches(string text, string query)
    {

        if (text == null)
            return false;

        if (query == null)
            throw new ArgumentNullException("Cannot search using a null query");

        if (string.IsNullOrWhiteSpace(query))
            return true;

        query = MySearchMatcher.RemoveDiacritics (query).ToLower();
        text = MySearchMatcher.RemoveDiacritics(text).ToLower();

        List<string> queryTerms = query.Split(' ').ToList();

        foreach (var term in queryTerms)
        {
            if (!text.Contains(term))
                return false;
        }
        
        return true;
    }
}
