using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Models
{
    [Serializable]
    public class User : ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public EGender Gender { get; set; }
        public EUserType UserType { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }
        private School school;
        public School School
        {
            get => school;
            set
            {
                school = value;
                SchoolId = school?.Id;
            }
        }
        public int? SchoolId { get; set; }

        public User()
        {
            IsActive = true;
        }

        public override string ToString()
        {
            return FirstName;
        }

        public object Clone()
        {
            return new User
            {
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                JMBG = JMBG,
                UserType = UserType,
                Gender = Gender,
                Street = Street,
                StreetNumber = StreetNumber,
                City = City,
                Country = Country,
                IsActive = IsActive,
                School = School?.Clone() as School
            };
        }
        public string Error
        {
            get
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return "Email cannot be empty!";
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    return "Password cannot be empty!";
                }
                else if (string.IsNullOrEmpty(FirstName))
                {
                    return "First name cannot be empty!";
                }
                else if (string.IsNullOrEmpty(LastName))
                {
                    return "Last name cannot be empty!";
                }
                else if (string.IsNullOrEmpty(JMBG))
                {
                    return "JMBG cannot be empty!";
                }

                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
                if (columnName == "Email" && string.IsNullOrEmpty(Email))
                {
                    IsValid = false;
                    return "Email cannot be empty!";
                }
                else if (columnName == "Password" && string.IsNullOrEmpty(Password))
                {
                    IsValid = false;
                    return "Password cannot be empty!";
                }
                else if (columnName == "FirstName" && string.IsNullOrEmpty(FirstName))
                {
                    IsValid = false;
                    return "First name cannot be empty!";
                }
                else if (columnName == "LastName" && string.IsNullOrEmpty(LastName))
                {
                    IsValid = false;
                    return "Last name cannot be empty!";
                }
                else if (columnName == "JMBG" && string.IsNullOrEmpty(JMBG))
                {
                    IsValid = false;
                    return "JMBG cannot be empty!";
                }

                return "";
            }
        }
    }
}
