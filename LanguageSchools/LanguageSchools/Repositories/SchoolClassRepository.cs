using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace LanguageSchools.Repositories
{
    class SchoolClassRepository : ISchoolClassRepository
    {
        public int Add(SchoolClass schoolClass)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.SchoolClasses (Name, Date, StartTime, Duration, State, IsActive, ProfessorId, StudentId)
                    output inserted.Id
                    values (@Email, @Password, @FirstName, @LastName, @Jmbg, @Gender, @UserType, @IsActive, @Street, @StreetNumber, @City, @Country)";

                command.Parameters.Add(new SqlParameter("Name", schoolClass.Name));
                command.Parameters.Add(new SqlParameter("Date", schoolClass.Date));
                command.Parameters.Add(new SqlParameter("StartTime", schoolClass.StartTime));
                command.Parameters.Add(new SqlParameter("Duration", schoolClass.Duration));
                command.Parameters.Add(new SqlParameter("State", schoolClass.State));
                command.Parameters.Add(new SqlParameter("IsActive", schoolClass.IsActive));
                command.Parameters.Add(new SqlParameter("ProfessorId", (object)schoolClass.ProfessorId ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("StudentId", (object)schoolClass.StudentId ?? DBNull.Value));

                return (int)command.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.SchoolClasses set IsActive=0 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<SchoolClass> GetAll()
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();

            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = "select * from dbo.SchoolClasses";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "SchoolClassses");

                foreach (DataRow row in ds.Tables["SchoolClasses"].Rows)
                {
                    var schoolClass = new SchoolClass
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        //Date = (DateTime)row["Date"] as DateTime,
                        StartTime = row["StartTime"] as string,
                        Duration = row["Duration"] as string,
                        State = (EState)Enum.Parse(typeof(EState), row["Jmbg"] as string),
                        IsActive = (bool)row["IsActive"],
                        ProfessorId = (int)row["ProfessorId"],
                        StudentId = (int)row["StudentId"]
                    };
                    //uzmem IDeve koje dobijem i nadjem klase
                    //user.findbyid(profesorId)
                    schoolClasses.Add(schoolClass);
                }
            }
            return schoolClasses;
        }

        public SchoolClass GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                string commandText = $"select * from dbo.SchoolClasses where Id={id}";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandText, conn);

                DataSet ds = new DataSet();

                dataAdapter.Fill(ds, "SchoolClasses");
                if (ds.Tables["SchoolClasses"].Rows.Count > 0)
                {
                    var row = ds.Tables["SchoolClasses"].Rows[0];

                    var schoolClass = new SchoolClass
                    {
                        Id = (int)row["Id"],
                        Name = row["Name"] as string,
                        //Date = (DateTime)row["Date"] as DateTime,
                        StartTime = row["StartTime"] as string,
                        Duration = row["Duration"] as string,
                        State = (EState)Enum.Parse(typeof(EState), row["Jmbg"] as string),
                        IsActive = (bool)row["IsActive"],
                        ProfessorId = (int)row["ProfessorId"],
                        StudentId = (int)row["StudentId"]
                    };
                    return schoolClass;
                }
            }
            return null;
        }

        public void Update(int id, SchoolClass schoolClass)
        {
            using (SqlConnection conn = new SqlConnection(Config.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.SchoolClasses set 
                        Name = @Name,
                        Date = @Date,
                        StartTime = @StartTime,
                        Duration = @Duration,
                        State = @State,
                        IsActive = @IsActive,
                        ProfessorId = @ProfessorId,
                        StudentId = @StudentId";

                command.Parameters.Add(new SqlParameter("Name", schoolClass.Name));
                command.Parameters.Add(new SqlParameter("Date", schoolClass.Date));
                command.Parameters.Add(new SqlParameter("StartTime", schoolClass.StartTime));
                command.Parameters.Add(new SqlParameter("Duration", schoolClass.Duration));
                command.Parameters.Add(new SqlParameter("State", schoolClass.State));
                command.Parameters.Add(new SqlParameter("IsActive", schoolClass.IsActive));
                command.Parameters.Add(new SqlParameter("ProfessorId", (object)schoolClass.ProfessorId ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("StudentId", (object)schoolClass.StudentId ?? DBNull.Value));

                command.ExecuteScalar();
            }
        }
    }
}
