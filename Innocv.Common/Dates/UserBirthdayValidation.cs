using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Innocv.Common.Dates
{
    /// <summary>
    /// The birthday of the user cannot be greater than current date
    /// and we don't accept persons more than 100 years.
    /// </summary>
    public class UserBirthdayValidation
    {
        public static ValidationResult Range(DateTime date)
        {
            if(date == DateTime.MinValue)
                return new ValidationResult("The birthday must be specified.");

            if (date.CompareTo(DateTime.Now.AddYears(-100)) < 0)
                return new ValidationResult("The birthday is more than 100 years ago.");

            if(date.CompareTo(DateTime.Now) > 0)
                return new ValidationResult("The birthday is after current day. It's not acceptable a future birthday.");

            return ValidationResult.Success;
        }
    }
}
