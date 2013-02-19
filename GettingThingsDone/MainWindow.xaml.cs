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

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ((App)App.Current).GTD;
            System.Console.WriteLine("coucou");
            
            System.Console.WriteLine("ok");
        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            Task t = TaskCreationWindow.GetNewTask();
            if (t!=null)
                ((App)App.Current).GTD.Inbox.AddItem(t);
            System.Console.WriteLine(((App)App.Current).GTD.Inbox.List.Count(item => ((SingleTask)item).Done));
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //foreach (StaticList l in ((App)App.Current).GTD)
            //{
            //    StaticListPanel panel = new StaticListPanel();
            //    panel.DataContext = l;
            //    panel.Width = 100;
            //    ContextsPanel.Children.Add(panel);
            //}
        }
    }
}
