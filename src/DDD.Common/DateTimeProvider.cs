using System;

namespace DDD.Common
{

    public static class DateTimeProvider
    {
        private static Func<DateTime> _func;

        public static DateTime Now
        {
            get { return _func(); }
        }

        public static void Init(Func<DateTime> func)
        {
            _func = func;
        }
    }


}
