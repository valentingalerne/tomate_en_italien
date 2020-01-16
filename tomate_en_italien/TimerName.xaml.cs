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
using System.ComponentModel;

namespace tomate_en_italien
{
    /// <summary>
    /// Logique d'interaction pour TimerName.xaml
    /// </summary>
    public partial class TimerName : Window
    {
        public String name = "";

        public TimerName()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            // On load les éléments de la liste des noms
            /////////////////////////////////////////////////////
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // On récupère le nom
            if (txtBox.Text == "")
            {
                name = cbBox.Text;
            }
            else 
            {
                name = txtBox.Text;
            }
            // On change la variable name de la dont on hérite
            MainWindow mainWindow = Owner as MainWindow;
            mainWindow.monTimerName = name;
            // On sauvegarde le nom en bdd
            /////////////////////////////////////////////////////
            this.Close();
        }
    }
}
