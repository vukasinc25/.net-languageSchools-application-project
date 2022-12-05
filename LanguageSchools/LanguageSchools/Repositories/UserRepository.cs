using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;
using LanguageSchools.CustomExceptions;

namespace LanguageSchools.Repositories
{
    class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }

        public void Add(User user)
        {
            Data.Instance.Users.Add(user);
            Data.Instance.Save();
        }

        public void Add(List<User> newUsers)
        {
            Data.Instance.Users.AddRange(newUsers);
            Data.Instance.Save();
        }

        public void Set(List<User> newUsers)
        {
            Data.Instance.Users = newUsers;
        }

        public void Delete(string email)
        {
            User user = GetById(email);

            if(user != null)
            {
                user.IsActive = false;
            }
            else
            {
                throw new UserNotFoundException();
            }

            Data.Instance.Save();
        }

        public List<User> GetAll()
        {
            return Data.Instance.Users;
        }

        public User GetById(string email)
        {
            return Data.Instance.Users.Find(u => u.Email == email);
        }

        public void Update(string email, User updatedUser)
        {
            User user = GetById(email);

            if(user != null)
            {
                user.Address = updatedUser.Address;
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Password = updatedUser.Password;
                user.Gender = updatedUser.Gender;
                user.UserType = updatedUser.UserType;
            }
            Data.Instance.Save();
        }
    }
}
