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
    public partial class AddEditSchoolsWindow : Window
    {
        private School school;
        ISchoolService schoolService = new SchoolService();
        private bool isAddMode;

        public AddEditSchoolsWindow()
        {
            InitializeComponent();
            school = new School
            {
                IsActive= true,
            };
            DataContext = school;
            isAddMode = true;
        }

        public AddEditSchoolsWindow(School school)
        {
            InitializeComponent();
            this.school = school.Clone() as School;

            DataContext = this.school;
            isAddMode = false;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (school.IsValid)
            {
                if (isAddMode)
                {
                    schoolService.Add(school);
                }
                else
                {
                    schoolService.Update(school.Id, school);
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
