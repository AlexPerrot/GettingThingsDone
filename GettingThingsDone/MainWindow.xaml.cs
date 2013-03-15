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
        private ISchedule schedule;
        public ISchedule Schedule { get { return schedule; } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ((App)App.Current).GTD;
            schedule = (App.Current as App).Factory.makeSchedule(DataContext as IGTDSystem);
        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            Task t = TaskCreationWindow.GetNewTask();
            if (t != null)
            {
                ((App)App.Current).GTD.Inbox.AddTask(t);
            }
        }

        private void CreateContext(object sender, RoutedEventArgs e)
        {
            IStaticList c = ContextCreationWindow.GetNewContext();
            if (c != null)
            {
                ((App)App.Current).GTD.Contexts.Add(c);
            }
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            TabItem item = (sender as TabControl).SelectedItem as TabItem;
            if (item != null)
            {
                switch (item.Header.ToString())
                {
                    case "Schedule":
                        Dispatcher.BeginInvoke(new Action(this.SchedulePage.update),
                            System.Windows.Threading.DispatcherPriority.Background,
                            new object[0]);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SchedulePage_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.SchedulePage.DataContext = Schedule;
        }


    }
}
