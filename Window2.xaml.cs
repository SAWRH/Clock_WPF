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

    public partial class Window2: Window
    {
        public Window BaseWindow=null;

        
        
        public Window2()
        {
            InitializeComponent();
            
        }
        

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = calendar.SelectedDate;
            MainWindow.currentdate=(DateTime)selectedDate;
 
            MessageBox.Show(selectedDate.Value.Date.ToShortDateString());
        }
        bool move_cal = false;
        Point calPosition;
        private void Window2_MouseDown(object sender, MouseEventArgs e)
        {
            calPosition = e.GetPosition(this);

            // Разрешаем движение
            move_cal = true;

            //  мышь не теряет окно, даже если оно под др окнами.
            this.CaptureMouse();
        }
        private void Window2_MouseUp(object sender, MouseEventArgs e)
        {
            move_cal = false;
            this.ReleaseMouseCapture();
        }
        private void Window2_MouseMove(object sender, MouseEventArgs e)
        {
            if (move_cal == true)
            {
                // разница между бывш и тек полож
                double deltaX = e.GetPosition(this).X - calPosition.X;
                double deltaY = e.GetPosition(this).Y - calPosition.Y;


                // положение окна ставим на вычисленную разницу
                this.Left += deltaX;
                this.Top += deltaY;
            }
            cencelCal.Visibility = Visibility.Visible;
            open_wathc.Visibility = Visibility.Visible;

        }
        private void Window2_MouseLeave(object sender, MouseEventArgs e)
        {
            int counter2=6;
            counter2--;
            if(counter2==0)
            {
                cencelCal.Visibility = Visibility.Hidden;
                open_wathc.Visibility = Visibility.Hidden;
            }
        }
        
        private void CencelCal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            BaseWindow.Close();
        }
        private void Open_Watch (object sender, RoutedEventArgs e)
        {
            this.Close();
            if (BaseWindow!=null)
                BaseWindow.Visibility=Visibility.Visible;
        }
    }
}
