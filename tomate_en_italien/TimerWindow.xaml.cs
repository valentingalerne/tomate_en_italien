using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace tomate_en_italien
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class TimerWindow : Window
    {

        private static DispatcherTimer MonDispatcheTimer;
        private static TimerPomo MonTimer;
        public TimerWindow()
        {
            InitializeComponent();
            MonDispatcheTimer = new System.Windows.Threading.DispatcherTimer();
            MonTimer = new TimerPomo("Développement", 15, util.TypeTimer.Work);
            MonTimer.setLabelChrono(lblView);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Si toujours pas de timer de lancé
            if (MonDispatcheTimer == null)
            {
                btnPause.Content = "Pause";
                // On lance un timer
                MonDispatcheTimer = new System.Windows.Threading.DispatcherTimer();
                MonTimer = new TimerPomo("Développement", 1, util.TypeTimer.Work);
                MonTimer.HandleChrono(MonDispatcheTimer, lblView, ProgressBarTimeLeft);
            }
            else
            {
                MonTimer.setPause(btnPause);
            }
        }
    }
}
