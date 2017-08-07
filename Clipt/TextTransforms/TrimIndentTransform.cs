using System;
using System.Linq;

namespace Clipt.TextTransforms
{
    public class TrimIndentTransform
    {
        public static string Transform(string text)
        {
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Find the minimum indent
            var minimumIndent = lines.Where(x => x.Trim().Length > 0).Select(x => x.Length - x.TrimStart().Length).Min();
            lines = lines.Select(x => x.Substring(Math.Min(minimumIndent, x.Length))).ToArray();

            text = string.Join(Environment.NewLine, lines);
            return text;
        }
    }
}