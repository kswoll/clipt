using Clipt.TextTransforms;

namespace Clipt.Utils
{
    public class TextUtils
    {
        public string TrimIndent(string text) => TrimIndentTransform.Transform(text);
    }
}