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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class StaticListPanel : UserControl
    {
        public StaticListPanel()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseMove_1(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("Mouse on : " + DataContext);
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TaskCreationWindow editWin = new TaskCreationWindow();
                SingleTask task = (sender as StackPanel).DataContext as SingleTask;
                editWin.CreationPanel.LoadFromTask(task);
                if (editWin.ShowDialog().Value)
                    editWin.CreationPanel.WriteToTask(task);
            }
        }
    }
}
