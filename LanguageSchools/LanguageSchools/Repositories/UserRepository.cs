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
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace LanguageSchools.Repositories
{
    class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }

        public int Add(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Users (Email, Password, FirstName, LastName, Jmbg, Gender, UserType, IsActive, Street, StreetNumber, City, Country)
                    output inserted.Id
                    values (@Email, @Password, @FirstName, @LastName, @Jmbg, @Gender, @UserType, @IsActive, @Street, @StreetNumber, @City, @Country)";

                command.Parameters.Add(new SqlParameter("Email", user.Email));
                command.Parameters.Add(new SqlParameter("Password", user.Password));
                command.Parameters.Add(new SqlParameter("FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("Jmbg", user.JMBG));
                command.Parameters.Add(new SqlParameter("Gender", user.Gender));
                command.Parameters.Add(new SqlParameter("UserType", user.UserType));
                command.Parameters.Add(new SqlParameter("IsActive", user.IsActive));
                command.Parameters.Add(new SqlParameter("Street", user.Street));
                command.Parameters.Add(new SqlParameter("StreetNumber", user.StreetNumber));
                command.Parameters.Add(new SqlParameter("City", user.City));
                command.Parameters.Add(new SqlParameter("Country", user.Country));

                return (int)command.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Users set IsActive=0 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Users";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Users");

                foreach (DataRow row in ds.Tables["Users"].Rows)
                {
                    var user = new User
                    {
                        Id = (int)row["Id"],
                        FirstName = row["FirstName"] as string,
                        LastName = row["LastName"] as string,
                        Email = row["Email"] as string,
                        Password = row["Password"] as string,
                        JMBG = row["Jmbg"] as string,
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"] as string),
                        UserType = (EUserType)Enum.Parse(typeof(EUserType), row["UserType"] as string),
                        IsActive = (bool)row["IsActive"],
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string,
                    };

                    users.Add(user);
                }
            }

            return users;
        }

        public User GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Users where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Users");
                if (ds.Tables["Users"].Rows.Count > 0)
                {
                    var row = ds.Tables["Users"].Rows[0];

                    var user = new User
                    {
                        Id = (int)row["Id"],
                        FirstName = row["FirstName"] as string,
                        LastName = row["LastName"] as string,
                        Email = row["Email"] as string,
                        Password = row["Password"] as string,
                        JMBG = row["Jmbg"] as string,
                        Gender = (EGender)Enum.Parse(typeof(EGender), row["Gender"] as string),
                        UserType = (EUserType)Enum.Parse(typeof(EUserType), row["UserType"] as string),
                        IsActive = (bool)row["IsActive"],
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string,
                    };

                    return user;
                }
            }

            return null;
        }

        public void Update(int id, User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Users set 
                        FirstName = @FirstName,
                        LastName = @LastName,
                        Password = @Password,
                        Gender = @Gender,
                        UserType = @UserType,
                        Street = @Street,
                        StreetNumber = @StreetNumber,
                        City = @City,
                        Country = @Country
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("Password", user.Password));
                command.Parameters.Add(new SqlParameter("Gender", user.Gender));
                command.Parameters.Add(new SqlParameter("UserType", user.UserType));
                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("Street", user.Street));
                command.Parameters.Add(new SqlParameter("StreetNumber", user.StreetNumber));
                command.Parameters.Add(new SqlParameter("City", user.City));
                command.Parameters.Add(new SqlParameter("Country", user.Country));

                command.ExecuteScalar();
            }
        }
    }
}
