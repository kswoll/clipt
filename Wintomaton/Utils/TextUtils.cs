using Wintomaton.TextTransforms;

namespace Wintomaton.Utils
{
    public class TextUtils
    {
        public string TrimIndent(string text) => TrimIndentTransform.Transform(text);
    }
}