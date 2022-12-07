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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StudentService studentService = new StudentService();
        private ProfessorService professorService = new ProfessorService();
        private SchoolClassesService schoolClassService = new SchoolClassesService();
        public MainWindow()
        {
            InitializeComponent();
            Data.Load();
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            List<User> usersStudents = studentService.GetAll().Select(p => p.User).ToList();
            DgStudents.ItemsSource = usersStudents;
   
            List<User> usersProfessors = professorService.GetAll().Select(p => p.User).ToList();
            DgProfessors.ItemsSource = usersProfessors;

            List<SchoolClass> schoolClasses = schoolClassService.GetAll().Select(p => p).ToList();
            DgClasses.ItemsSource = schoolClasses;

            //Console.WriteLine("alooo1" + usersStudents);
            //Console.WriteLine("alooo2" + usersProfessors);
            //Console.WriteLine("alooo3" + schoolClasses);
        }
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
                professorService.Delete(selectedUser.Email);
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
                studentService.Delete(selectedUser.Email);
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
        private void DgClasses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnAddClasses_Click(object sender, RoutedEventArgs e)
        {
            var addEditSchClassWindow = new AddEditSchoolClassesWindow();

            var successeful = addEditSchClassWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void BtnEditClasses_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = DgClasses.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var classes = schoolClassService.GetAll();

                var addEditSchClassWindow = new AddEditSchoolClassesWindow(classes[selectedIndex]);

                var successeful = addEditSchClassWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void BtnDeleteClasses_Click(object sender, RoutedEventArgs e)
        {
            var selectedClass = DgClasses.SelectedItem as SchoolClass;

            if (selectedClass != null)
            {
                int id = selectedClass.Id;
                schoolClassService.Delete(id);
                RefreshDataGrid();
            }
        }

        //private void BtnAddSchools_Click(object sender, RoutedEventArgs e)
        //{
        //    var addEditSchoolWindow = new AddEditSchoolsWindow();

        //    var successeful = addEditSchoolWindow.ShowDialog();

        //    if ((bool)successeful)
        //    {
        //        RefreshDataGrid();
        //    }
        //}

        //private void BtnEditSchools_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedIndex = DgSchools.SelectedIndex;

        //    if (selectedIndex >= 0)
        //    {
        //        var students = studentService.GetAll();

        //        var addEditStudentWindow = new AddEditStudentsWindow(students[selectedIndex]);

        //        var successeful = addEditStudentWindow.ShowDialog();

        //        if ((bool)successeful)
        //        {
        //            RefreshDataGrid();
        //        }
        //    }
        //}

        //private void BtnDeleteSchools_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedUser = DgStudents.SelectedItem as User;

        //    if (selectedUser != null)
        //    {
        //        studentService.Delete(selectedUser.Email);
        //        RefreshDataGrid();
        //    }
        //}

        //private void DgSchols_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
        //    {
        //        e.Column.Visibility = Visibility.Collapsed;
        //    }
        //}
    }
}
