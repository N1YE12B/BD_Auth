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
using BD_Auth;
using BD_Auth.DataBaseFolder;
using BD_Auth.Windows;

namespace BD_Auth
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        Entities context = new Entities();
        
        public AuthWindow()
        {
            InitializeComponent();
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private  void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(uLogin.Text))
            {
                MessageBox.Show("Введите Логин");
                return;
            }
            if(string.IsNullOrWhiteSpace(uPassword.Password))
            {
                MessageBox.Show("Введите Пароль");
                return;
            }

            string log = uLogin.Text;
            string pass = uPassword.Password;

            var user = context.Worker.Where(w => w.Login == uLogin.Text && w.Password == uPassword.Password).FirstOrDefault();

            switch(user.IdRole)
            {
                case 1:
                    AdminWindow aw = new AdminWindow();
                    this.Hide();
                    aw.ShowDialog();
                    this.Show();
                    uLogin.Text = string.Empty;
                    uPassword.Password = string.Empty;
                    break;
                case 2:
                    UserWindow usew = new UserWindow(log, pass);
                    this.Hide();
                    usew.ShowDialog();
                    this.Show();
                    uLogin.Text = string.Empty;
                    uPassword.Password = string.Empty;
                    break;
                case 3:
                    ManagerWindow mw = new ManagerWindow(log, pass);
                    this.Hide();
                    mw.ShowDialog();
                    this.Show();
                    uLogin.Text = string.Empty;
                    uPassword.Password = string.Empty;
                    break;
                default:
                    break;
            }





        }

        private void uPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void uLogin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
