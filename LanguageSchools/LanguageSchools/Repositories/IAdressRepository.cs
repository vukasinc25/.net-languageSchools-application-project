using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Repositories
{
    internal interface IAdressRepository
    {
        List<Adress> GetAll();
        Adress GetById(int id);
        void Add(Adress address);
        void Add(List<Adress> address);
        void Set(List<Adress> address);
        void Update(int id, Adress address);
        void Delete(int id);
    }
}
