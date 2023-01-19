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
    /// Interaction logic for ShowClassesStudents.xaml
    /// </summary>
    public partial class ShowClassesStudents : Window
    {
        private User student;
        private SchoolClass schoolClass;
        private List<User> studentsList;
        private UserService studentService = new UserService();
        public ShowClassesStudents(List<User> students, SchoolClass schoolClass)
        {
            InitializeComponent();
            this.schoolClass = schoolClass;
            studentsList = students;
            RefreshDataGrid();
        }
        public void RefreshDataGrid()
        {
            studentsList = studentService.GetAll().Where(p => p.Id == this.schoolClass.StudentId && p.IsActive == true).ToList();
            DgClassesStudents.ItemsSource = studentsList;
        }

        private void DgClassesStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive" || e.PropertyName == "Languages" || e.PropertyName == "Password")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
