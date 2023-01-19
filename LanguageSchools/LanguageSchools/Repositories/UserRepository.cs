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
        SchoolRepository schoolRepository = new SchoolRepository();
        LanguageRepository languageRepository= new LanguageRepository();
        public UserRepository()
        {
        }
        //public List<Language> GetAllLanguages()
        //{
        //    List<Language> languages = new List<Language>();

        //    using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
        //    {
        //        string commandText = "select * from Languages";
        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

        //        DataSet ds = new DataSet();

        //        dataAdapter.Fill(ds, "Languages");

        //        foreach (DataRow row in ds.Tables["Languages"].Rows)
        //        {
        //            var language = new Language
        //            {
        //                Id = (int)row["Id"],
                        
        //            };

        //            languages.Add(language);
        //        }
        //    }
        //}
        public void Add(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Users (Email, Password, FirstName, LastName, Jmbg, Gender, UserType, IsActive, Street, StreetNumber, City, Country, SchoolId)
                    output inserted.Id
                    values (@Email, @Password, @FirstName, @LastName, @Jmbg, @Gender, @UserType, @IsActive, @Street, @StreetNumber, @City, @Country, @SchoolId)";

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
                command.Parameters.Add(new SqlParameter("SchoolId", (object)user.SchoolId?? DBNull.Value));

                command.ExecuteScalar();

                if (user.UserType == EUserType.PROFESSOR)   
                {
                    int professorId2 = GetAll().Max(p => p.Id);
                    User professorId = GetById(professorId2);
                    foreach (Language language in user.Languages)
                    {
                        SqlCommand command2 = conn.CreateCommand();
                        command2.CommandText = @"
                            insert into ProfessorsLanguages (professorId, languageId)
                            values (@professorId, @languageId)";

                        command2.Parameters.Add(new SqlParameter("professorId", professorId.Id));
                        command2.Parameters.Add(new SqlParameter("languageId", language.Id));
                        command2.ExecuteScalar();
                    }
                }
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

                string commandText2 = "select * from ProfessorsLanguages";
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(commandText2, conn);
                DataSet ds2 = new DataSet();
                dataAdapter2.Fill(ds2, "ProfessorsLanguages");

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
                        SchoolId = Convert.IsDBNull(row["SchoolId"]) ? null : (int?)row["SchoolId"]
                    };
                    

                    if (user.UserType == EUserType.PROFESSOR)
                    {
                        user.School = schoolRepository.GetById(user.SchoolId);
                        foreach (DataRow row2 in ds2.Tables["ProfessorsLanguages"].Rows)
                        {
                            if (user.Id == (int)row2["professorId"])
                            {
                                user.Languages.Add(languageRepository.GetById((int)row2["languageId"]));
                            }
                        }
                    }
                    users.Add(user);
                }
            }

            return users;
        }

        public User GetById(int? id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Users where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);
                DataSet ds = new DataSet();

                string commandText2 = "select * from ProfessorsLanguages";
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(commandText2, conn);
                DataSet ds2 = new DataSet();
                dataAdapter2.Fill(ds2, "ProfessorsLanguages");

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
                        SchoolId = Convert.IsDBNull(row["SchoolId"]) ? null : (int?)row["SchoolId"]
                    };
                    foreach (DataRow row2 in ds2.Tables["ProfessorsLanguages"].Rows)
                    {
                        if (user.Id == (int)row2["professorId"])
                        {
                            user.Languages.Add(languageRepository.GetById((int)row2["languageId"]));
                        }
                    }

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
                command.CommandText = @"update Users set 
                        email = @Email,
                        firstName = @FirstName,
                        lastName = @LastName,
                        password = @Password,
                        gender = @Gender,
                        userType = @UserType,
                        street = @Street,
                        streetNumber = @StreetNumber,
                        city = @City,
                        country = @Country,
                        schoolId = @SchoolId
                        where id=@id";

                command.Parameters.Add(new SqlParameter("Email", user.Email));
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
                command.Parameters.Add(new SqlParameter("SchoolId", (object)user.SchoolId ?? DBNull.Value));

                command.ExecuteScalar();

                
                if (user.UserType == EUserType.PROFESSOR)
                {
                    SqlCommand command3 = conn.CreateCommand();
                    command3.CommandText = $"delete from ProfessorsLanguages where professorId = {id}";
                    command3.ExecuteScalar();
                    foreach (Language language in user.Languages)
                    {
                        SqlCommand command2 = conn.CreateCommand();
                        command2.CommandText = @"insert into ProfessorsLanguages (professorId, languageId)
                                                 values (@professorId, @languageId)";
                        command2.Parameters.Add(new SqlParameter("languageId", user.Id));
                        command2.Parameters.Add(new SqlParameter("professorId", language.Id));
                            command2.ExecuteScalar();
                    }
                }

            }
        }
    }
}
