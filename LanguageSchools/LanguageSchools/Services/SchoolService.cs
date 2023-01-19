using LanguageSchools.Models;
using LanguageSchools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    class SchoolService : ISchoolService
    {
        private ISchoolRepository repository;

        public SchoolService()
        {
            repository = new SchoolRepository();
        }

        public List<School> GetActiveSchools()
        {
            return repository.GetAll().Where(p => p.IsActive).ToList();
        }

        public List<School> GetAll()
        {
            return repository.GetAll();
        }

        public void Add(School school)
        {
            repository.Add(school);
        }

        public void Update(int id, School school)
        {
            repository.Update(id, school);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
