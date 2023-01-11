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
    /// Interaction logic for AddEditSchoolClassesWindow.xaml
    /// </summary>
    public partial class AddEditSchoolClassesWindow : Window
    {
        private SchoolClass schoolClass;
        private ISchoolClassesService schoolClassService = new SchoolClassesService();
        private IProfessorService professorService = new ProfessorService();
        private IStudentService studentService = new StudentService();
        private bool isAddMode;
        public AddEditSchoolClassesWindow()
        {
            InitializeComponent();
            cbProfessor.ItemsSource = new List<Professor>(professorService.GetAll());
            cbStudent.ItemsSource = new List<Student>(studentService.GetAll());

            schoolClass = new SchoolClass
            {
                Date = DateTime.Today,
                IsActive = true

                //Id = 0
            };

            isAddMode = true;
            DataContext = schoolClass;
        }
        public AddEditSchoolClassesWindow(SchoolClass schoolClass)
        {
            InitializeComponent();
            cbProfessor.ItemsSource = new List<Professor>(professorService.GetAll());
            cbStudent.ItemsSource = new List<Student>(studentService.GetAll());
            this.schoolClass = schoolClass.Clone() as SchoolClass;

            DataContext = this.schoolClass;

            isAddMode = false;
            txtName.IsReadOnly = true;
            //TxtEmail.IsReadOnly = true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isAddMode)
            {
                schoolClass.Professor = cbProfessor.SelectedItem as Professor;
                schoolClass.Student = cbStudent.SelectedItem as Student;
                schoolClassService.Add(schoolClass);
            }
            else
            {
                schoolClassService.Update(schoolClass.Id, schoolClass);
            }
            DialogResult = true;
            Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
