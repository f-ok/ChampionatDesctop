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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private Point? _movePoint;

        public Page1()
        {
            InitializeComponent();
            UpdateGauge();

        }
        private void Btn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _movePoint = e.GetPosition(btn);
            btn.CaptureMouse();
        }

        private void Btn_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _movePoint = null;
            btn.ReleaseMouseCapture();
        }




        

        private void Btn_OnMouseMove(object sender, MouseEventArgs e)
        { 
 
            if (_movePoint == null)
                return;
            //передвижение лейбла
            var p = e.GetPosition(this) - (Vector)_movePoint.Value;
                Canvas.SetLeft(btn, p.X);
                Canvas.SetTop(btn, p.Y);
            
            //передвижение зеленой дуги
            Canvas.SetLeft(btn1, (p.X+50));
            Canvas.SetTop(btn1, (p.Y+60));

            //передвежиние стрелки
            Canvas.SetLeft(Needle, (p.X + 150));
            Canvas.SetTop(Needle, (p.Y + 60));

            //перемещение текстового блока
            Canvas.SetLeft(Text, (p.X));
            Canvas.SetTop(Text, (p.Y + 147));

        }

        public void UpdateGauge()
        {
            double percent = DataClass.GetWorkingPercent();  // метод для получения процента работающих ТА

            // ограничим на всякий случай 0–100
            percent = Math.Max(0, Math.Min(100, percent));

            // перевод процента в угол: 0% -> -90, 100% -> +90
            double angle = -90 + percent * 180.0 / 100.0;

            NeedleRotate.Angle = angle;   // повернули стрелку

            Text.Text = "Работающих автоматов: " + percent + " %";
        }

    }
}
