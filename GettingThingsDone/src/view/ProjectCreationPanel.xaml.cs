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
    }
}
