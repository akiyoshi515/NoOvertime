
// Author     : Atuki Yoshinaga

using System.Collections;

namespace AkiVACO
{

namespace XUnityString
{
    public class XColors
    {
        public const string AQUA = "aqua";
        public const string BLACK = "black";
        public const string BLUE = "blue";
        public const string BROWN = "brown";
        public const string CYAN = "cyan";
        public const string DARKBLUE = "darkblue";
        public const string FUCHSIA = "fuchsia";
        public const string GREEN = "green";
        public const string GREY = "grey";
        public const string LIGHTBLUE = "lightblue";
        public const string LIME = "lime";
        public const string MAGENTA = "magenta";
        public const string MAROON = "maroon";
        public const string NAVY = "navy";
        public const string OLIVE = "olive";
        public const string ORANGE = "orange";
        public const string PURPLE = "purple";
        public const string RED = "red";
        public const string SILVER = "silver";
        public const string TEAL = "teal";
        public const string WHITE = "white";
        public const string YELLOW = "yellow";
    }

    public class RichStringInfo
    {
        private string m_head = "";
        private string m_tail = "";

        public RichStringInfo()
        {
            Clear();
        }

        public RichStringInfo(RichStringInfo info)
        {
            this.m_head = info.m_head;
            this.m_tail = info.m_tail;
        }

        public void Clear()
        {
            m_head = "";
            m_tail = "";
        }

        public void Coloring(string color)
        {
            m_head = string.Format("<color={0}>", color) + m_head;
            m_tail = m_tail + "</color>";
        }

        public void Resize(int size)
        {
            m_head = string.Format("<size={0}>", size) + m_head;
            m_tail = m_tail + "</size>";
        }
        public void Medium()
        {
            this.Resize(11);
        }
        public void Small()
        {
            this.Resize(9);
        }
        public void Large()
        {
            this.Resize(16);
        }

        public void Bold()
        {
            m_head = string.Format("<b>") + m_head;
            m_tail = m_tail + "</b>";
        }
        public void Italic()
        {
            m_head = string.Format("<i>") + m_head;
            m_tail = m_tail + "</i>";
        }

        public string Collection(string msg)
        { 
            return (m_head + msg + m_tail);
        }

    }

    public static class UnityStringExtensions
    {
        public static string Coloring(this string str, string color)
        {
            return string.Format("<color={0}>{1}</color>", color, str);
        }
        public static string Red(this string str)
        {
            return str.Coloring("red");
        }
        public static string Green(this string str)
        {
            return str.Coloring("green");
        }
        public static string Blue(this string str)
        {
            return str.Coloring("blue");
        }

        public static string Resize(this string str, int size)
        {
            return string.Format("<size={0}>{1}</size>", size, str);
        }
        public static string Medium(this string str)
        {
            return str.Resize(11);
        }
        public static string Small(this string str)
        {
            return str.Resize(9);
        }
        public static string Large(this string str)
        {
            return str.Resize(16);
        }

        public static string Bold(this string str)
        {
            return string.Format("<b>{0}</b>", str);
        }
        public static string Italic(this string str)
        {
            return string.Format("<i>{0}</i>", str);
        }
    }

}
}