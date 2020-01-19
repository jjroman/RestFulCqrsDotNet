using System;

namespace Innocv.Common.Dates
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now.Date;
        }
    }
}