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

namespace tomate_en_italien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DateTime end = DateTime.Now.AddMinutes(25);
        private static Boolean pause = false;
        private static DateTime pauseTime = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();

            //  DispatcherTimer setup
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e) {

            // Updating the Label which displays the timer
            if (DateTime.Now.CompareTo(end) == -1) { 
                if (pause == false) {
                    var time = end.Subtract(DateTime.Now);
                    lblView.Content = String.Concat(time.Minutes, ":", time.Seconds);
                }
            } else {
                lblView.Content = "C'est fini !";
            }

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pause == false) {
                pause = true;
                pauseTime = DateTime.Now;
                lblPause.Content = "Play";
            } else { 
                pause = false;
                var diff = DateTime.Now.Subtract(pauseTime);
                end = end.Add(diff);
                lblPause.Content = "Pause";
            }
        }
    }
}
