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
    public partial class AddEditLanguagesWindow : Window
    {
        private Language language;
        private ILanguageService languageService = new LanguageService();
        private bool isAddMode;
        public AddEditLanguagesWindow()
        {
            InitializeComponent();
            language = new Language
            {
                IsActive = true,
            };
            isAddMode = true;
            DataContext= language;
        }
        public AddEditLanguagesWindow(Language language)
        {
            InitializeComponent();
            this.language = language.Clone() as Language;
            DataContext= this.language;
            isAddMode= false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtName.Text))
            {
                if (isAddMode)
                {
                    languageService.Add(language);
                }
                else
                {
                    languageService.Update(language.Id, language);
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
