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
    /// Logique d'interaction pour TaskCreationPanel.xaml
    /// </summary>
    public partial class TaskCreationPanel : UserControl
    {
        public TaskCreationPanel()
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

        public void LoadFromTask(ISingleTask task)
        {
            TitleText.Text = task.Title;
            DescText.Text = task.Description;
            DueDateBox.IsChecked = task.DueDate.HasValue;
            if (task.DueDate.HasValue)
             DueDatePicker.SelectedDate = new DateTime?(task.DueDate.Value.DateTime);
        }

        public void WriteToTask(ISingleTask task)
        {
            task.Title = TitleText.Text;
            task.Description = DescText.Text;
            if (DueDateBox.IsChecked.Value && DueDatePicker.SelectedDate.HasValue)
                task.DueDate = new DateTimeOffset(DueDatePicker.SelectedDate.Value);
            else
                task.DueDate = new DateTimeOffset?();
        }

        public Task CreateTask()
        {
            IGTDFactory factory = (App.Current as App).Factory;
            IUser user = (App.Current as App).GTD.Owner;
            return factory.makeTask(TitleText.Text, DescText.Text, DueDatePicker.SelectedDate, user);
        }

        private void PanelLoaded(object sender, RoutedEventArgs e)
        {
            TitleText.Focus();
        }
    }
}
