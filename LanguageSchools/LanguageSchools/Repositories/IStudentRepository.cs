using LanguageSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Repositories
{
    interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(string email);
        void Add(Student student);
        void Add(List<Student> student);
        void Set(List<Student> student);
        void Update(string email, Student student);
        void Delete(string email);
    }
}
