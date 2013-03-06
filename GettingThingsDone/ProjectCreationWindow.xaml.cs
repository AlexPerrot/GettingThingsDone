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
using System.Windows.Shapes;

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour ProjectCreationWindow.xaml
    /// </summary>
    public partial class ProjectCreationWindow : Window
    {
        public ProjectCreationWindow()
        {
            InitializeComponent();
        }

        public static Task GetNewProject()
        {
            ProjectCreationWindow win = new ProjectCreationWindow();
            if (win.ShowDialog().Value)
                return win.CreationPanel.CreateProject();
            else
                return null;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
