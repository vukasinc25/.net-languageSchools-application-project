using LanguageSchools.Models;
using LanguageSchools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    class LanguageService : ILanguageService
    {
        private ILanguageRepository repository;
        public LanguageService()
        {
            repository = new LanguageRepository();
        }
        public List<Language> GetActiveLanguages()
        {
            return repository.GetAll().Where(p => p.IsActive).ToList();
        }

        public List<Language> GetAll()
        {
            return repository.GetAll();
        }

        public void Add(Language language)
        {
            repository.Add(language);
        }

        public void Update(int id, Language language)
        {
            repository.Update(id, language);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
