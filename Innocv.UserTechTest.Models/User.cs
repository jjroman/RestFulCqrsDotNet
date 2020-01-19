using System;
using System.ComponentModel.DataAnnotations;
using Innocv.Common;
using Innocv.Common.Dates;

namespace Innocv.UserTechTest.Models
{
    public class User
    {
        // This constuctor is only used for EF.
        protected User()
        {

        }

        // I prefer to use factories to create objects in an consistent way.
        // Constructor are unnamed methods and for that reason we don't know
        // if to create a object is for one purpose or another.
        // In the current/simple case, I'm creating a user to get its age.
        // So, I'm sure that I'll be able to calculate its age.
        // This is a feature in DDD.
        // Also, I could implement some logic to compare this object with others
        // but I am saving time to follow the principle KISS (Keep It Simply Stupid)
        // and to not implement more of what I need and not to complex the code.
        public static User CreateForGettingAge(string name, DateTime birthdate)
        {
            User user = new User()
            {
                Name = name,
                Birthdate = birthdate
            };

            return user;
        }

        public static User CreateForUpdating(int id, string name, DateTime birthdate)
        {
            User user = new User()
            {
                Id = id,
                Name = name,
                Birthdate = birthdate
            };

            return user;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        public int GetAge(IDateTimeProvider dateTimeProvider)
        {
            var zeroTime = new DateTime(1, 1, 1);
            var span = dateTimeProvider.GetCurrentDate() - Birthdate;
            var years = (zeroTime + span).Year - 1;

            return years;
        }
    }
}