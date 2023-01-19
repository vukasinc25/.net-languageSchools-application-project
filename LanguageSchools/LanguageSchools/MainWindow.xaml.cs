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
        private School school = new School();
        private User professor = new User();
        private SchoolClass schoolClass;
        private List<School> schoolList = new List<School>();
        private List<User> usersProfessors = new List<User>();
        List<User> professorListFilled = new List<User>();
        private List<SchoolClass> classesList = new List<SchoolClass>();
        private List<int> listLangUser = new List<int>();
        private List<SchoolClass> professorsClasses = new List<SchoolClass>();

        public MainWindow()
        {
            InitializeComponent();
            showButtons();
            RefreshDataGrid();
            //Data.Instance.loggedUser = Data.Instance.loggedUser;
        }

        private void RefreshDataGrid()
        {
            List<User> usersStudents = studentService.GetAll().Where(p => p.UserType == EUserType.STUDENT).ToList();
            //DgStudents.ItemsSource = usersStudents;
            //DgStudentsAll.ItemsSource = usersStudents;

            usersProfessors = professorService.GetAll().Where(p => p.UserType == EUserType.PROFESSOR).ToList();
            DgSearchedProfessors.ItemsSource = usersProfessors;

            List<School> schools = schoolService.GetAll();
            DgSchools.ItemsSource = schools;

            //List<SchoolClass> schoolClasses = schoolClassService.GetAll().Select(p => p).ToList();
            //DgClasses.ItemsSource = schoolClasses;
            //DgClassesAll.ItemsSource = schoolClasses;

            if (Data.Instance.loggedUser.UserType == EUserType.STUDENT)
            {
                var studentId = Data.Instance.loggedUser.Id;
                List<SchoolClass> studentsClasses = schoolClassService.GetAll().Where(p => p.StudentId == studentId && p.IsActive == true).ToList();
                DgLoggedClasses.ItemsSource= studentsClasses;
            }
            else if (Data.Instance.loggedUser.UserType == EUserType.PROFESSOR)
            {
                var professorId = Data.Instance.loggedUser.Id;
                professorsClasses = schoolClassService.GetAll().Where(p => p.ProfessorId == professorId && p.IsActive == true).ToList();
                DgLoggedClasses.ItemsSource= professorsClasses;
            }
        }
        //--------------------PROFESSOR--------------------//
        #region Professor
        private void DgProfessors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Data.Instance.loggedUser.JMBG != null && Data.Instance.loggedUser.UserType != EUserType.PROFESSOR)
            {
                professor = (User)DgProfessors.SelectedItem;
                var classes = schoolClassService.GetAll().Where(p => p.Professor.Id == professor.Id && p.IsActive == true).ToList();
                ShowProfessorsClasses showProfessorsClasses = new ShowProfessorsClasses(classes, professor);
                showProfessorsClasses.ShowDialog();
                RefreshDataGrid();
            }
        }
        private void DgSearchedProfessors_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive" || e.PropertyName == "Languages" || e.PropertyName == "Password")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        private void professorLanguageSearch_KeyUp(object sender, KeyEventArgs e)
        {
            // professorLanguageSearch
            // DgSearchedProfessors
            string searchedString = professorLanguageSearch.Text.ToLower();
            List<User> searchedProfessors = new List<User>();
            professorListFilled = professorService.GetAll();

            foreach (User professor in professorListFilled)
            {
                foreach (User schoolProfessor in usersProfessors)
                {
                    if (professor.Id == schoolProfessor.Id)
                    {
                        foreach (Language language in professor.Languages)
                        {
                            if (language.Name.ToLower().Contains(searchedString))
                            {
                                searchedProfessors.Add(professor);
                                break;
                            }
                        }
                    }
                }
            }
            if (searchedProfessors.Count > usersProfessors.Count)
            {
                searchedProfessors.Clear();
            }
            else
            {
                DgSearchedProfessors.ItemsSource = searchedProfessors;
            }
        }
        private void professorSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DgSearchedProfessors.ItemsSource = professorService.GetAll().Where(p => p.IsActive == true && (p.UserType == EUserType.PROFESSOR) && (
               p.FirstName.ToLower().Contains(professorSearch.Text.ToLower())
            || p.LastName.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Email.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Street.ToLower().Contains(professorSearch.Text.ToLower())
            || p.City.ToLower().Contains(professorSearch.Text.ToLower())
            || p.StreetNumber.ToLower().Contains(professorSearch.Text.ToLower())
            || p.Country.ToLower().Contains(professorSearch.Text.ToLower()))).ToList();
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
        //private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        //{
        //    var addEditStudentWindow = new AddEditStudentsWindow();

        //    var successeful = addEditStudentWindow.ShowDialog();

        //    if ((bool)successeful)
        //    {
        //        RefreshDataGrid();
        //    }
        //}
        //private void BtnEditStudent_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedIndex = DgStudents.SelectedIndex;

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
        //private void BtnDeleteStudent_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedUser = DgStudents.SelectedItem as User;

        //    if (selectedUser != null)
        //    {
        //        studentService.Delete(selectedUser.Id);
        //        RefreshDataGrid();
        //    }
        //}
        //private void DgStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    if (e.PropertyName == "Error" || e.PropertyName == "IsValid")
        //    {
        //        e.Column.Visibility = Visibility.Collapsed;
        //    }
        //}
        #endregion

        //--------------------CLASS--------------------//
        #region Class
        private void DgLoggedClasses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Data.Instance.loggedUser.JMBG != null && Data.Instance.loggedUser.UserType != EUserType.STUDENT)
            {
                schoolClass = (SchoolClass)DgLoggedClasses.SelectedItem;
                var students = studentService.GetAll().Where(p => p.Id == schoolClass.Id && p.IsActive == true).ToList();
                ShowClassesStudents showClassesStudents = new ShowClassesStudents(students, schoolClass);
                showClassesStudents.ShowDialog();
                RefreshDataGrid();
            }
        }
        private void DgClasses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime) && (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive"))
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
   
        private void BtnDeleteClass_Click(object sender, RoutedEventArgs e)
        {
            var selectedClass = DgLoggedClasses.SelectedItem as SchoolClass;

            if (selectedClass != null)
            {
                int id = selectedClass.Id;
                if (Data.Instance.loggedUser.UserType == EUserType.PROFESSOR && selectedClass.State == EState.RESERVED)
                {
                    MessageBox.Show("Cannot delete reserved class");
                }
                else
                {
                    schoolClassService.Delete(id);
                    RefreshDataGrid();
                }
            }
        }
        private void BtnUnscheduleClass_Click(object sender, RoutedEventArgs e)
        {
            schoolClass = (SchoolClass)DgLoggedClasses.SelectedItem;
            if (schoolClass != null)
            {
                if (schoolClass.State == EState.RESERVED)
                {
                    schoolClass.State = EState.AVAILABLE;
                    schoolClass.Student = null;
                    schoolClassService.Update(schoolClass.Id, schoolClass);
                }
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("No Class is Selected");
            }
        }

        private void DgLoggedClasses_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            }
        }
        private void txtDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtDatePicker.SelectedDate != null)
            {
                DgLoggedClasses.ItemsSource = professorsClasses.Where(p => p.IsActive == true && (p.Date.Date == txtDatePicker.SelectedDate)).ToList();
            }
            else
            {
                if (Data.Instance.loggedUser.UserType == EUserType.PROFESSOR)
                {
                    professorsClasses = schoolClassService.GetAll().Where(p => p.IsActive == true && p.Professor.JMBG == Data.Instance.loggedUser.JMBG).ToList();
                    DgLoggedClasses.ItemsSource = professorsClasses;
                }
                else
                {
                    professorsClasses = schoolClassService.GetAll().Where(p => p.IsActive == true).ToList();
                    DgLoggedClasses.ItemsSource = professorsClasses;
                }
                DgLoggedClasses.ItemsSource = professorsClasses;
            }
        }
        private void btnClearDate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtDatePicker.SelectedDate = null;
        }
        #endregion

        //--------------------SCHOOL--------------------//
        #region School
        private void schoolLanguageSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //schoolSearch
            //DgSchools
            string searchedString = schoolLanguageSearch.Text.ToLower();
            string emptyString = "";
            List<User> user = new List<User>();
            List<User> searchedSchools = new List<User>();
            List<School> schools = schoolService.GetAll().Where(p => p.IsActive == true).ToList();
            List<User> professorListFilled = professorService.GetAll();

            foreach (School school in schools)
            {
                user = professorService.GetAll().Where(p => p.IsActive == true && p.SchoolId == school.Id).ToList();
                foreach (User user1 in professorListFilled)
                {
                    foreach (User user2 in user)
                    {
                        if (user1.Id == user2.Id)
                        {
                            foreach (Language language in user1.Languages)
                            {
                                if (language.Name.ToLower().Contains(searchedString))
                                {
                                    searchedSchools.Add(user2);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            List<School> listSchools = new List<School>();
            foreach (User user3 in searchedSchools)
            {
                foreach (School school in schools)
                {
                    if (school.Id == user3.SchoolId)
                    {
                        listSchools.Add(school);
                        break;
                    }
                }
            }
            if (SchoolLanguage(listSchools).Count() > schoolList.Count)
            {
                listSchools.Clear();
            }
            else
            {
                if (searchedString == emptyString)
                {
                    DgSchools.ItemsSource = listSchools;
                }
                else
                {
                    listLangUser.Clear();
                    DgSchools.ItemsSource = SchoolLanguage(listSchools);
                }
            }
        }
        public List<School> SchoolLanguage(List<School> schools)
        {
            List<School> newList = new List<School>();
            foreach (School school in schools)
            {
                if (!listLangUser.Contains(school.Id))
                {
                    listLangUser.Add(school.Id);
                    newList.Add(school);
                }
            }
            return newList;

        }
        private void DgSchools_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            school = (School)DgSchools.SelectedItem;
            DgProfessors.ItemsSource = professorService.GetAll().Where(p => p.School?.Id == school?.Id && (p.UserType == EUserType.PROFESSOR));
        }
        private void schoolSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DgSchools.ItemsSource = schoolService.GetAll().Where(p => p.IsActive == true && (
               p.Name.ToLower().Contains(schoolSearch.Text.ToLower())
            // pretraga po jeziku
            || p.Street.ToLower().Contains(schoolSearch.Text.ToLower())
            || p.City.ToLower().Contains(schoolSearch.Text.ToLower())
            || p.StreetNumber.ToLower().Contains(schoolSearch.Text.ToLower())
            || p.Country.ToLower().Contains(schoolSearch.Text.ToLower()))).ToList();
        }
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
            if (e.PropertyName == "Error" || e.PropertyName == "IsValid" || e.PropertyName == "IsActive")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        //--------------------PROFILE LOGIN REGISTER--------------------//
        #region Profile
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
            showButtons();
            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            User loggedUser = Data.Instance.loggedUser;
            if (loggedUser.JMBG == null)
            {
                MessageBox.Show("No User is Logged In");
                showButtons();
                
            }
            else
            {
                Data.Instance.loggedUser = new User();
                showButtons();
                AllTabs.SelectedItem = TiSchools;
                MessageBox.Show("Successfully Logged Out");
            }
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Data.Instance.loggedUser = Data.Instance.loggedUser;
            if (Data.Instance.loggedUser.JMBG != null)
            {
                if (Data.Instance.loggedUser.UserType == EUserType.STUDENT)
                {
                    var studentProfileWindow = new AddEditStudentsWindow(Data.Instance.loggedUser);
                    var successeful = studentProfileWindow.ShowDialog();
                    if ((bool)successeful)
                    {
                        RefreshDataGrid();
                    }
                }
                else if (Data.Instance.loggedUser.UserType == EUserType.PROFESSOR)
                {
                    var professorProfileWindow = new AddEditProfessorsWindow(Data.Instance.loggedUser);
                    var successeful = professorProfileWindow.ShowDialog();
                    if ((bool)successeful)
                    {
                        RefreshDataGrid();
                    }
                }
                else if (Data.Instance.loggedUser.UserType == EUserType.ADMIN)
                {
                    var adminProfileWindow = new AddEditProfessorsWindow(Data.Instance.loggedUser);
                    var successeful = adminProfileWindow.ShowDialog();
                    if ((bool)successeful)
                    {
                        RefreshDataGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        #endregion

        //--------------------ALL TABLES--------------------//
        #region AllTables
        private void ShowAllTables_Click(object sender, RoutedEventArgs e)
        {
            var showAllTables = new ShowAllTables();
            var successeful = showAllTables.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }
        #endregion

        public void showButtons()
        {
            if (Data.Instance.loggedUser.JMBG != null)
            {
                btnProfile.Visibility = Visibility.Visible;
                btnRegister.Visibility = Visibility.Collapsed;
                btnLogin.Visibility = Visibility.Collapsed;
                btnLogout.Visibility = Visibility.Visible;
                if (Data.Instance.loggedUser.UserType == EUserType.STUDENT)
                {
                    TiMyClasses.Visibility = Visibility.Visible;
                    BtnUnscheduleClass.Visibility = Visibility.Visible;
                    BtnAddClass.Visibility = Visibility.Collapsed;
                    BtnDeleteClass.Visibility = Visibility.Collapsed;
                    DateSearch.Visibility = Visibility.Collapsed;
                }
                else if (Data.Instance.loggedUser.UserType == EUserType.PROFESSOR)
                {
                    TiMyClasses.Visibility = Visibility.Visible;
                    BtnUnscheduleClass.Visibility = Visibility.Collapsed;
                    DateSearch.Visibility = Visibility.Visible;
                }
                else if (Data.Instance.loggedUser.UserType == EUserType.ADMIN)
                {
                    TiAllTables.Visibility = Visibility.Visible;
                }
            }
            else
            {
                btnRegister.Visibility = Visibility.Visible;
                TiMyClasses.Visibility = Visibility.Collapsed;
                btnProfile.Visibility = Visibility.Collapsed;
                btnLogin.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Collapsed;
                TiAllTables.Visibility = Visibility.Collapsed;
            }
        }
    }
}   

