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
using System.Windows.Shapes;

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour TaskCreationWindow.xaml
    /// </summary>
    public partial class TaskCreationWindow : Window
    {
        public TaskCreationWindow()
        {
            InitializeComponent();
        }

        public static Task GetNewTask()
        {
            TaskCreationWindow win = new TaskCreationWindow();
            if (win.ShowDialog().Value)
                return win.CreationPanel.CreateTask();
            else
                return null;
        }
        
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
