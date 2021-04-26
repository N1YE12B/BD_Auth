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
using BD_Auth.Windows;

namespace BD_Auth
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        Entities context = new Entities();

        public AdminWindow()
        {
            InitializeComponent();
            Data.ItemsSource = context.Worker.ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow auw = new AddUserWindow();
            this.Close();
            auw.ShowDialog();
        }

       

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItems.Count > 0)
            {
                var result = MessageBox.Show("Удалить клиента?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (Data.SelectedItem is Worker client)
                    {
                        context.Worker.Remove(context.Worker.Where(i => i.IdWorker == client.IdWorker).FirstOrDefault());
                        context.SaveChanges();
                        Data.ItemsSource = context.Worker.ToList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись клиента!");
                return;
            }
        }
    }
}
