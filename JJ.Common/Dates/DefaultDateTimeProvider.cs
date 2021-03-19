using System;

namespace JJ.Common.Dates
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now.Date;
        }
    }
}