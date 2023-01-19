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
        private ISchoolService schoolService = new SchoolService();
        private ILanguageService languageService = new LanguageService();
        private bool isAddMode;
        
        public AddEditProfessorsWindow()
        {
            InitializeComponent();
            cbSchool.ItemsSource = new List<School>(schoolService.GetAll().Where(p => p.IsActive));
            cbSchool.SelectedItem = cbSchool.Items[0];
            cbLanguage.ItemsSource = new List<Language>(languageService.GetAll().Where(p => p.IsActive));
            cbLanguage.SelectedItem = cbLanguage.Items[0];
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
            List<School> schools = schoolService.GetAll().Where(p => p.IsActive).ToList();
            cbSchool.ItemsSource = schools;
            cbSchool.SelectedValuePath = "Id";
            cbSchool.SelectedValue = this.professor.School.Id;

            List<Language> languages = languageService.GetAll().Where(p => p.IsActive).ToList();
            cbLanguage.ItemsSource = languages;
            cbLanguage.SelectedValuePath = "Id";
            //cbLanguage.SelectedValue = this.professor
            
            DataContext = this.professor;

            isAddMode = false;
            txtJMBG.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            professor.School = (School)cbSchool.SelectedItem;
            //professor.Languages = (Language)cbLanguage.SelectedItem;
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
