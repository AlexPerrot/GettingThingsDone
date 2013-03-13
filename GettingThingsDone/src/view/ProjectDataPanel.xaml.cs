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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GettingThingsDone.src.view
{
    /// <summary>
    /// Logique d'interaction pour ProjectDataPanel.xaml
    /// </summary>
    public partial class ProjectDataPanel : UserControl
    {
        public ProjectDataPanel()
        {
            InitializeComponent();
        }

        private void CreateTaskClick(object sender, MouseButtonEventArgs e)
        {
            Task t = TaskCreationWindowWithContext.NewTaskDialog();
            if (t != null)
            {
                (this.DataContext as IProject).AddTask(t);
                this.TaskList.Items.Refresh();
            }
        }

        private void CreateTaskLinkEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            this.CreateTaskLink.Foreground = new SolidColorBrush(Colors.Aqua);
        }

        private void CreateTaskLinkLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.CreateTaskLink.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
        }

        private void MoveItemUpClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = this.TaskList.SelectedIndex;
            if (selectedIndex > 0)
            {
                (this.DataContext as IProject).moveTaskTo(selectedIndex, selectedIndex - 1);
                this.TaskList.Items.Refresh();
            }
        }

        private void MoveItemUpLinkEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            this.MoveItemUpLink.Foreground = new SolidColorBrush(Colors.Aqua);
        }

        private void MoveItemUpLinkLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.MoveItemUpLink.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
        }

        private void MoveItemDownClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = this.TaskList.SelectedIndex;
            if (selectedIndex < (this.DataContext as IProject).Tasks.Count() - 1)
            {
                (this.DataContext as IProject).moveTaskTo(selectedIndex, selectedIndex + 1);
                this.TaskList.Items.Refresh();
            }
        }

        private void MoveItemDownLinkEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            this.MoveItemDownLink.Foreground = new SolidColorBrush(Colors.Aqua);
        }

        private void MoveItemDownLinkLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.MoveItemDownLink.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
        }

        private void DeleteTaskButton(object sender, RoutedEventArgs e)
        {
            ISingleTask task = (sender as Button).DataContext as ISingleTask;
            IGTDSystem sys = (App.Current as App).GTD;
            sys.removeTask(task);
            this.TaskList.Items.Refresh();
        }
    }
}
