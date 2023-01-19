using LanguageSchools.Models;
using LanguageSchools.Repositories;
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
        private IUserRepository userRepository= new UserRepository();
        private bool isAddMode;
        
        public AddEditProfessorsWindow()
        {
            InitializeComponent();
            cbSchool.ItemsSource = new List<School>(schoolService.GetAll().Where(p => p.IsActive));
            cbSchool.SelectedItem = cbSchool.Items[0];
            cbLanguage.ItemsSource = new List<Language>(languageService.GetAll().Where(p => p.IsActive));
            //cbLanguage.SelectedItem = cbLanguage.Items[0];
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
            if (Data.Instance.loggedUser.UserType == EUserType.PROFESSOR || this.professor.UserType == EUserType.PROFESSOR)
            {
                lblUserType.Visibility = Visibility.Collapsed;
                cbUserType.Visibility = Visibility.Collapsed;
                List<School> schools = schoolService.GetAll().Where(p => p.IsActive).ToList();
                cbSchool.ItemsSource = schools;
                cbSchool.SelectedValuePath = "Id";
                cbSchool.SelectedValue = this.professor.School.Id;

                List<Language> languages = languageService.GetAll().Where(p => p.IsActive).ToList();
                cbLanguage.ItemsSource = languages;
                //cbLanguage.SelectedValuePath = "Id";
                //cbLanguage.SelectedValue = this.professor
                User usersLanguages = userRepository.GetById(professor.Id);
                foreach (Language userLanguage in usersLanguages.Languages)
                {
                    foreach (Language language in languages)
                    {
                        if (userLanguage.Id == language.Id)
                        {
                            cbLanguage.SelectedItems.Add(language);
                        }
                    }
                }
            }
            else
            {
                
                lblUserType.Visibility = Visibility.Visible;
                cbUserType.Visibility = Visibility.Visible;
            }
            
            DataContext = this.professor;
                
            isAddMode = false;
            txtJMBG.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            professor.School = (School)cbSchool.SelectedItem;
            //professor.Languages = (Language)cbLanguage.SelectedItem;
            if (!String.IsNullOrEmpty(txtEmail.Text) && !String.IsNullOrEmpty(txtPassword.Text) && !String.IsNullOrEmpty(txtFirstName.Text) &&
                !String.IsNullOrEmpty(txtCountry.Text) && !String.IsNullOrEmpty(txtJMBG.Text) && !String.IsNullOrEmpty(txtLastName.Text)
                && !String.IsNullOrEmpty(txtCity.Text) && !String.IsNullOrEmpty(txtNumber.Text) && !String.IsNullOrEmpty(txtStreet.Text))
            {
                foreach (Language language in cbLanguage.SelectedItems)
                {
                    professor.Languages.Add(language);
                }
                if (isAddMode)
                {
                    professorService.Add(professor);
                }
                else
                {
                    professorService.Update(professor.Id, professor);
                    if (this.professor.UserType == EUserType.ADMIN)
                    {
                        Data.Instance.loggedUser = professor;

                    }
                }

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("One of the Fields is Empty");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
