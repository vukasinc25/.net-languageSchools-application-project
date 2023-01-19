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
    public class SchoolRepository : ISchoolRepository
    {
        public SchoolRepository()
        {
        }

        public int Add(School school)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Schools (Name, IsActive, Street, StreetNumber, City, Country)
                    output inserted.Id
                    values (@Name, @IsActive, @Street, @StreetNumber, @City, @Country)";

                command.Parameters.Add(new SqlParameter("Name", school.Name));
                command.Parameters.Add(new SqlParameter("IsActive", school.IsActive));
                command.Parameters.Add(new SqlParameter("Street", school.Street));
                command.Parameters.Add(new SqlParameter("StreetNumber", school.StreetNumber));
                command.Parameters.Add(new SqlParameter("City", school.City));
                command.Parameters.Add(new SqlParameter("Country", school.Country));

                return (int)command.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Schools set IsActive=0 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<School> GetAll()
        {
            List<School> schools = new List<School>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.Schools";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Schools");

                foreach (DataRow row in ds.Tables["Schools"].Rows)
                {
                    var school = new School
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        IsActive = (bool)row["IsActive"],
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string,
                    };

                    schools.Add(school);
                }
            }

            return schools;
        }

        public School GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.Schools where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "Schools");
                if (ds.Tables["Schools"].Rows.Count > 0)
                {
                    var row = ds.Tables["Schools"].Rows[0];

                    var school = new School
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        IsActive = (bool)row["IsActive"],
                        Street = row["Street"] as string,
                        StreetNumber = row["StreetNumber"] as string,
                        City = row["City"] as string,
                        Country = row["Country"] as string,
                    };

                    return school;
                }
            }

            return null;
        }

        public void Update(int id, School school)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Schools set 
                        Name = @Name,
                        Street = @Street,
                        StreetNumber = @StreetNumber,
                        City = @City,
                        Country = @Country
                        where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.Parameters.Add(new SqlParameter("Name", school.Name));
                command.Parameters.Add(new SqlParameter("IsActive", school.IsActive));
                command.Parameters.Add(new SqlParameter("Street", school.Street));
                command.Parameters.Add(new SqlParameter("StreetNumber", school.StreetNumber));
                command.Parameters.Add(new SqlParameter("City", school.City));
                command.Parameters.Add(new SqlParameter("Country", school.Country));

                command.ExecuteScalar();
            }
        }
    }
}
