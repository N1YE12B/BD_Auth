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
using BD_Auth;
using System.Text.RegularExpressions;
using BD_Auth.DataBaseFolder;



namespace BD_Auth.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {

        Entities context = new Entities();

        public AddUserWindow()
        {
            InitializeComponent();
            
        }

        private void FName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
        

        

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
           
            AdminWindow admin = new AdminWindow();
            this.Close();
            admin.ShowDialog();
        }

        private void LName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        private void Patr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (int.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        private void Birth_GotFocus(object sender, RoutedEventArgs e)
        {
            Birth.Clear();
        }

        private void Birth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Birth.Text == string.Empty)
            {
                Birth.Text = "ГГ-ММ-ДД";
            }
        }

        private void Login_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        //Main button
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(FName.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if(string.IsNullOrWhiteSpace(LName.Text))
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            if(string.IsNullOrWhiteSpace(Patr.Text))
            {
                Patr.Text = null;
            }


            //Date def

            string bday = Birth.Text;
            string bdayPattern = @"[0-9]{4}-[0-9]{2}-[0-9]{2}";
            bool bdayCheck = Regex.IsMatch(bday, bdayPattern);

            bday = bday.Substring(5);
            bday = bday.Substring(0, bday.Length - 3);

            string byear = Birth.Text;
            byear = byear.Substring(0, byear.Length - 6);

            string byday = Birth.Text;
            byday = byday.Substring(8);


            int datday = Convert.ToInt32(byday);
            int datye = Convert.ToInt32(byear);
            int dat = Convert.ToInt32(bday);
            if (datye < 2021)
            {
                if (dat < 13)
                {
                    if (datday < 32)
                    {
                        if (bdayCheck == false)
                        {
                            MessageBox.Show("Неверный формат даты рождения");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат дня");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат месяца");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Неверный формат года");
                return;
            }

            if(string.IsNullOrWhiteSpace(Login.Text))
            {
                MessageBox.Show("Введите Логин");
                return;
            }
            if(string.IsNullOrWhiteSpace(Password.Text))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if(rb1.IsChecked == false && rb2.IsChecked == false)
            {
                MessageBox.Show("Выберите пол");
                return;
            }

            int genderCode = 0;
            if(rb1.IsChecked == true)
            {
                genderCode = 1;
            }
            if(rb2.IsChecked == true)
            {
                genderCode = 2;
            }

            
            if(string.IsNullOrWhiteSpace(Role.Text))
            {
                MessageBox.Show("Выберите роль");
                return;
            }
            string role = Role.Text;
            int roleCode =0;

            if(role == "Администратор")
            {
                roleCode = 1;
            }

            if(role == "Менеджер")
            {
                roleCode = 3;
            }

            if(role == "Сотрудник")
            {
                roleCode = 2;
            }

            DateTime D = DateTime.Parse(Birth.Text);
            context.Worker.Add(new Worker
            {
                
                LastName = LName.Text,
                FirstName = FName.Text,
                
                Patronymic = Patr.Text,
                Login = Login.Text,
                Password = Password.Text,
                IdRole = roleCode,
                IdGender = genderCode,
                BirthDate = D
               

            });
            context.SaveChanges();
            MessageBox.Show("Пользователь добавлен!");

        }
    }
}
