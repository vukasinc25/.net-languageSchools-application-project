using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Repositories
{
    class LanguageRepository : ILanguageRepository
    {
        public LanguageRepository() { }
        public int Add(Language language)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into Languages (Name, IsActive)
                    output inserted.Id
                    values (@Name, @IsActive)";

                command.Parameters.Add(new SqlParameter("Name", language.Name));
                command.Parameters.Add(new SqlParameter("IsActive", language.IsActive));

                return (int)command.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update Languages set IsActive=0 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Language> GetAll()
        {
            List<Language> languages = new List<Language>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from Languages";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Languages");

                foreach (DataRow row in ds.Tables["Languages"].Rows)
                {
                    var language = new Language
                    {
                        Id = (int)row["id"],        
                        Name = row["name"] as string,
                        IsActive = (bool)row["isActive"]
                    };
                    languages.Add(language);
                }
            }
            return languages;
        }

        public Language GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from Languages where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Languages");
                if (ds.Tables["Languages"].Rows.Count > 0)
                {
                    var row = ds.Tables["Languages"].Rows[0];

                    var language = new Language
                    {
                        Id = (int)row["id"],
                        Name = row["name"] as string,
                        IsActive = (bool)row["isActive"]
                    };
                    return language;
                }
            }
            return null;
        }

        public void Update(int id, Language language)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update Languages set 
                        Name = @Name,
                        IsActive = @IsActive
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("Name", language.Name));
                command.Parameters.Add(new SqlParameter("IsActive", language.IsActive));
                command.Parameters.Add(new SqlParameter("id", id));

                command.ExecuteScalar();
            }
        }
    }
}
