using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LanguageSchools.Models
{
    [Serializable]
    public class SchoolClass : ICloneable, IDataErrorInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public EState State { get; set; }
        public bool IsActive { get; set; }
        public bool IsValid { get; set; }
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
        private User student;
        public User Student
        {
            get => student;
            set
            {
                student = value;
                StudentId = student?.Id;
            }
        }
        public int? StudentId { get; set; }
        public int? ProfessorId { get; set; }
        public object Clone()
        {
            return new SchoolClass
            {
                Id = Id,
                Date = Date,
                StartTime = StartTime,
                Duration = Duration,
                State = State,
                IsActive= IsActive,
                Professor = Professor?.Clone() as User,
                Student = Student?.Clone() as User
            };
        }
        public string Error
        {
            get
            {
                if (string.IsNullOrEmpty(Date + ""))
                {
                    return "Date cannot be empty!";
                }
                else if (string.IsNullOrEmpty(StartTime))
                {
                    return "StartTime cannot be empty!";
                }
                else if (string.IsNullOrEmpty(Duration))
                {
                    return "Duration cannot be empty!";
                }
                return "";
            }
        }

        public string this[string columnName]
        {

            get
            {
                IsValid = true;
                if (columnName == "Date" && string.IsNullOrEmpty(Date + ""))
                {
                    IsValid = false;
                    return "Date cannot be empty!";
                }
                else if (columnName == "StartTime" && string.IsNullOrEmpty(StartTime))
                {
                    IsValid = false;
                    return "StartTime cannot be empty!";
                }
                else if (columnName == "Duration" && string.IsNullOrEmpty(Duration))
                {
                    IsValid = false;
                    return "Duration name cannot be empty!";
                }

                return "";
            }
        }
    }
}
