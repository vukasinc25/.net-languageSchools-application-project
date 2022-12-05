using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;

namespace LanguageSchools.Repositories
{
    interface IProfessorRepository
    {
        List<Professor> GetAll();
        Professor GetById(string email);
        void Add(Professor professor);
        void Add(List<Professor> professors);
        void Set(List<Professor> professors);
        void Update(string email, Professor professor);
        void Delete(string email);
    }
}
