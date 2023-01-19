using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LanguageSchools.Models
{
    public class Language : ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public bool IsValid { get; set; }
        //private User professor;
        //public User Professor
        //{
        //    get => professor;
        //    set
        //    {
        //        professor = value;
        //        ProfessorId = professor?.Id;
        //    }
        //}
        //public int? ProfessorId { get; set; }
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
            };
        }
        public string Error
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "Name cannot be empty!";
                }
                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
                if (columnName == "Date" && string.IsNullOrEmpty(Name))
                {
                    IsValid = false;
                    return "Date cannot be empty!";
                }

                return "";
            }
        }
    }
}
