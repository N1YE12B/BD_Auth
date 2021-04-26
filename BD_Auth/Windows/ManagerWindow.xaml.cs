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
using BD_Auth.DataBaseFolder;


namespace BD_Auth.Windows
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        Entities context = new Entities();
        public ManagerWindow(string Log, string Pass)
        {
            InitializeComponent();
            var user = context.Worker.Where(w => w.Login == Log && w.Password == Pass).FirstOrDefault();

            FIO.Text = $"{user.LastName} {user.FirstName} {user.Patronymic}";
            Login.Text = user.Login;
            Password.Text = user.Password;
            Birth.Text = Convert.ToString(user.BirthDate);
            Role.Text = user.Role.NameRole;

            Data.ItemsSource = context.Worker.ToList();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
