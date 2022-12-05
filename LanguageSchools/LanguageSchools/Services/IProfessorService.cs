using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;

namespace LanguageSchools.Services
{
    interface IProfessorService
    {
        List<Professor> GetAll();
        Professor GetById(string email);
        List<Professor> GetActiveProfessors();
        List<Professor> GetActiveProfessorsByEmail(string email);
        List<Professor> GetActiveProfessorsOrderedByEmail();
        void Add(Professor professor);
        void Set(List<Professor> professors);
        void Update(string email, Professor professor);
        void Delete(string email);
        List<User> ListAllStudents();
    }
}
