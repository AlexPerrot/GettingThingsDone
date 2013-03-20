using GettingThingsDone.src.model;
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

namespace GettingThingsDone.src.view
{
    /// <summary>
    /// Logique d'interaction pour TaskCreationWindow.xaml
    /// </summary>
    public partial class TaskCreationWindowWithContext : Window
    {
        public IEnumerable<GTDItem> Contexts { get { return App.Current.Properties["GTD"] as IGTDSystem; } }

        public TaskCreationWindowWithContext()
        {
            InitializeComponent();
        }

        public static Task NewTaskDialog()
        {
            TaskCreationWindowWithContext win = new TaskCreationWindowWithContext();
            if (win.ShowDialog().Value)
            {
                Task t = win.CreationPanel.CreateTask();
                TaskList l = win.ContextSelect.SelectedItem as TaskList;
                if (l != null)
                {
                    l.AddTask(t);
                }

                return t;
            }
            return null;
        }
        
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
