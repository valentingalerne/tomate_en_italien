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
using tomate_en_italien.util;

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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Récupération des tasks en BDD
            comboTask.Items.Clear();
            foreach(Task task in SqliteDbAccess.LoadTask())
            {
                comboTask.Items.Add(task);
            }
        }

        /// <summary>
        /// Save la nouvelle task en bdd si besoin
        /// Incrémente le nombre d'exécution de cette task et ferme cette fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTask(object sender, RoutedEventArgs e)
        {
            Task newTask = new Task();
            var name = "";
            // On récupère le nom
            if (txtBox.Text.Trim() == "")
            {
                name = comboTask.Text;
                newTask.Libelle = name;
            }
            else 
            {
                name = txtBox.Text;

                // Insertion de la task en BDD si elle n'existe pas
                newTask.Libelle = name;
                try
                {
                    SqliteDbAccess.SaveTasks(newTask);
                } catch(Exception) { }
            }
            SqliteDbAccess.UpdateCountTask(newTask);
            // On change la variable name de la dont on hérite
            MainWindow mainWindow = Owner as MainWindow;
            mainWindow.monTimerName = name;

            this.Close();
        }
    }
}
