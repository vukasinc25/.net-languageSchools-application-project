using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    interface ISchoolService
    {
        List<School> GetAll();
        List<School> GetActiveSchools();
        void Add(School school);
        void Update(int id, School school);
        void Delete(int id);
    }
}
