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

namespace подготовка
{
    /// <summary>
    /// Логика взаимодействия для dropDownPage.xaml
    /// </summary>
    public partial class dropDownPage : Page
    {

        Window1 a = new Window1();


        public dropDownPage()
        {
            InitializeComponent();


            btn.Content = a.GetUser();

        }

        
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Menu.Visibility = this.Menu.Visibility == Visibility.Visible
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }

        
    }
}
