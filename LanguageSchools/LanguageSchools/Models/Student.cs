using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Models
{
    [Serializable]
    public class Student : ICloneable
    {
        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                UserId = user.Email;
            }
        }
        public string UserId { get; set; }
        public object Clone()
        {
            return new Student
            {
                User = User.Clone() as User
            };
        }
        public override string ToString()
        {
            return $"[Student] {User.FirstName}, {User.LastName}, {User.Email}";
        }
    }
}
