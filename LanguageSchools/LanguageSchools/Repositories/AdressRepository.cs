using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Repositories
{
    class AdressRepository : IAdressRepository
    {
        public void Add(Address adress)
        {
            Data.Instance.Adresses.Add(adress);
            Data.Instance.Save();
        }
        public void Add(List<Address> newAdress)
        {
            Data.Instance.Adresses.AddRange(newAdress);
            Data.Instance.Save();
        }
        public void Delete(int id)
        {
            Address address = GetById(id);

            if (address != null)
            {
                address.IsActive = false;
            }

            Data.Instance.Save();
        }
        public List<Address> GetAll()
        {
            //TODO :DDDDDDDDD
            throw new NotImplementedException();
        }
        public Address GetById(int id)
        {
            //NOTTODO :D:D::D
            throw new NotImplementedException();
        }
        public void Set(List<Address> address)
        {
            //TODO:D:D::D
            throw new NotImplementedException();
        }
        public void Update(int id, Address address)
        {
            //TODO :!:!:!:
            throw new NotImplementedException();
        }
    }
}
