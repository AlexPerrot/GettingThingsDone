using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTDLibrary
{
    /// <summary>
    /// Logique d'interaction pour LinkLabel.xaml
    /// </summary>
    public partial class LinkLabel : UserControl
    {
        public LinkLabel()
        {
            InitializeComponent();
            this.Height = 30;
            this.Width = 60;
        }

        public LinkLabel(String content) : this()
        {
            this.text.Text = content;
        }

        private void hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
    }
}
