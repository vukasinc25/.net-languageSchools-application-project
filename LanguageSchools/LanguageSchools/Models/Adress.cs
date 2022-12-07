using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Models
{
    [Serializable]
    public class Adress: ICloneable
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; } 

        public object Clone()
        {
            return new Adress
            {
                Id = Id,
                Street = Street,
                StreetNumber = StreetNumber,
                City = City,
                Country = Country,
                IsActive = IsActive
            };
        }

        public override string ToString()
        {
            return $"{Street} {StreetNumber}, {City}, {Country}";
        }
    }
}
