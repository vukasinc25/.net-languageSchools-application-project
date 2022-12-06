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
        List<Address> GetAll();
        Address GetById(int id);
        void Add(Address address);
        void Add(List<Address> address);
        void Set(List<Address> address);
        void Update(int id, Address address);
        void Delete(int id);
    }
}
