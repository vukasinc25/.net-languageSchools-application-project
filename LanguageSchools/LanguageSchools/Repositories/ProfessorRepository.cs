using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;

namespace LanguageSchools.Repositories
{
    class ProfessorRepository : IProfessorRepository
    {
        public void Add(Professor professor)
        {
            Data.Instance.Professors.Add(professor);
            Data.Instance.Save();
        }

        public void Add(List<Professor> newProfessors)
        {
            Data.Instance.Professors.AddRange(newProfessors);
            Data.Instance.Save();
        }

        public void Set(List<Professor> newProfessors)
        {
            Data.Instance.Professors = newProfessors;
        }

        public void Delete(string email)
        {
            Professor professor = GetById(email);

            if (professor != null)
            {
                professor.User.IsActive = false;
            }

            Data.Instance.Save();
        }

        public List<Professor> GetAll()
        {
            return Data.Instance.Professors;
        }

        public Professor GetById(string email)
        {
            return Data.Instance.Professors.Find(u => u.User.Email == email);
        }

        public void Update(string email, Professor updatedProfessor)
        {
            Data.Instance.Save();
        }
    }
}
