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
    public partial class AddEditProfessorsWindow : Window
    {
        private User professor;
        private IUserService professorService = new UserService();
        private bool isAddMode;
        
        public AddEditProfessorsWindow()
        {
            InitializeComponent();
            professor = new User
            {
                UserType = EUserType.PROFESSOR,
                IsActive = true
            };

            isAddMode = true;
            DataContext = professor;
        }

        public AddEditProfessorsWindow(User professor)
        {
            InitializeComponent();
            this.professor = professor.Clone() as User;
            
            DataContext = this.professor;

            isAddMode = false;
            txtJMBG.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (professor.IsValid)
            {
                if (isAddMode)
                {
                    professorService.Add(professor);
                }
                else
                {
                    professorService.Update(professor.Id, professor);
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
