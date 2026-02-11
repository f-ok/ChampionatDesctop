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

namespace подготовка
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string id, password;

        private string fullNameUser = "null"; private string role;

        public Window1()
        {
            InitializeComponent();
            
        }






        private void Button_Click(object sender, RoutedEventArgs e)
        {
            id = nameOfUser.Text;
            password = passwordOfUser.Text;

            Login login = new Login(id, password);

            var user = login.Logining();
            if (user != null)
            {
                // успешный вход
                fullNameUser = user.full_name;
                role = user.role;

                MessageBox.Show("Вход успешен");

                this.Close();



            }
            else
            {
                MessageBox.Show("Неверный id или пароль");
            }
        }
            public string GetUser()
        {
            return id;
        }
        public string GetPassword()
        {
            return password;
        }
        public string GetfullNameUser()
        {
            return fullNameUser;
        }
        public string GetRole()
        {
            return role;
        }


    }
}
