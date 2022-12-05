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
    /// Interaction logic for ShowProfessorsWindow.xaml
    /// </summary>
    public partial class ShowProfessorsWindow : Window
    {
        private ProfessorService professorService = new ProfessorService();

        public ShowProfessorsWindow()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void miAddProfessor_Click(object sender, RoutedEventArgs e)
        {
            var addEditProfessorWindow = new AddEditProfessorsWindow();

            var successeful = addEditProfessorWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgProfessors.SelectedIndex;

            if(selectedIndex >= 0)
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

        private void miDeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgProfessors.SelectedItem as User;

            if(selectedUser != null)
            {
                professorService.Delete(selectedUser.Email);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            List<User> users = professorService.GetAll().Select(p => p.User).ToList();
            dgProfessors.ItemsSource = users;
        }

        private void dgProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
