using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Models
{
    [Serializable]
    public class SchoolClass : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public EState State { get; set; }
        public bool IsActive { get; set; }
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
                Name = Name,
                StartTime = StartTime,
                Duration = Duration,
                Date = Date,
                State = State,
                IsActive= IsActive,
                Student = Student?.Clone() as User,
                Professor = Professor?.Clone() as User
            };
        }
    }
}
