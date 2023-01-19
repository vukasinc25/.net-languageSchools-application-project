using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    interface ILanguageService
    {
        List<Language> GetAll();
        List<Language> GetActiveLanguages();
        void Add(Language language);
        void Update(int id, Language language);
        void Delete(int id);
    }
}
