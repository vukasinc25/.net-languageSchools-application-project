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
        private bool isAddMode;
        public AddEditSchoolClassesWindow()
        {
            InitializeComponent();
            var schoolClass = new SchoolClass
            {
                IsActive = true
            };

            isAddMode = true;
            DataContext = schoolClass;
        }
        public AddEditSchoolClassesWindow(SchoolClass schoolClass)
        {
            InitializeComponent();
            this.schoolClass = schoolClass.Clone() as SchoolClass;

            DataContext = this.schoolClass;

            isAddMode = false;
            //TxtJmbg.IsReadOnly = true;
            //TxtEmail.IsReadOnly = true;
        }
    }
}
