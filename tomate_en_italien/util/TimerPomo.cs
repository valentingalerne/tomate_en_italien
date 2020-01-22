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
        /// <summary> Attribut Started </summary>
        private Boolean Started { get; set; }
        /// <summary> Attribut Pause </summary>
        public Boolean Pause { get; set; }
        /// <summary> Attribut TimerTime </summary>
        private int TimerTime { get; set; }
        /// <summary> Attribut Type </summary>
        private util.TypeTimer Type { get; set; }
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
        /// <param name="TimeInMinute">Temps du chrono</param>
        /// <param name="TimerType">Type de timer (Deprecated)</param>
        public TimerPomo(int TimeInMinute, util.TypeTimer TimerType)
        {
            // TODO retirer le timerType du constructeur
            this.Pause = false;
            this.TimerTime = TimeInMinute;
            this.Started = false;
            this.Type = GetTimerType();
        }

        /// <summary>
        /// CHange le label du chrono
        /// </summary>
        /// <param name="lblChrono"></param>
        public void setLabelChrono(Label lblChrono)
        {
            var Tl = DateEnd.Subtract(DateTime.Now);
            lblChrono.Content = TimerTime + ":00";
        }

        /// <summary>
        /// Gère l'affichage des données lorsque le chrono se lance
        /// </summary>
        /// <param name="dispatcherTimer"></param>
        /// <param name="lblChrono"></param>
        /// <param name="ProgressBarTimeLeft"></param>
        public void HandleChrono(DispatcherTimer dispatcherTimer, Label lblChrono, ProgressBar ProgressBarTimeLeft)
        {
            this.DateStart = DateTime.Now;
            this.DateEnd = DateTime.Now.AddMinutes(this.TimerTime).AddSeconds(1);
            this.Started = true;
            this.dispatcherTimer = dispatcherTimer;
            this.dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.dispatcherTimer.Tick += new EventHandler(UpdateChronoLabel);
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.dispatcherTimer.Start();
            this.LblChrono = lblChrono;
            this.ProgressBarTimeLeft = ProgressBarTimeLeft;

        }

        /// <summary>
        /// Récupération du type de chrono en fonction du temps set
        /// </summary>
        /// <returns>util.TypeTimer</returns>
        private util.TypeTimer GetTimerType()
        {
            util.TypeTimer typeTimer;
            switch(this.TimerTime)
            {
                case 5:
                    typeTimer = util.TypeTimer.Short_Break;
                    break;
                case 15:
                    typeTimer = util.TypeTimer.Long_Break;
                    break;
                case 25:
                    typeTimer = util.TypeTimer.Work;
                    break;
                default:
                    typeTimer = util.TypeTimer.Pause;
                    break;
            }
            return typeTimer;
        }

        /// <summary>
        /// Check si le timer est set sur work
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean IsWork()
        {
            return this.Type == util.TypeTimer.Work;
        }

        /// <summary>
        /// Update le label du chrono/ la progressbar s'il est pas en pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateChronoLabel(object sender, EventArgs e)
        {
            if (!Pause)
            {
                // Si il reste encore du temps dans le timer
                if (DateTime.Now.CompareTo(DateEnd) == -1)
                {
                    var Tl = DateEnd.Subtract(DateTime.Now);
                    // Met à jour le compteur
                    var TimeLeft = Tl.Minutes + ":" + Tl.Seconds.ToString("00");
                    this.LblChrono.Content = TimeLeft;
                    //Met à jour la ProgressBar
                    var timeLeftSeconds = Tl.Minutes * 60 + Tl.Seconds;
                    var timeSpend = this.TimerTime * 60 - timeLeftSeconds;
                    ProgressBarTimeLeft.Value = timeSpend * 100 / (this.TimerTime * 60);
                }

            }

        }

        /// <summary>
        /// Stop puis reset le temps du timer et la progress bar
        /// </summary>
        /// <param name="TimeInMinute"></param>
        public void resetTimer(int TimeInMinute)
        {
            this.Type = GetTimerType();
            if (this.dispatcherTimer != null)
            {
                this.dispatcherTimer.Stop();
            }
            if(this.ProgressBarTimeLeft != null)
            {
                this.ProgressBarTimeLeft.Value = 0;
            }
            this.Pause = false;
            this.Started = false;
            this.TimerTime = TimeInMinute;
        }

        /// <summary>
        /// Mets le chrono en pause
        /// </summary>
        /// <param name="btnPause"></param>
        public void setPause(Button btnPause)
        {
            if (!this.Pause)
            {
                this.Pause = true;
                this.DatePause = DateTime.Now;
                btnPause.Content = "Play";
            }
            else
            {
                this.Pause = false;
                var diff = DateTime.Now.Subtract(DatePause);
                DateEnd = DateEnd.Add(diff);
                btnPause.Content = "Pause";
            }

        }

        /// <summary>
        /// Check si le chrono est lancé
        /// </summary>
        /// <returns></returns>
        public Boolean isStart()
        {
            return this.Started;
        }
    }
}
