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
using System.Windows.Threading;

namespace clock
{
    public partial class MainWindow : Window
    {
        const double NumberSystem = 60;
        const double baseAngleNumberSystem = 360 / NumberSystem;
        const double baseAngleHour = 30;
        int counter=0;
        public static DateTime currentdate;

        public MainWindow()
        {
            InitializeComponent();
            StartClock();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var rotateSecondArrow = new RotateTransform();
            var rotateMinuteArrow = new RotateTransform();
            var rotateHourArrow = new RotateTransform();


            // текущ время.
            int sec = DateTime.Now.Second;
            int min = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;



            // угол для секундной стрелки.
            rotateSecondArrow.Angle = baseAngleNumberSystem * sec;

            // Вращение стрелки на вычисленный угол.
            SecondArrow.RenderTransform = rotateSecondArrow;



            // Угол минутной стрелки от количества полных минут +
            // угол секунд приведенный к долям текущей минуты.
            rotateMinuteArrow.Angle = (min * baseAngleNumberSystem) + (rotateSecondArrow.Angle / 60.0);

            MinuteArrow.RenderTransform = rotateMinuteArrow;



            // час в 12 час вид,
            //  угол полных часов +
            // угол минут приведенный к долям текущего часа.
            rotateHourArrow.Angle = (hour - 12) * baseAngleHour + rotateMinuteArrow.Angle / 12;

            HourArrow.RenderTransform = rotateHourArrow;



            
            counter--;
            if (counter==0)
            {
                cencel.Visibility = Visibility.Hidden;
                open_calc.Visibility = Visibility.Hidden;
            }
           
        }

        bool move = false;
        Point constPosition;



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            
            // фикс неизм позицию.
            constPosition = e.GetPosition(this);

            // Разрешаем движение
            move = true;

            //  мышь не теряет окно, даже если оно под др окнами.
            this.CaptureMouse();
            
           
            
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

            cencel.Visibility = Visibility.Visible;
            open_calc.Visibility = Visibility.Visible;
            if (move == true)
            {
                // разница между бывш и тек полож
                double deltaX = e.GetPosition(this).X - constPosition.X;
                double deltaY = e.GetPosition(this).Y - constPosition.Y;


                // положение окна ставим на вычисленную разницу
                this.Left += deltaX;
                this.Top += deltaY;
            }


        }
        private void MouseOut(object sender,MouseEventArgs e)
        {
            counter=6;
            
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {

            // Запрещ движ при отпускании мыши
            move = false;
            this.ReleaseMouseCapture();

        }

        void StartClock()
        {
            // скрываем окно до корректировки
            // После Timer_Tick врубаем
            this.Hide();

            var timer = new DispatcherTimer(); // use system.threading
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
            this.Show();
            //string crdateprs = currentdate.ToString();
            //date_face.Text=crdateprs;
        }

        private void Cencel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void OpenCal_Click(object sender, RoutedEventArgs e)
        {
            Window2 win =new  Window2();
            win.BaseWindow = this;
            win.Show();
            Visibility=Visibility.Hidden;
            
        } 
    }
}
