using System;

namespace Innocv.Common.Dates
{
    public class FixedDateTimeProvider : IDateTimeProvider
    {
        private DateTime _fixedDateTime;


        public FixedDateTimeProvider(DateTime fixedDateTime)
        {
            _fixedDateTime = fixedDateTime;
        }

        public DateTime GetCurrentDate()
        {
            return _fixedDateTime;
        }
    }
}