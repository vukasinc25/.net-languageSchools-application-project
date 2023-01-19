using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;

namespace LanguageSchools.Services
{
    interface IUserService
    {
        List<User> GetAll();
        List<User> GetActiveUsers();
        void Add(User user);
        void Update(int id, User user);
        void Delete(int id);
    }
}
