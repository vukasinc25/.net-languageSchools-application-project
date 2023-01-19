using LanguageSchools.Models;
using LanguageSchools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    class SchoolClassesService : ISchoolClassesService
    {
        private ISchoolClassRepository repository;
        public SchoolClassesService()
        {
            repository = new SchoolClassRepository();
        }
        public List<SchoolClass> GetActiveSchoolClasses()
        {
            return repository.GetAll().Where(p => p.IsActive).ToList();
        }

        public List<SchoolClass> GetAll()
        {
            return repository.GetAll();
        }

        public void Add(SchoolClass schoolClass)
        {
            repository.Add(schoolClass);
        }

        public void Update(int id, SchoolClass schoolClass)
        {
            repository.Update(id, schoolClass);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
