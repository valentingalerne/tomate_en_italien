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

        /// <summary>
        /// Confirme le nombre de pomodoro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmNbPomodoro(object sender, RoutedEventArgs e)
        {
            int NbPomodoro = Int32.Parse(lblNb.Content.ToString());
            timeArray.Clear();
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

        /// <summary>
        /// Start le pomodoro ou le met en pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPomodoro(object sender, RoutedEventArgs e)
        {
            // Check si le timer est lancé
            if (!MonTimer.isStart())
            {
                btnPause.Content = "Pause";
                // On lance un timer
                MonDispatcheTimer = new DispatcherTimer();
                var time = 0;
                try
                {
                    time = timeArray[indexTimeArray];
                }
                catch (Exception)
                {
                    time = 25;
                }
                MonTimer = new TimerPomo(time, util.TypeTimer.Work);
                if (MonTimer.IsWork())
                {
                    // Ouvre une popup pour la séléction du nom
                    TimerName tname = new TimerName();
                    tname.Owner = this;
                    tname.Closed += new EventHandler(OnClosePomoSaisieName);
                    tname.Show();
                }
                MonTimer.HandleChrono(MonDispatcheTimer, lblView, ProgressBarTimeLeft);
            }
            else
            {
                MonTimer.setPause(btnPause);
            }
        }
        
        /// <summary>
        /// Change le nom du pomo à la fermeture de la fenêtre de saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosePomoSaisieName(object sender, EventArgs e)
        {
            PomoName.Content = monTimerName;
        }

        /// <summary>
        /// Passe au pomodoro suivant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPomodoro(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Reset le Pomodoro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetPomodoro(object sender, RoutedEventArgs e)
        {
            boolTimer = false;
            indexTimeArray = 0;
            MonTimer.resetTimer(25);
            MonTimer.setLabelChrono(lblView);
            btnNext.IsEnabled = true;
            btnPause.Content = "Play";
        }

        /// <summary>
        /// Augmente le nombre de pomodoro au click sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecreaseNbPomo(object sender, RoutedEventArgs e)
        {
            int NbPomodoro = Int32.Parse(lblNb.Content.ToString());
            if (NbPomodoro > 0)
            {
                lblNb.Content = NbPomodoro - 1;
            }
        }

        /// <summary>
        /// Réduit le nombre de pomodoro au click sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncreaseNbPomo(object sender, RoutedEventArgs e)
        {
            int NbPomodoro = Int32.Parse(lblNb.Content.ToString());
            lblNb.Content = NbPomodoro + 1;
        }
        
        /// <summary>
        /// Charge l'historique des pomodoro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadHistorique(object sender, RoutedEventArgs e)
        {
            viewHistorique.Items.Clear();
            var taskList = SqliteDbAccess.LoadTask();
            foreach (Task task in taskList)
            {
                viewHistorique.Items.Add($"{task.Libelle} {task.Count}");
            }
            if(taskList.Count == 0)
            {
                viewHistorique.Items.Add($"No task found");
            }
        }
    }
}
