using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    interface IStudentService
    {
        List<Student> GetAll();
        Student GetById(string email);
        List<Student> GetActiveStudents();
        List<Student> GetActiveStudentsByEmail(string email);
        List<Student> GetActiveStudentsOrderedByEmail();
        void Add(Student student);
        void Set(List<Student> students);
        void Update(string email, Student student);
        void Delete(string email);
        List<User> ListAllStudents();
    }
}
