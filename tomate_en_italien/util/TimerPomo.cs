using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using System.Windows.Controls;

namespace tomate_en_italien
{
    class TimerPomo
    {
        private string Name { get; }

        public Boolean pause { get; set; }

        private util.TypeTimer Type { get; }
        private DateTime DateStart { get; }
        private DateTime DateEnd { get; set; }
        private DateTime DatePause { get; set; }
        private Label LblChrono { get; set; }
        private ProgressBar ProgressBarTimeLeft { get; set; }


        public TimerPomo(string TimerName,int TimeInMinute, util.TypeTimer TimerType)
        {
            this.Name = TimerName;
            this.Type = TimerType;
            this.DateStart = DateTime.Now;
            this.DateEnd = DateTime.Now.AddMinutes(TimeInMinute);
            this.pause = false;
        }

        public void HandleChrono(System.Windows.Threading.DispatcherTimer dispatcherTimer, Label lblChrono, ProgressBar ProgressBarTimeLeft)
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateChronoLabel);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            this.LblChrono = lblChrono;
            this.ProgressBarTimeLeft = ProgressBarTimeLeft;

        }

        private void UpdateChronoLabel(object sender, EventArgs e)
        {
            if(!pause)
            {
                // Si il reste encore du temps dans le timer
                if (DateTime.Now.CompareTo(DateEnd) == -1)
                {
                    var Tl = DateEnd.Subtract(DateTime.Now);
                    // Met à jour le compteur
                    var TimeLeft = Tl.Minutes + ":" + Tl.Seconds;
                    this.LblChrono.Content = TimeLeft;
                    //Met à jour la ProgressBar
                    var timeLeftSeconds = Tl.Minutes * 60 + Tl.Seconds;
                    var timeSpend = 25 * 60 - timeLeftSeconds;
                    ProgressBarTimeLeft.Value = timeSpend * 100 / (25 * 60);
                }
                
            }

        }

        public void setPause(Button btnPause)
        {
            if (!this.pause)
            {
                this.pause = true;
                this.DatePause = DateTime.Now;
                btnPause.Content = "Play";
            } else
            {
                this.pause = false;
                var diff = DateTime.Now.Subtract(DatePause);
                DateEnd = DateEnd.Add(diff);
                btnPause.Content = "Pause";
            }
            
            
        }
    }
}
