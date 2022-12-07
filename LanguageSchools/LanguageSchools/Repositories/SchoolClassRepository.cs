using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Repositories
{
    class SchoolClassRepository : ISchoolClassRepository
    {
        public void Add(SchoolClass schoolClass)
        {
            Data.Instance.SchoolClasses.Add(schoolClass);
            Data.Instance.Save();
        }
        public void Add(List<SchoolClass> newSchoolClass)
        {
            Data.Instance.SchoolClasses.AddRange(newSchoolClass);
            Data.Instance.Save();
        }
        public void Delete(int id)
        {
            SchoolClass newSchoolClass = GetById(id);

            if (newSchoolClass != null)
            {
                newSchoolClass.IsActive = false;
            }

            Data.Instance.Save();
        }
        public List<SchoolClass> GetAll()
        {
            return Data.Instance.SchoolClasses;
        }

        public SchoolClass GetById(int id)
        {
            return Data.Instance.SchoolClasses.Find(u => u.Id.Equals(id));
        }
        public void Set(List<SchoolClass> newSchoolClass)
        {
            Data.Instance.SchoolClasses = newSchoolClass;
        }
        public int nextId(List<SchoolClass> listClasses)
        {
            int nextId = 0;
            for (int i = 0; i <= listClasses.Count; i++)
            {
                if (nextId < i)
                {
                    nextId = i;
                }
            }
            nextId += 1;
            return nextId;
        }
        public void Update(int id, SchoolClass classs)
        {   
            SchoolClass schoolClass = GetById(id);
            if (schoolClass != null)
            {
                schoolClass.Id = classs.Id;
                schoolClass.Name = classs.Name;
                schoolClass.Date = classs.Date;
                schoolClass.StartTime = classs.StartTime;
                schoolClass.Duration = classs.Duration;
                schoolClass.State = classs.State;
            }
            Data.Instance.Save();

        }
    }
}
