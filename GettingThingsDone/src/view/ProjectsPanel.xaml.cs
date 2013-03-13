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
using GettingThingsDone.src.model;

namespace GettingThingsDone.src.view
{
    /// <summary>
    /// Logique d'interaction pour ProjectPanel.xaml
    /// </summary>
    public partial class ProjectsPanel : UserControl
    {

        //MainWindow main = App.Current.MainWindow as MainWindow;

        public ProjectsPanel()
        {
            InitializeComponent();
            this.DataContext = ((App)App.Current).GTD;
        }

        private void CreateProjectLinkEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            this.CreateProjectLink.Foreground = new SolidColorBrush(Colors.Aqua);
        }

        private void CreateProjectLinkLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            this.CreateProjectLink.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
        }

        private void CreateProjectClick(object sender, MouseButtonEventArgs e)
        {
            Task newProject = ProjectCreationWindow.GetNewProject();

            if (newProject != null)
            {
                ((App)App.Current).GTD.Projects.Add(newProject);
                this.FileList.Items.Refresh();
            }
        }

        private void UpdateProject(object sender, MouseButtonEventArgs e)
        {
            ProjectCreationWindow editWindow = new ProjectCreationWindow();
            IProject project = (sender as Label).DataContext as IProject;
            editWindow.CreationPanel.LoadFromProject(project);
            if (editWindow.ShowDialog().Value)
            {
                editWindow.CreationPanel.WriteToProject(project);
                this.FileList.Items.Refresh();
            }
        }

        private void CreateTaskClick(object sender, MouseButtonEventArgs e)
        {
            Task t = TaskCreationWindowWithContext.NewTaskDialog();
            if (t != null)
            {
                (FileList.SelectedItem as IProject).AddTask(t);
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
                (FileList.SelectedItem as IProject).moveTaskTo(selectedIndex, selectedIndex - 1);
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
            if (selectedIndex < (FileList.SelectedItem as IProject).Tasks.Count())
            {
                (FileList.SelectedItem as IProject).moveTaskTo(selectedIndex, selectedIndex + 1);
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

        private void DeleteProjectButton(object sender, RoutedEventArgs e)
        {
            IProject project = (sender as Button).DataContext as IProject;
            IGTDSystem sys = (App.Current as App).GTD;
            sys.removeProject(project);
            this.FileList.Items.Refresh();
        }
    }
}
