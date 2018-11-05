using System.Text;

namespace Randevy.Infrastructure.Extensions
{
    public static class BaseExt
    {
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static StringBuilder SetUrlDivider(this StringBuilder builder)
        {
            return builder.Length == 0 ? builder.Append("?") : builder.Append("&");
        }
    }
}