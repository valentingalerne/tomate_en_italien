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
        private static DispatcherTimer MonDispatcheTimer;
        private static TimerPomo MonTimer;
        public String monTimerName;

        public MainWindow()
        {
            InitializeComponent();
            MonDispatcheTimer = new System.Windows.Threading.DispatcherTimer();
            MonTimer = new TimerPomo(15, util.TypeTimer.Work);
            MonTimer.setLabelChrono(lblView);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Si aucun timer lancé
            if (!MonTimer.isStart())
            {
                btnPause.Content = "Pause";
                // On lance un timer
                MonDispatcheTimer = new System.Windows.Threading.DispatcherTimer();
                MonTimer = new TimerPomo(25, util.TypeTimer.Work);
                // Ouvre une popup pour la séléction du nom
                TimerName tname = new TimerName();
                tname.Owner = this;
                tname.Closed += new EventHandler(tname_Closed);
                tname.Show();

                MonTimer.HandleChrono(MonDispatcheTimer, lblView, ProgressBarTimeLeft);
            }
            else
            {
                MonTimer.setPause(btnPause);
            }
        }

        // Lors de la fermeture de tname
        private void tname_Closed(object sender, EventArgs e)
        {
            name.Content = monTimerName;
            MonTimer.Name = monTimerName;
        }
    }
}
