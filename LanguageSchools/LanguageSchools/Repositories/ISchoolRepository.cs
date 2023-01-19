using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Repositories
{
    internal interface ISchoolRepository
    {
        List<School> GetAll();
        School GetById(int id);
        int Add(School school);
        void Update(int id, School school);
        void Delete(int id);
    }
}
