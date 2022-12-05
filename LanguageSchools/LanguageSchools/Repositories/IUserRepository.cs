using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;

namespace LanguageSchools.Repositories
{
    interface IUserRepository
    {
        List<User> GetAll();
        User GetById(string email);
        void Add(User user);
        void Add(List<User> users);
        void Set(List<User> users);
        void Update(string email, User user);
        void Delete(string email);
    }
}
