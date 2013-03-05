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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GettingThingsDone.src.view;
using GettingThingsDone.src.model;

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ISchedule Schedule { get { return (App.Current as App).Factory.makeSchedule(DataContext as IGTDSystem); } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ((App)App.Current).GTD;
        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            Task t = TaskCreationWindow.GetNewTask();
            if (t != null)
            {
                ((App)App.Current).GTD.Inbox.AddTask(t);
            }
        }

    }
}
