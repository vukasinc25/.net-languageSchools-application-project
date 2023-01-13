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
        User GetById(int id);
        int Add(User user);
        void Update(int id, User user);
        void Delete(int id);
    }
}
