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
        public Brush LabelBackground {
            get { return GetValue(ProxyProp) as Brush;
            }
            set { 
            SetValue(ProxyProp, value);
            }
        }

        public bool AllowListDrop
        {
            get { return (bool)GetValue(AllowListDropProperty); }
            set { SetValue(AllowListDropProperty, value); }
        }

        public bool AllowDelete
        {
            get { return (bool)GetValue(AllowDeleteProperty); }
            set { SetValue(AllowDeleteProperty, value); }
        }

        public static readonly DependencyProperty AllowDeleteProperty =
            DependencyProperty.Register("AllowDelete", typeof (bool), typeof (StaticListPanel), new PropertyMetadata(true));

        public static readonly DependencyProperty AllowListDropProperty =
            DependencyProperty.Register("AllowListDrop", typeof(bool), typeof(StaticListPanel), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty LabelBackgroundProperty =
            DependencyProperty.Register("LabelBackground", typeof(Brush), typeof(StaticListPanel), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), FrameworkPropertyMetadataOptions.AffectsRender));
        private static readonly DependencyProperty ProxyProp = Label.BackgroundProperty.AddOwner(typeof(StaticListPanel));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Name == LabelBackgroundProperty.Name)
                this.NameLabel.Background = e.NewValue as Brush;
        }

        public StaticListPanel()
        {
            InitializeComponent();
            this.List.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
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

            if (e.Data.GetFormats().Contains("GettingThingsDone.src.view.StaticListPanel") && this.AllowListDrop)
            {
                StaticListPanel source = e.Data.GetData(e.Data.GetFormats().First()) as StaticListPanel;
                StaticListPanel target = e.Source as StaticListPanel;

                var tmp = source.DataContext;
                source.DataContext = target.DataContext;
                target.DataContext = tmp;

                return;
            }

            else if (e.Data.GetFormats().Contains(typeof(TaskMoveData).ToString()))
            {
                TaskMoveData data = e.Data.GetData(e.Data.GetFormats().First(), true) as TaskMoveData;

                data.OrigList.removeTask(data.Task);

                TaskList l = DataContext as TaskList;

                l.AddTask(data.Task);

                this.List.BorderThickness = new Thickness(0);
            }
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

        private void UserControl_DragEnter_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetFormats().Contains(typeof(TaskMoveData).ToString()))
                this.List.BorderThickness = new Thickness(2);
            else if (!AllowListDrop)
                e.Effects = DragDropEffects.None;
        }

        private void UserControl_DragLeave_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetFormats().Contains(typeof(TaskMoveData).ToString()))
                this.List.BorderThickness = new Thickness(0);
        }

        private void TrashcanButton_Click(object sender, RoutedEventArgs e)
        {
            StaticList list = DataContext as StaticList;
            list.Delete();
        }

        private void UserControl_MouseEnter_1(object sender, MouseEventArgs e)
        {
            if(this.AllowDelete)
            {
                TrashcanButton.Visibility = Visibility.Visible;
            }
        }

        private void UserControl_MouseLeave_1(object sender, MouseEventArgs e)
        {
            TrashcanButton.Visibility = Visibility.Hidden;
        }
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
