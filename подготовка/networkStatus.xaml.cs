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
    /// Логика взаимодействия для networkStatus.xaml
    /// </summary>
    public partial class networkStatus : Page
    {
        private Point? _movePoint;
        public networkStatus()
        {
            InitializeComponent();

            UpdateNetworkState();
        }

        //передвежение
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

            //для диаграммы
            Canvas.SetLeft(RingCanvas, p.X);
            Canvas.SetTop(RingCanvas, p.Y+30);
        }




        //для диаграммы
        private void DrawRing(double pWork, double pBroken, double pService)
        {
            

            double cx = 75;   // центр
            double cy = 75;
            double rOuter = 60; // внешний радиус
            double rInner = 40; // внутренний радиус

            // вспомогательный метод
            void AddSegment(double startAngle, double sweepAngle, Brush brush)
            {
                if (sweepAngle <= 0) return;

                // угол в радианы
                double sa = startAngle * Math.PI / 180.0;
                double ea = (startAngle + sweepAngle) * Math.PI / 180.0;

                // точки внешней дуги
                Point p1 = new Point(cx + rOuter * Math.Cos(sa), cy + rOuter * Math.Sin(sa));
                Point p2 = new Point(cx + rOuter * Math.Cos(ea), cy + rOuter * Math.Sin(ea));

                // точки внутренней дуги
                Point p3 = new Point(cx + rInner * Math.Cos(ea), cy + rInner * Math.Sin(ea));
                Point p4 = new Point(cx + rInner * Math.Cos(sa), cy + rInner * Math.Sin(sa));

                bool largeArc = sweepAngle > 180;

                var fig = new PathFigure { StartPoint = p1, IsClosed = true };

                fig.Segments.Add(new ArcSegment
                {
                    Point = p2,
                    Size = new Size(rOuter, rOuter),
                    IsLargeArc = largeArc,
                    SweepDirection = SweepDirection.Clockwise
                });

                fig.Segments.Add(new LineSegment { Point = p3 });

                fig.Segments.Add(new ArcSegment
                {
                    Point = p4,
                    Size = new Size(rInner, rInner),
                    IsLargeArc = largeArc,
                    SweepDirection = SweepDirection.Counterclockwise
                });

                var geom = new PathGeometry();
                geom.Figures.Add(fig);

                var path = new Path
                {
                    Data = geom,
                    Fill = brush,
                    Stroke = Brushes.Transparent
                };

                RingCanvas.Children.Add(path);
            }

            double angle = -90; // начинаем сверху
            AddSegment(angle, pWork * 360.0 / 100.0, Brushes.LimeGreen); // Работает
            angle += pWork * 360.0 / 100.0;

            AddSegment(angle, pBroken * 360.0 / 100.0, Brushes.Red);     // Не работает
            angle += pBroken * 360.0 / 100.0;

            AddSegment(angle, pService * 360.0 / 100.0, Brushes.Gold);   // На обслуживании

            // внутренняя белая окружность (дырка) поверх секторов
            var inner = new Ellipse
            {
                Width = rInner * 2,
                Height = rInner * 2,
                Fill = Brushes.White
            };
            Canvas.SetLeft(inner, cx - rInner);
            Canvas.SetTop(inner, cy - rInner);
            RingCanvas.Children.Add(inner);
        }

        private void UpdateNetworkState()
        {

            var (pWork, pBroken, pService) = DataClass.GetStatusPercents();
            DrawRing(pWork, pBroken, pService);
        }

    }
}
