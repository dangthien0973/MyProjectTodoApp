using System.Globalization;

namespace APIToDoListV1.Utils
{
    public class ExteptionUtils : Exception
    {
        public ExteptionUtils() : base() { }

        public ExteptionUtils(string message) : base(message) { }

        public ExteptionUtils(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
