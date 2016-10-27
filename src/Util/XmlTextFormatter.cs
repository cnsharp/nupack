namespace CnSharp.VisualStudio.NuPack.Util
{
    public class XmlTextFormatter
    {
        protected const string Blank = "&#x0020;";
        protected const string Tab = "&#x0009;";
        protected const string Enter = "&#x000D;";
        protected const string NewLine = "&#x000A;";

        protected static readonly string[] WhiteSpacesXmlCodes = {Blank, Tab, Enter, NewLine};
        protected static readonly string[] WhiteSpacesOriginalStrings = {" ", "\t", "\r", "\n"};

        public static string Encode(string text)
        {
            var i = 0;
            foreach (var s in WhiteSpacesOriginalStrings)
            {
                text = text.Replace(s, WhiteSpacesXmlCodes[i]);
                i ++;
            }
            return text;
        }

        public static string Decode(string text)
        {
            var i = 0;
            foreach (var s in WhiteSpacesXmlCodes)
            {
                text = text.Replace(s, WhiteSpacesOriginalStrings[i]);
                i++;
            }
            return text;
        }
    }
}