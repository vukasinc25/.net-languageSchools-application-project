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
        List<SchoolClass> GetActiveSchoolClasses();
        void Add(SchoolClass schoolClass);
        void Update(int id, SchoolClass schoolClass);
        void Delete(int id);
    }
}
