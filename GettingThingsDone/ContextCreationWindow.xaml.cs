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

namespace GettingThingsDone
{
    /// <summary>
    /// Logique d'interaction pour ContextCreationWindow.xaml
    /// </summary>
    public partial class ContextCreationWindow : Window
    {
        public ContextCreationWindow()
        {
            InitializeComponent();
        }

        public static IStaticList GetNewContext()
        {
            ContextCreationWindow win = new ContextCreationWindow();
            if (win.ShowDialog().Value)
                return win.CreationPanel.CreateContext();
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
