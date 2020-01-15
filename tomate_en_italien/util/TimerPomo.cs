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

        private util.TypeTimer Type { get; }
        private DateTime DateStart { get; }
        private DateTime DateEnd { get; }
        public string TimeLeft { get; set; }
        private Label lblChrono { get; set; }


        public TimerPomo(string TimerName, util.TypeTimer TimerType)
        {
            Name = TimerName;
            Type = TimerType;
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now.AddMinutes(25);
        }

        public void HandleChrono(System.Windows.Threading.DispatcherTimer dispatcherTimer, Label lblChrono)
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateChronoLabel);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            this.lblChrono = lblChrono;
        }

        private void UpdateChronoLabel(object sender, EventArgs e)
        {
            var Tl = DateEnd.Subtract(DateTime.Now);
            // if( < DateEnd)
            TimeLeft = Tl.Minutes + ":" + Tl.Seconds;
            this.lblChrono.Content = TimeLeft;
        }

        public void setPause()
        {

        }
    }
}
