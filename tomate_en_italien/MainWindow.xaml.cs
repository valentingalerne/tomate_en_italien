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
using Task = tomate_en_italien.util.Task;

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
        public Boolean boolTimer = false;
        public List<int> timeArray = new List<int>();
        public int indexTimeArray = 0;

        public MainWindow()
        {
            InitializeComponent();
            MonDispatcheTimer = new System.Windows.Threading.DispatcherTimer();
            MonTimer = new TimerPomo(25, util.TypeTimer.Work);
            MonTimer.setLabelChrono(lblView);

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int NbPomodoro = Int32.Parse(lblNb.Content.ToString());
            for (int i = 1; i < NbPomodoro + 1; i++)
            {
                timeArray.Add(25);
                if (i % 4 == 0)
                {
                    timeArray.Add(15);
                }
                else
                {
                    timeArray.Add(5);
                }
            }
            lblNbPomodoroValue.Content = NbPomodoro;
            TabControl.SelectedItem = TabItemRun;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
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

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var hasNext = true;
            if (indexTimeArray < timeArray.Count - 1 && boolTimer == false)
            {
                indexTimeArray++;
                lblView.Content = timeArray[indexTimeArray];
                MonTimer.resetTimer(timeArray[indexTimeArray]);
                MonTimer.setLabelChrono(lblView);
                btnPause.Content = "Play";
                if (indexTimeArray == timeArray.Count - 1)
                {
                    hasNext = false;
                }
            }
            else
            {
                boolTimer = true;
                indexTimeArray = 0;
            }
            btnNext.IsEnabled = hasNext;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            boolTimer = false;
            indexTimeArray = 0;
            MonTimer.resetTimer(timeArray[indexTimeArray]);
            MonTimer.setLabelChrono(lblView);
            btnNext.IsEnabled = true;
            btnPause.Content = "Play";
        }

        private void btnRemovePomodoro_Click(object sender, RoutedEventArgs e)
        {
            int NbPomodoro = Int32.Parse(lblNb.Content.ToString());
            if (NbPomodoro > 0)
            {
                lblNb.Content = NbPomodoro - 1;
            }
        }

        private void btnAddPomodoro_Click(object sender, RoutedEventArgs e)
        {
            int NbPomodoro = Int32.Parse(lblNb.Content.ToString());
            lblNb.Content = NbPomodoro + 1;
        }
    }
}
