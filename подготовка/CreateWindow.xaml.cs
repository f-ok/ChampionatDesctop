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
    /// Логика взаимодействия для CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        public CreateWindow()
        {
            InitializeComponent();
            LoadCombos();
        }
        private void LoadCombos()
        {
            // здесь можно подгрузить варианты из БД (фирмы, статусы и т.п.)
            // для примера cbCompany уже имеет статический список в XAML
            cbCompany.ItemsSource = ПодготовкаEntities.GetContext().vending_machines.Select(v => v.company).Where(c => c != null).Distinct().OrderBy(c => c).ToList();

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var db = ПодготовкаEntities.GetContext();
            // простая валидация
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Введите название ТА");
                return;
            }

            if (cbCompany.SelectedItem == null)
            {
                MessageBox.Show("Выберите производителя ТА");
                return;
            }

            // пример формирования строки payment_type
            string paymentType = string.Join(", ",
                new[]
                {
                    cbCash.IsChecked == true ? "Наличные" : null,
                    cbCard.IsChecked == true ? "Карта" : null,
                    cbQr.IsChecked   == true ? "QR" : null
                }.Where(s => s != null));

            if (string.IsNullOrEmpty(paymentType))
            {
                MessageBox.Show("Выберите хотя бы один способ оплаты");
                return;
            }

            var vm = new vending_machines();
            /*
            // создание сущности
            var vm = new vending_machines
            {
                name = tbName.Text.Trim(),
                company = (cbCompany.SelectedItem as ComboBoxItem)?.Content.ToString(),
                model = tbModel.Text.Trim(),
                location = tbAddress.Text.Trim(),
                place = tbPlace.Text.Trim(),
                work_mode = (cbWorkMode.SelectedItem as ComboBoxItem)?.Content.ToString(),
                payment_type = paymentType,
                working_hours = tbWorkingHours.Text.Trim(),
                manager = tbManager.Text.Trim(),
                notes = tbNotes.Text.Trim(),
                serial_number = tbSerial.Text.Trim(),
                status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString(),
                timezone = (cbTimezone.SelectedItem as ComboBoxItem)?.Content.ToString(),
                install_date = dpInstallDate.SelectedDate.ToString(),   // если в БД тип date/datetime
                total_income = ""    // новый аппарат — без дохода
            };
            */
            try
            {
                
                
                    // проверка уникальности серийного номера
                    bool existsSerial = db.vending_machines
                        .Any(x => x.serial_number == vm.serial_number);

                    if (existsSerial)
                    {
                        MessageBox.Show("ТА с таким серийным номером уже существует");
                        return;
                    }

                    db.vending_machines.Add(vm);
                    db.SaveChanges();
                

                MessageBox.Show("Торговый автомат успешно создан");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
