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
    public partial class AddEditSchoolClassesWindow : Window
    {
        private SchoolClass schoolClass;
        private ISchoolClassesService schoolClassService = new SchoolClassesService();
        private IUserService professorService = new UserService();
        private IUserService studentService = new UserService();
        private bool isAddMode;
        public AddEditSchoolClassesWindow()
        {
            InitializeComponent();
            cbProfessor.ItemsSource = new List<User>(professorService.GetAll().Where(p => p.UserType == EUserType.PROFESSOR));
            cbProfessor.SelectedItem = cbProfessor.Items[0];
            txtDate.BlackoutDates.AddDatesInPast();
            //cbStudent.ItemsSource = new List<User>(studentService.GetAll());
            //cbStudent.SelectedItem= cbStudent.Items[0];

            schoolClass = new SchoolClass
            {
                Date = DateTime.Today,
                IsActive = true
            };

            isAddMode = true;
            DataContext = schoolClass;
        }
        public AddEditSchoolClassesWindow(SchoolClass schoolClass)
        {
            InitializeComponent();
            this.schoolClass = schoolClass.Clone() as SchoolClass;
            List<User> professors = professorService.GetAll().Where(p => p.UserType == EUserType.PROFESSOR).ToList();
            cbProfessor.ItemsSource = professors;
            cbProfessor.SelectedValuePath = "Id";
            cbProfessor.SelectedValue = this.schoolClass.Professor.Id;

            DataContext = this.schoolClass;
            isAddMode = false;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            schoolClass.Professor = (User)cbProfessor.SelectedItem;
            if (!String.IsNullOrEmpty(txtStartTime.Text) && !String.IsNullOrEmpty(txtDuration.Text))
            {
                if (isAddMode)
                {
                    schoolClass.Professor = cbProfessor.SelectedItem as User;
                    //schoolClass.Student = cbStudent.SelectedItem as User;
                    schoolClassService.Add(schoolClass);
                }
                else
                {
                    schoolClassService.Update(schoolClass.Id, schoolClass);
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
    }
}
