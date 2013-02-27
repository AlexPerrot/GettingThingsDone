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
            DataContext = (App.Current as App).GTD;
        }

  

        private void StaticListPanel_Drop_1(object sender, DragEventArgs e)
        {
            StaticListPanel source = e.Source as StaticListPanel;
            StaticListPanel target = sender as StaticListPanel;

            var tmp = source.DataContext;
            source.DataContext = target.DataContext;
            target.DataContext = tmp;
        }

        private void StaticListPanel_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject(sender);
                DragDrop.DoDragDrop(sender as StaticListPanel, data, DragDropEffects.Move);
            }
        }
    }
}
