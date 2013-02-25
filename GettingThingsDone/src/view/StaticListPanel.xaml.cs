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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class StaticListPanel : UserControl
    {
        public StaticListPanel()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TaskCreationWindow editWin = new TaskCreationWindow();
                ISingleTask task = (sender as StackPanel).DataContext as ISingleTask;
                editWin.CreationPanel.LoadFromTask(task);
                if (editWin.ShowDialog().Value)
                {
                    editWin.CreationPanel.WriteToTask(task);
                    this.List.Items.Refresh();
                }
            }
        }

        private void DelButton_Click_1(object sender, RoutedEventArgs e)
        {
            ISingleTask task = (sender as Button).DataContext as ISingleTask;
            StaticList list = DataContext as StaticList;
            list.removeTask(task);
            task.Delete();
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            Task t = e.Data.GetData(e.Data.GetFormats().First(), true) as Task;
            TaskList l = DataContext as TaskList;

            l.AddTask(t);
        }

        private void StackPanel_Drag(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Task t = (sender as StackPanel).DataContext as Task;
                DataObject data = new DataObject(t);
                TaskList l = DataContext as TaskList;
                l.removeTask(t);
                DragDrop.DoDragDrop(sender as StackPanel, data, DragDropEffects.Copy);
            }
        }
    }
}
