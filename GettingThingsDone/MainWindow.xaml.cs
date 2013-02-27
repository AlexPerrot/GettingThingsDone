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
        }

        private void CreateTask(object sender, RoutedEventArgs e)
        {
            Task t = TaskCreationWindow.GetNewTask();
            if (t!=null)
                ((App)App.Current).GTD.Inbox.AddItem(t);
        }

        private void ReviewButtonClick(object sender, MouseButtonEventArgs e)
        {
            this.ReviewLink.Visibility = Visibility.Collapsed;
            this.ScheduleLink.Visibility = Visibility.Collapsed;
            this.ReviewPanel.Visibility = Visibility.Visible;
        }

        private void ReviewLinkEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            this.ReviewLink.Foreground = new SolidColorBrush(Colors.Aqua);
        }

        private void ReviewLinkLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.ReviewLink.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
        }

        private void ScheduleButtonClick(object sender, MouseButtonEventArgs e)
        {
            this.ReviewLink.Visibility = Visibility.Collapsed;
            this.ScheduleLink.Visibility = Visibility.Collapsed;
            this.ReviewPanel.Visibility = Visibility.Visible;
        }

        private void ScheduleLinkEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            this.ScheduleLink.Foreground = new SolidColorBrush(Colors.Aqua);
        }

        private void ScheduleLinkLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.ScheduleLink.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
        }

    }
}
