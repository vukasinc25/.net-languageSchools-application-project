using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Models
{
    public class Language : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private User professor;
        public User Professor
        {
            get => professor;
            set
            {
                professor = value;
                ProfessorId = professor?.Id;
            }
        }
        public int? ProfessorId { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public object Clone()
        {
            return new Language
            {
                Id = Id,
                Name = Name,
                Professor = Professor?.Clone() as User
            };
        }
    }
}
