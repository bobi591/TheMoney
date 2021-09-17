using System;
using System.Text.RegularExpressions;

namespace TheMoney.Shared.Entities.Validators
{
    public static class CamelCaseToSentenceCase
    {
        public static string Convert(string camelCaseText)
        {
            string output = Regex.Replace(camelCaseText, @"\p{Lu}", m => " " + m.Value.ToLowerInvariant());
            return char.ToUpperInvariant(output[0]) + output.Substring(1);
        }
    }
}
