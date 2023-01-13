using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Repositories
{
    interface ISchoolClassRepository
    {
        List<SchoolClass> GetAll();
        SchoolClass GetById(int id);
        int Add(SchoolClass schoolClass);
        void Update(int id, SchoolClass schoolClass);
        void Delete(int id);
    }
}
