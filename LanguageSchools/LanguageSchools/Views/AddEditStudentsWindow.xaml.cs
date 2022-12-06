using LanguageSchools.Models;
using LanguageSchools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LanguageSchools.Views
{
    /// <summary>
    /// Interaction logic for AddEditStudentsWindow.xaml
    /// </summary>
    public partial class AddEditStudentsWindow : Window
    {
        private Student student;
        private IStudentService studentService = new StudentService();
        private bool isAddMode;
        public AddEditStudentsWindow()
        {
            InitializeComponent();

            var user = new User
            {
                UserType = EUserType.STUDENT,
                IsActive = true
            };

            student = new Student
            {
                User = user
            };

            isAddMode = true;
            DataContext = student;
        }

        public AddEditStudentsWindow(Student student)
        {
            InitializeComponent();
            this.student = student.Clone() as Student;

            DataContext = this.student;

            isAddMode = false;
            TxtJmbg.IsReadOnly = true;
            TxtEmail.IsReadOnly = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (student.User.IsValid)
            {
                if (isAddMode)
                {
                    studentService.Add(student);
                }
                else
                {
                    studentService.Update(student.User.Email, student);
                }

                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
