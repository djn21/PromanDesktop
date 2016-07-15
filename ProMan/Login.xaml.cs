using ProjectManager.dao;
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

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static string username="";
        public static string password="";

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Password;
            bool response=(bool)WebService.callFunction("login", username, password);
            if (response)
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                lblIncorect.Content = "Incorrect username or password.";
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                username = txtUsername.Text;
                password = txtPassword.Password;
                bool response = (bool)WebService.callFunction("login", username, password);
                if (response)
                {  
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    lblIncorect.Content = "Incorrect username or password.";
                }
            }
        }
    }
}
