using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Models
{
    //Škola – šifra, naziv,
    //adresa na kojoj se nalazi,
    //lista jezika koje je moguće pohađati
    public class School : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AdressId { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        private Language language { get; set; }
        public Language Language
        {
            get => language;
            set
            {
                language = value;
                LanguageId = language?.Id;
            }
        }
        public int? LanguageId { get; set; }
        public object Clone()
        {
            return new School
            {   Id= Id, 
                Name = Name,
                AdressId = AdressId,
                Street = Street,
                StreetNumber = StreetNumber,
                City = City,
                Country = Country,
                IsActive = IsActive,
                Language = Language
            };
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
