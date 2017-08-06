namespace Clipt.Apis
{
    public class TextTransform
    {
        public static string TrimIndent(string text) => TextTransforms.TrimIndent.Transform(text);
    }
}