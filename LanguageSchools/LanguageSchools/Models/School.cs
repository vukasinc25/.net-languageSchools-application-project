using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Models
{
    public class School : ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }
        public object Clone()
        {
            return new School
            {   Id= Id, 
                Name = Name,
                Street = Street,
                StreetNumber = StreetNumber,
                City = City,
                Country = Country,
                IsActive = IsActive,
            };
        }
        public override string ToString()
        {
            return Name;
        }
        public string Error
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "Name cannot be empty!";
                }
                else if (string.IsNullOrEmpty(Street))
                {
                    return "Street cannot be empty!";
                }
                else if (string.IsNullOrEmpty(StreetNumber))
                {
                    return "StreetNumber name cannot be empty!";
                }
                else if (string.IsNullOrEmpty(City))
                {
                    return "City name cannot be empty!";
                }
                else if (string.IsNullOrEmpty(Country))
                {
                    return "Country cannot be empty!";
                }

                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
                if (columnName == "Name" && string.IsNullOrEmpty(Name))
                {
                    IsValid = false;
                    return "Name cannot be empty!";
                }
                else if (columnName == "Street" && string.IsNullOrEmpty(Street))
                {
                    IsValid = false;
                    return "Street cannot be empty!";
                }
                else if (columnName == "StreetNumber" && string.IsNullOrEmpty(StreetNumber))
                {
                    IsValid = false;
                    return "StreetNumber name cannot be empty!";
                }
                else if (columnName == "City" && string.IsNullOrEmpty(City))
                {
                    IsValid = false;
                    return "City name cannot be empty!";
                }
                else if (columnName == "Country" && string.IsNullOrEmpty(Country))
                {
                    IsValid = false;
                    return "Country cannot be empty!";
                }

                return "";
            }
        }
    }
}
