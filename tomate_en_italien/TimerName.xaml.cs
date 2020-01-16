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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            // Récupération des tasks en BDD
            comboTask.Items.Clear();
            foreach(Task task in SqliteDbAccess.LoadTask())
            {
                comboTask.Items.Add(task);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // On récupère le nom
            if (txtBox.Text.Trim() == "")
            {
                name = comboTask.Text;
            }
            else 
            {
                name = txtBox.Text;

                // Insertion de la task en BDD
                Task newTask = new Task();
                newTask.Libelle = name;
                SqliteDbAccess.SaveTasks(newTask);
            }
            // On change la variable name de la dont on hérite
            MainWindow mainWindow = Owner as MainWindow;
            mainWindow.monTimerName = name;

            this.Close();
        }
    }
}
