using LanguageSchools.Models;
using LanguageSchools.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    public partial class ShowProfessorsClasses : Window
    {
        private User professor; 
        private SchoolClass schoolClass;
        private SchoolClassesService classesService = new SchoolClassesService();
        private List<SchoolClass> classesList = new List<SchoolClass>();
        public ShowProfessorsClasses(List<SchoolClass> classes, User professor)
        {
            InitializeComponent();
            this.professor = professor;
            classesList = classes;
            RefreshDataGrid();
        }
        public void RefreshDataGrid()
        {
            var professorId = Data.Instance.loggedUser.Id;
            classesList = classesService.GetAll().Where(p => p.Professor.Id == this.professor.Id && p.IsActive == true && p.State == EState.AVAILABLE).ToList();
            DgUncheduledClasses.ItemsSource = classesList;
            classesList = classesService.GetAll().Where(p => p.Professor.Id == this.professor.Id && p.IsActive == true && p.State == EState.RESERVED
                                                        && p.StudentId == Data.Instance.loggedUser.Id).ToList();
            DgScheduledClasses.ItemsSource = classesList;
        }

    
        private void BtnScheduleClass_Click(object sender, RoutedEventArgs e)
        {
            schoolClass = (SchoolClass)DgUncheduledClasses.SelectedItem;
            if (schoolClass != null)
            {
                if (schoolClass.State == EState.AVAILABLE)
                {
                    schoolClass.State = EState.RESERVED;
                    schoolClass.Student = Data.Instance.loggedUser;
                    classesService.Update(schoolClass.Id, schoolClass);
                }
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("No Class is Selected");
            }
        }
        private void BtnUnscheduleClass_Click(object sender, RoutedEventArgs e)
        {
            schoolClass = (SchoolClass)DgScheduledClasses.SelectedItem;
            if (schoolClass != null)
            {
                if (schoolClass.State == EState.RESERVED)
                {
                    schoolClass.State = EState.AVAILABLE;
                    schoolClass.Student = null;
                    classesService.Update(schoolClass.Id, schoolClass);
                }
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("No Class is Selected");
            }
        }

        private void DgUncheduledClasses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "StudentId" || e.PropertyName == "ProfessorId")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void DgScheduledClasses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "StudentId" || e.PropertyName == "ProfessorId")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        
    }
}
