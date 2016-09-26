
// Author     : Atuki Yoshinaga

using System.Collections;

namespace AkiVACO
{

namespace XString
{
    public static class StringExtensions
    {
        /// <summary>
        /// 指定された System.String オブジェクトが null または System.String.Empty 文字列であるかどうかを示します。
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 指定した System.String への参照を取得します。
        /// </summary>
        public static string IsInterned(this string str)
        {
            return string.IsInterned(str);
        }

        public static string Formats(this string format, params object[] values)
        {
            return string.Format(format, values);
        }

    }

}   // End of namespace XUnityString
}   // End of namespace AkiVACO