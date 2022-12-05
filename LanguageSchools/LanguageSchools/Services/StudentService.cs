using LanguageSchools.Models;
using LanguageSchools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchools.Services
{
    class StudentService : IStudentService
    {
        private IStudentRepository studentRepository;
        private IUserRepository userRepository;

        public StudentService()
        {
            studentRepository = new StudentRepository();
            userRepository = new UserRepository();
        }

        public Student GetById(string email)
        {
            return studentRepository.GetById(email);
        }
        public List<Student> GetAll()
        {
            return studentRepository.GetAll();
        }
        public List<Student> GetActiveStudents()
        {
            return studentRepository.GetAll().Where(p => p.User.IsActive).ToList();
        }
        public List<Student> GetActiveStudentsByEmail(string email)
        {
            return studentRepository.GetAll().Where(p => p.User.IsActive && p.User.Email.Contains(email)).ToList();
        }
        public List<Student> GetActiveStudentsOrderedByEmail()
        {
            return studentRepository.GetAll().Where(p => p.User.IsActive).OrderBy(p => p.User.Email).ToList();
        }
        public void Add(Student student)
        {
            studentRepository.Add(student);
            userRepository.Add(student.User);
        }

        public void Set(List<Student> students)
        {
            studentRepository.Set(students);
        }
        public void Update(string email, Student student)
        {
            userRepository.Update(email, student.User);
            studentRepository.Update(email, student);
        }
        public void Delete(string email)
        {
            studentRepository.Delete(email);
            userRepository.Delete(email);
        }
        public List<User> ListAllStudents()
        {
            throw new NotImplementedException();
            // TODO :D
        }
       
    } 
}

