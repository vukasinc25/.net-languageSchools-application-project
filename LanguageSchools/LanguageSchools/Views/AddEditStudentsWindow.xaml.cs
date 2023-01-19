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
    public partial class AddEditStudentsWindow : Window
    {
        private User student;
        private IUserService studentService = new UserService();
        private bool isAddMode;
        public AddEditStudentsWindow()
        {
            InitializeComponent();
            student = new User
            {
                UserType = EUserType.STUDENT,
                IsActive = true
            };

            isAddMode = true;
            DataContext = student;
        }

        public AddEditStudentsWindow(User student)
        {
            InitializeComponent();
            this.student = student.Clone() as User;

            DataContext = this.student;

            isAddMode = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (student.IsValid)
            {
                if (isAddMode)
                {
                    studentService.Add(student);
                }
                else
                {
                    studentService.Update(student.Id, student);
                }

                DialogResult = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
