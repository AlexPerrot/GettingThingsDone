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

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.ReviewLink.Visibility = Visibility.Collapsed;
            this.ReviewPanel.Visibility = Visibility.Visible;
        }

        private void ReviewLink_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            this.ReviewLink.Foreground = new SolidColorBrush(Colors.Aqua);
        }

        private void ReviewLink_MouseLeave_1(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.ReviewLink.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
        }

    }
}
