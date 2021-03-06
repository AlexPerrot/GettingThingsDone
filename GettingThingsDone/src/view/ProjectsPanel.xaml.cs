﻿using System;
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
        public ProjectsPanel()
        {
            InitializeComponent();
            this.DataContext = App.Current.Properties["GTD"];
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
                (this.DataContext as IGTDSystem).Projects.Add(newProject);
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

        private void DeleteProjectButton(object sender, RoutedEventArgs e)
        {
            IProject project = (sender as Button).DataContext as IProject;
            IGTDSystem sys = App.Current.Properties["GTD"] as IGTDSystem;
            sys.removeProject(project);
            this.FileList.Items.Refresh();
        }
    }
}
