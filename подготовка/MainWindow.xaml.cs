using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 




    public partial class MainWindow : Window
    {
        string name;

        

        public string NameOfUser { get { return name; } set {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("NameOfUser"); // Уведомляем UI об изменении
                }
            } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            //попытка сделать авторизацию не получилось
            Window1 window1 = new Window1();
            window1.ShowDialog();
            

            InitializeComponent();

            MainFrame.Navigate(new Main());
            Manager.MainFrame = MainFrame;

            DropDownPage.Navigate(new dropDownPage());
            ManagerOfDropDown.Menu = DropDownPage;

        }



        private void Administration_Click(object sender, RoutedEventArgs e)
        {
            this.asd.Visibility = this.asd.Visibility == Visibility.Visible
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }

        private void TMC_Click(object sender, RoutedEventArgs e)
        {
            xey.Height = GridLength.Auto;
            this.dsa.Visibility = this.dsa.Visibility == Visibility.Visible
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }

        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Panel.Height = GridLength.Auto;
            this.panell.Visibility = this.panell.Visibility == Visibility.Visible
                               ? Visibility.Collapsed
                               : Visibility.Visible;
        }
        */

        public double GetActualHeight()
        {
            return MainFrame.ActualHeight;
        }
        public double GetActualWidth()
        {
            return MainFrame.Width; 
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        static bool bl = true;
        private void TA_Click(object sender, RoutedEventArgs e)
        {
            
            if (AdminFrame.Visibility == Visibility.Hidden)
            {
                if (bl)
                {
                    AdminFrame.Navigate(new AdminTA());
                    ManagerOfTA.FrameOfAdminTA = AdminFrame;
                    bl = false;
                }

                AdminFrame.Visibility = Visibility.Visible;
                MainFrame.Visibility = Visibility.Hidden;
                
            }
            
            
            

            if (nameOfPage.Content.ToString().ToLower() != "Администрация / торговые автоматы")
            {
                nameOfPage.Content = "Администрация / торговые автоматы";
            }
            

        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Visibility == Visibility.Hidden)
            {
                
                MainFrame.Visibility= Visibility.Visible;
                AdminFrame.Visibility = Visibility.Hidden;
            }
            
                
            if (nameOfPage.Content.ToString().ToLower() != "главная")
            {
                nameOfPage.Content = "Главная";
            }

        }

    }
}
