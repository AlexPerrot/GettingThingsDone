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
    /// Logique d'interaction pour ReviewPage.xaml
    /// </summary>
    public partial class ReviewPage : UserControl
    {
        public ReviewPage()
        {
            InitializeComponent();
        }

  

        private void StaticListPanel_Drop_1(object sender, DragEventArgs e)
        {
            StaticListPanel target = sender as StaticListPanel;
            if (e.Data.GetFormats().Contains(typeof(int).ToString()) && target.AllowListDrop)
            {
                int sourceIndex = (int)e.Data.GetData(typeof(int));
                int targetIndex = Contexts.Items.IndexOf(target.DataContext);

                IGTDSystem sys = DataContext as IGTDSystem;

                var sourceData = sys.Contexts.ElementAt(sourceIndex);
                sys.Contexts.RemoveAt(sourceIndex);
                sys.Contexts.Insert(targetIndex, sourceData);
            }
            else if (e.Data.GetFormats().Contains(typeof(TaskMoveData).ToString()))
            {
                TaskMoveData data = e.Data.GetData(e.Data.GetFormats().First(), true) as TaskMoveData;

                data.OrigList.removeTask(data.Task);

                TaskList l = target.DataContext as TaskList;

                l.AddTask(data.Task);

                target.List.BorderThickness = new Thickness(0);
            }
        }

        private void StaticListPanel_MouseMove_1(object sender, MouseEventArgs e)
        {
            StaticListPanel source = sender as StaticListPanel;

            if (e.LeftButton == MouseButtonState.Pressed && source.AllowListDrop)
            {
                int index = Contexts.Items.IndexOf(source.DataContext);
                DataObject data = new DataObject(index);
                DragDrop.DoDragDrop(sender as StaticListPanel, data, DragDropEffects.Link);
            }
        }

        private void ItemsControl_TargetUpdated_1(object sender, DataTransferEventArgs e)
        {
            Contexts.Items.Refresh();
        }
    }
}
