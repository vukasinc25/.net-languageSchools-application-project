using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;
using LanguageSchools.CustomExceptions;
using LanguageSchools.Repositories;

namespace LanguageSchools.Services
{
    class UserService : IUserService
    {
        private IUserRepository repository;

        public UserService()
        {
            repository = new UserRepository();
        }

        public List<User> GetActiveUsers()
        {
            return repository.GetAll().Where(p => p.IsActive).ToList();
        }

        public List<User> GetAll()
        {
            return repository.GetAll();
        }

        public void Add(User user)
        {
            repository.Add(user);
        }

        public void Update(int id, User user)
        {
            repository.Update(id, user);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
