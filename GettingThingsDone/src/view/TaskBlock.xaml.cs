using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GettingThingsDone.src.model;

namespace GettingThingsDone.src.view
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class TaskBlock : UserControl
    {
        public TaskBlock()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, RoutedEventArgs routedEventArgs)
        {
            TaskCreationWindow editWin = new TaskCreationWindow();
            ISingleTask task = (sender as Button).DataContext as ISingleTask;
            editWin.CreationPanel.LoadFromTask(task);
            if (editWin.ShowDialog().Value)
            {
                editWin.CreationPanel.WriteToTask(task);
            }
        }

        private void DelButton_Click_1(object sender, RoutedEventArgs e)
        {
            ISingleTask task = (sender as Button).DataContext as ISingleTask;
            IGTDSystem sys = (App.Current as App).GTD;
            sys.removeTask(task);
        }

        private void StackPanel_Drag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Task t = (sender as StackPanel).DataContext as Task;
                TaskList l = DataContext as TaskList;
                TaskMoveData tmd = new TaskMoveData(t, l);
                DataObject data = new DataObject(tmd);
                DragDrop.DoDragDrop(sender as StackPanel, data, DragDropEffects.Move);
            }
        }

        private void Task_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var s = sender as Panel;
            var childPanel = s.FindName("listStackPanel") as Panel;

            var element = childPanel.FindName("DelButton") as UIElement;
            element.Visibility = Visibility.Visible;
            element = childPanel.FindName("EditButton") as UIElement;
            element.Visibility = Visibility.Visible;
        }

        private void Task_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var s = sender as Panel;
            var childPanel = s.FindName("listStackPanel") as Panel;

            var element = childPanel.FindName("DelButton") as UIElement;
            element.Visibility = Visibility.Hidden;
            element = childPanel.FindName("EditButton") as UIElement;
            element.Visibility = Visibility.Hidden;

        }

        class TaskMoveData
        {
            private Task task;
            private TaskList origin;
            public Task Task { get { return this.task; } }
            public TaskList OrigList { get { return this.origin; } }

            public TaskMoveData(Task t, TaskList l)
            {
                this.task = t;
                this.origin = l;
            }
        }
    }
}
