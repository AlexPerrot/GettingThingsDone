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
    public partial class ContextCreationPanel : UserControl
    {
        public ContextCreationPanel()
        {
            InitializeComponent();
        }

        /*public void WriteToTask(ISingleTask task)
        {
            task.Title = TitleText.Text;
            task.Description = DescText.Text;
            if (DueDateBox.IsChecked.Value && DueDatePicker.SelectedDate.HasValue)
                task.DueDate = new DateTimeOffset(DueDatePicker.SelectedDate.Value);
            else
                task.DueDate = new DateTimeOffset?();
        }*/

        public IStaticList CreateContext()
        {
            IGTDFactory factory = (App.Current as App).Factory;
            return factory.makeContext(TitleText.Text, DescText.Text);
        }

        private void PanelLoaded(object sender, RoutedEventArgs e)
        {
            TitleText.Focus();
        }
    }
}