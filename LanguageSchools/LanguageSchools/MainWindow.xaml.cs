using LanguageSchools.Models;
using LanguageSchools.Services;
using LanguageSchools.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanguageSchools
{

    public partial class MainWindow : Window
    {
        private SchoolService schoolService = new SchoolService();
        private SchoolClassesService schoolClassService = new SchoolClassesService();
        private UserService professorService = new UserService();
        private UserService studentService = new UserService();
        public MainWindow()
        {
            InitializeComponent();
            //Data.Load();
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            List<User> usersStudents = studentService.GetAll().Where(p => p.UserType == EUserType.STUDENT).ToList();
            DgStudents.ItemsSource = usersStudents;
            //DgStudentsAll.ItemsSource = usersStudents;

            List<User> usersProfessors = professorService.GetAll().Where(p => p.UserType == EUserType.PROFESSOR).ToList();
            DgProfessors.ItemsSource = usersProfessors;

            List<School> schools = schoolService.GetAll();
            DgSchools.ItemsSource = schools;

            //List<SchoolClass> schoolClasses = schoolClassService.GetAll().Select(p => p).ToList();
            //DgClasses.ItemsSource = schoolClasses;
            //DgClassesAll.ItemsSource = schoolClasses;

        }
        //--------------------PROFESSOR--------------------//
        #region Professor
        private void BtnAddProfessors_Click(object sender, RoutedEventArgs e)
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
            var selectedIndex = DgProfessors.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var professors = professorService.GetAll();

                var addEditProfessorWindow = new AddEditProfessorsWindow(professors[selectedIndex]);

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
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
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
            var selectedIndex = DgStudents.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var students = studentService.GetAll();

                var addEditStudentWindow = new AddEditStudentsWindow(students[selectedIndex]);

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
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        //--------------------CLASS--------------------//
        #region Class
        //private void DgClasses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    if (e.PropertyType == typeof(DateTime))
        //    {
        //        (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        //    }
        //}
        //private void BtnAddClasses_Click(object sender, RoutedEventArgs e)
        //{
        //    var addEditSchClassWindow = new AddEditSchoolClassesWindow();

        //    var successeful = addEditSchClassWindow.ShowDialog();

        //    if ((bool)successeful)
        //    {
        //        RefreshDataGrid();
        //    }
        //}
        //private void BtnEditClasses_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedIndex = DgClasses.SelectedIndex;

        //    if (selectedIndex >= 0)
        //    {
        //        var classes = schoolClassService.GetAll();

        //        var addEditSchClassWindow = new AddEditSchoolClassesWindow(classes[selectedIndex]);

        //        var successeful = addEditSchClassWindow.ShowDialog();

        //        if ((bool)successeful)
        //        {
        //            RefreshDataGrid();
        //        }
        //    }
        //}
        //private void BtnDeleteClasses_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedClass = DgClasses.SelectedItem as SchoolClass;

        //    if (selectedClass != null)
        //    {
        //        int id = selectedClass.Id;
        //        schoolClassService.Delete(id);
        //        RefreshDataGrid();
        //    }
        //}
        #endregion

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
            var selectedIndex = DgSchools.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var schools = schoolService.GetAll();

                var addEditSchoolWindow = new AddEditSchoolsWindow(schools[selectedIndex]);

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
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
        

        private void ShowAllTables_Click(object sender, RoutedEventArgs e)
        {
            var showAllTables = new ShowAllTables();
            var successeful = showAllTables.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            var addEditStudentWindow = new AddEditStudentsWindow();
            var successeful = addEditStudentWindow.ShowDialog();
            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            var successeful = loginWindow.ShowDialog();
            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }
    }
}
