using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using System.Windows.Controls;

namespace tomate_en_italien
{
    /// <summary> Classe TimerPomo </summary>
    class TimerPomo
    {
        /// <summary> Attribut Name </summary>
        public string Name { get; set;}
        /// <summary> Attribut Started </summary>
        private Boolean Started { get; set; }
        /// <summary> Attribut Pause </summary>
        public Boolean Pause { get; set; }
        /// <summary> Attribut TimerTime </summary>
        private int TimerTime { get; set; }
        /// <summary> Attribut Type </summary>
        private util.TypeTimer Type { get; }
        /// <summary> Attribut DateStart </summary>
        private DateTime DateStart { get; set; }
        /// <summary> Attribut DateEnd </summary>
        private DateTime DateEnd { get; set; }
        /// <summary> Attribut DatePause </summary>
        private DateTime DatePause { get; set; }
        /// <summary> Attribut LblChrono </summary>
        private Label LblChrono { get; set; }
        /// <summary> Attribut ProgressBarTimeLeft </summary>
        private ProgressBar ProgressBarTimeLeft { get; set; }
        /// <summary> Attribut dispatcherTimer </summary>
        private DispatcherTimer dispatcherTimer { get; set; }


        /// <summary> Constructeur de la classe TimerPomo </summary>
        /// <param name="TimerName"></param>
        /// <param name="TimeInMinute"></param>
        /// <param name="TimerType"></param>
        public TimerPomo(int TimeInMinute, util.TypeTimer TimerType)
        {
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

        public void HandleChrono(DispatcherTimer dispatcherTimer, Label lblChrono, ProgressBar ProgressBarTimeLeft)
        {
            this.DateStart = DateTime.Now;
            this.DateEnd = DateTime.Now.AddMinutes(this.TimerTime);
            this.Started = true;
            this.dispatcherTimer = dispatcherTimer;
            this.dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.dispatcherTimer.Tick += new EventHandler(UpdateChronoLabel);
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.dispatcherTimer.Start();
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

        public void resetTimer()
        {
            this.dispatcherTimer.Stop();
            this.ProgressBarTimeLeft.Value = 0;
            this.Started = false;
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
