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
    public partial class ShowAllTables : Window
    {
        private SchoolService schoolService = new SchoolService();
        private SchoolClassesService schoolClassService = new SchoolClassesService();
        private UserService professorService = new UserService();
        private UserService studentService = new UserService();
        private LanguageService languageService= new LanguageService();

        public ShowAllTables()
        {
            InitializeComponent();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            List<School> schools = schoolService.GetAll().Where(p => p.IsActive).ToList();
            DgSchools.ItemsSource = schools;

            List<SchoolClass> schoolClasses = schoolClassService.GetAll().Where(p => p.IsActive).ToList();
            DgClasses.ItemsSource = schoolClasses;

            List<User> usersProfessors = professorService.GetAll().Where(p => p.UserType == EUserType.PROFESSOR && p.IsActive).ToList();
            DgProfessors.ItemsSource = usersProfessors;

            List<User> usersStudents = studentService.GetAll().Where(p => p.UserType == EUserType.STUDENT && p.IsActive).ToList();
            DgStudents.ItemsSource = usersStudents;

            List<Language> languages = languageService.GetAll().Where(p => p.IsActive).ToList();
            DgLanguages.ItemsSource = languages;


        }
        //--------------------SCHOOL--------------------//
        #region School
        private void BtnAddSchool_Click(object sender, RoutedEventArgs e)
        {
            var addEditSchoolWindow = new AddEditSchoolsWindow();
            var successeful = addEditSchoolWindow.ShowDialog();
            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }
        private void BtnEditSchool_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DgSchools.SelectedItem;

            if (selectedItem != null)
            {
                var schools = schoolService.GetAll();
                var addEditSchoolWindow = new AddEditSchoolsWindow((School)selectedItem);
                var successeful = addEditSchoolWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
        private void BtnDeleteSchool_Click(object sender, RoutedEventArgs e)
        {
            var selectedSchool = DgSchools.SelectedItem as School;

            if (selectedSchool != null)
            {
                schoolService.Delete(selectedSchool.Id);
                RefreshDataGrid();
            }
        }
        private void DgSchools_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        //--------------------SCHOOL CLASS--------------------//
        #region SchoolClass
        private void DgClasses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime) && ( e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive"))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        private void BtnAddClass_Click(object sender, RoutedEventArgs e)
        {
            var addEditSchClassWindow = new AddEditSchoolClassesWindow();

            var successeful = addEditSchClassWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }
        private void BtnEditClass_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DgClasses.SelectedItem;

            if (selectedItem != null)   
            {
                var classes = schoolClassService.GetAll();

                var addEditSchClassWindow = new AddEditSchoolClassesWindow((SchoolClass)selectedItem);

                var successeful = addEditSchClassWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
        private void BtnDeleteClass_Click(object sender, RoutedEventArgs e)
        {
            var selectedClass = DgClasses.SelectedItem as SchoolClass;

            if (selectedClass != null)
            {
                int id = selectedClass.Id;
                schoolClassService.Delete(id);
                RefreshDataGrid();
            }
        }
        #endregion

        //--------------------PROFESSOR--------------------//
        #region Professor
        private void BtnAddProfessor_Click(object sender, RoutedEventArgs e)
        {
            var addEditProfessorWindow = new AddEditProfessorsWindow();

            var successeful = addEditProfessorWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }
        private void BtnEditProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DgProfessors.SelectedItem;

            if (selectedItem != null)
            {
                var professors = professorService.GetAll();
                var addEditProfessorWindow = new AddEditProfessorsWindow((User)selectedItem);
                var successeful = addEditProfessorWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
        private void BtnDeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = DgProfessors.SelectedItem as User;

            if (selectedUser != null)
            {
                professorService.Delete(selectedUser.Id);
                RefreshDataGrid();
            }
        }
        private void DgProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive" || e.PropertyName == "Languages" || e.PropertyName == "Password")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        //--------------------STUDENT--------------------//
        #region Student
        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var addEditStudentWindow = new AddEditStudentsWindow();

            var successeful = addEditStudentWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }
        private void BtnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = DgStudents.SelectedItem;

            if (selectedItem != null)
            {
                var students = studentService.GetAll();

                var addEditStudentWindow = new AddEditStudentsWindow((User)selectedItem);

                var successeful = addEditStudentWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
        private void BtnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = DgStudents.SelectedItem as User;

            if (selectedUser != null)
            {
                studentService.Delete(selectedUser.Id);
                RefreshDataGrid();
            }
        }
        private void DgStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive" || e.PropertyName == "Languages" || e.PropertyName == "Password")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        //--------------------LANGUAGE--------------------//
        #region Language
        private void DgLanguages_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnAddLanguage_Click(object sender, RoutedEventArgs e)
        {
            var addEditLanguagesWindow = new AddEditLanguagesWindow();

            var successeful = addEditLanguagesWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }

        }

        private void BtnEditLanguage_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = DgLanguages.SelectedIndex;
            if (selectedIndex >= 0)
            {
                var languages = languageService.GetAll();
                var addEditLanguagesWindow = new AddEditLanguagesWindow(languages[selectedIndex]);
                var successeful = addEditLanguagesWindow.ShowDialog();
                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void BtnDeleteLanguage_Click(object sender, RoutedEventArgs e)
        {
            var selectedLanguage = DgLanguages.SelectedItem as Language;
            if (selectedLanguage != null)
            {
                languageService.Delete(selectedLanguage.Id);
                RefreshDataGrid();
            }
        }
        #endregion

        private void professorSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DgProfessors.ItemsSource = professorService.GetAll().Where(p => p.IsActive == true && (p.UserType == EUserType.PROFESSOR) && (
               p.FirstName.ToLower().Contains(professorSearch.Text.ToLower())
            || p.LastName.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Email.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Street.ToLower().Contains(professorSearch.Text.ToLower())
            || p.City.ToLower().Contains(professorSearch.Text.ToLower())
            || p.StreetNumber.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Country.ToLower().Contains(professorSearch.Text.ToLower()))).ToList();
        }

        private void studentSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DgStudents.ItemsSource = studentService.GetAll().Where(p => p.IsActive == true && (p.UserType == EUserType.STUDENT) && (
               p.FirstName.ToLower().Contains(professorSearch.Text.ToLower())
            || p.LastName.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Email.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Street.ToLower().Contains(professorSearch.Text.ToLower())
            || p.City.ToLower().Contains(professorSearch.Text.ToLower())
            || p.StreetNumber.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Country.ToLower().Contains(professorSearch.Text.ToLower()))).ToList();
        }
    }
}
