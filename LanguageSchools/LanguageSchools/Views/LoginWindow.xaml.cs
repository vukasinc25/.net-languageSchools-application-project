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
    public partial class LoginWindow : Window
    {
        private IUserService userService = new UserService();
        private User user;
        private List<User> users = new List<User>();
        public LoginWindow()
        {
            InitializeComponent();
        }
        public User login(string jmbg, string password)
        {
            users = userService.GetActiveUsers();
            if (jmbg != null && password!= null)
            {
                foreach (User user in users)
                {
                    if (user.JMBG == jmbg && user.Password == password) { return user; }
                }
            }
            return null;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtJMBG.Text != "" || txtPassword.Text != "")
            {
                user = login(txtJMBG.Text, txtPassword.Text);
                if (user != null)
                {
                    Data.Instance.loggedUser = user;
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong Credidentails");
                }
            }
            else
            {
                if (txtJMBG.Text == "")
                {
                    MessageBox.Show("JMBG field is empty");
                }
                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Password field is empty");
                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
