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
        public void Update(int code, SchoolClass newSchoolClass)
        {
            Data.Instance.Save();
        }
    }
}
