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
        public void Add(Adress adress)
        {
            Data.Instance.Adresses.Add(adress);
            Data.Instance.Save();
        }
        public void Add(List<Adress> newAdress)
        {
            Data.Instance.Adresses.AddRange(newAdress);
            Data.Instance.Save();
        }
        public void Delete(int id)
        {
            Adress address = GetById(id);

            if (address != null)
            {
                address.IsActive = false;
            }

            Data.Instance.Save();
        }
        public List<Adress> GetAll()
        {
            //TODO :DDDDDDDDD
            throw new NotImplementedException();
        }
        public Adress GetById(int id)
        {
            //NOTTODO :D:D::D
            throw new NotImplementedException();
        }
        public void Set(List<Adress> address)
        {
            //TODO:D:D::D
            throw new NotImplementedException();
        }
        public void Update(int id, Adress address)
        {
            //TODO :!:!:!:
            throw new NotImplementedException();
        }
    }
}
