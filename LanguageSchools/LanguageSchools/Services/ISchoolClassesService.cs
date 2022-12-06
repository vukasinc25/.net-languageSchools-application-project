using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    interface ISchoolClassesService
    {
        List<SchoolClass> GetAll();
        SchoolClass GetById(int id);
        List<SchoolClass> GetActiveClasses();
        List<SchoolClass> GetActiveClassesById(int id);
        void Add(SchoolClass schoolClass);
        void Set(List<SchoolClass> schoolClass);
        void Update(int id, SchoolClass schoolClass);
        void Delete(int id);
        List<SchoolClass> ListAllClasses();
    }
}
