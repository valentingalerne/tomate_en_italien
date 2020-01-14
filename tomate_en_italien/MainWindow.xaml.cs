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
                var time = end.Subtract(DateTime.Now);
                lblView.Content = String.Concat(time.Minutes, ":", time.Seconds);
            } else {
                lblView.Content = "C'est fini !";
            }

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
