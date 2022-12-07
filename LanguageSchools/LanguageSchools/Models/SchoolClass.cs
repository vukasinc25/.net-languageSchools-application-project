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
        //public Professor Professor { get; set; }
        //public Student Student { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public EState State { get; set; }
        public bool IsReserved { get; set; }
        public bool IsActive { get; set; }

        public object Clone()
        {
            return new SchoolClass
            {
                Id = Id,
                Name = Name,
                //Professor = Professor?.Clone() as Professor,
                //Student = Student?.Clone() as Student,
                StartTime = StartTime,
                Duration = Duration,
                Date = Date,
                State = State,
                IsReserved = IsReserved
            };
        }
    }
}
