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
    /// Логика взаимодействия для AdminTA.xaml
    /// </summary>
    public partial class AdminTA : Page
    {
        public AdminTA()
        {
            InitializeComponent();


            Numbers.ItemsSource = Enumerable.Range(1, 100);

            DGVendingMachines.ItemsSource = ПодготовкаEntities.GetContext().vending_machines.ToList();

            CountOfTA.Content = "Всего найдено " + DGVendingMachines.Items.Count + " шт";
        }
        private void Numbers_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var win = new CreateWindow();
            bool? result = win.ShowDialog();


        }
    }
}
