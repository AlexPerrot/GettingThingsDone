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
    /// Logique d'interaction pour ProjectCreationPanel.xaml
    /// </summary>
    public partial class ProjectCreationPanel : UserControl
    {
        public ProjectCreationPanel()
        {
            InitializeComponent();
        }

        private void DueDateChecked(object sender, RoutedEventArgs e)
        {
            DueDatePicker.IsEnabled = true;
        }

        private void DueDateUnchecked(object sender, RoutedEventArgs e)
        {
            DueDatePicker.IsEnabled = false;
        }

        public void LoadFromProject(IProject project)
        {
            TitleText.Text = project.Title;
            DescText.Text = project.Description;
            DueDateBox.IsChecked = project.DueDate.HasValue;
            if (project.DueDate.HasValue)
                DueDatePicker.SelectedDate = new DateTime?(project.DueDate.Value.DateTime);
        }

        public void WriteToProject(IProject project)
        {
            project.Title = TitleText.Text;
            project.Description = DescText.Text;
            if (DueDateBox.IsChecked.Value && DueDatePicker.SelectedDate.HasValue)
                project.DueDate = new DateTimeOffset(DueDatePicker.SelectedDate.Value);
            else
                project.DueDate = new DateTimeOffset?();
        }

        public Task CreateProject()
        {
            IGTDFactory factory = (App.Current as App).Factory;
            return factory.makeProject(TitleText.Text, DescText.Text, DueDatePicker.SelectedDate);
        }

        private void PanelLoaded(object sender, RoutedEventArgs e)
        {
            TitleText.Focus();
        }
    }
}
