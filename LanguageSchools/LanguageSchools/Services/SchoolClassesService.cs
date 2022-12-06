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
        private ISchoolClassRepository schoolClassRepository;
        public SchoolClassesService()
        {
            schoolClassRepository= new SchoolClassRepository();
        }
        public void Add(SchoolClass schoolClass)
        {
            schoolClassRepository.Add(schoolClass);
        }
        public void Delete(int id)
        {
            schoolClassRepository.Delete(id);
        }
        public List<SchoolClass> GetActiveClasses()
        {
            return schoolClassRepository.GetAll().Where(p =>p.IsActive).ToList();
        }
        public List<SchoolClass> GetActiveClassesById(int id)
        {
            throw new NotImplementedException();
            //TODO :D:D:D
        }
        public List<SchoolClass> GetAll()
        {
            return schoolClassRepository.GetAll();
        }
        public SchoolClass GetById(int id)
        {
            return schoolClassRepository.GetById(id);
        }
        public List<SchoolClass> ListAllClasses()
        {
            throw new NotImplementedException();
            //TODO :D:D:D:DD
        }
        public void Set(List<SchoolClass> schoolClass)
        {
            schoolClassRepository.Set(schoolClass);
        }
        public void Update(int id, SchoolClass schoolClass)
        {
            schoolClassRepository.Update(id, schoolClass);
        }
    }
}
