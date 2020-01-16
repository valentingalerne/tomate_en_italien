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
        private Boolean Started { get; set; }
        public Boolean Pause { get; set; }
        private int TimerTime { get; set; }
        private util.TypeTimer Type { get; }
        private DateTime DateStart { get; set; }
        private DateTime DateEnd { get; set; }
        private DateTime DatePause { get; set; }
        private Label LblChrono { get; set; }
        private ProgressBar ProgressBarTimeLeft { get; set; }


        public TimerPomo(string TimerName, int TimeInMinute, util.TypeTimer TimerType)
        {
            this.Name = TimerName;
            this.Type = TimerType;
            this.Pause = false;
            this.TimerTime = TimeInMinute;
            this.Started = false;
        }

        public void setLabelChrono(Label lblChrono)
        {
            var Tl = DateEnd.Subtract(DateTime.Now);
            lblChrono.Content = TimerTime + ":00" ;
        }

        public void HandleChrono(System.Windows.Threading.DispatcherTimer dispatcherTimer, Label lblChrono, ProgressBar ProgressBarTimeLeft)
        {   
            this.DateStart = DateTime.Now;
            this.DateEnd = DateTime.Now.AddMinutes(this.TimerTime);
            this.Started = true;
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateChronoLabel);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            this.LblChrono = lblChrono;
            this.ProgressBarTimeLeft = ProgressBarTimeLeft;

        }

        private void UpdateChronoLabel(object sender, EventArgs e)
        {
            if(!Pause)
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
                    var timeSpend = this.TimerTime * 60 - timeLeftSeconds;
                    ProgressBarTimeLeft.Value = timeSpend * 100 / (this.TimerTime * 60);
                }
                
            }

        }

        public void setPause(Button btnPause)
        {
            if (!this.Pause)
            {
                this.Pause = true;
                this.DatePause = DateTime.Now;
                btnPause.Content = "Play";
            } else
            {
                this.Pause = false;
                var diff = DateTime.Now.Subtract(DatePause);
                DateEnd = DateEnd.Add(diff);
                btnPause.Content = "Pause";
            }
            
            
        }

        public Boolean isStart()
        {
            return this.Started;
        }
    }
}
