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
using System.ComponentModel;

namespace GTDLibrary
{
    /// <summary>
    /// Logique d'interaction pour ArrowButtonMiddle.xaml
    /// </summary>
    public partial class ArrowButtonMiddle : UserControl
    {
        [Category("Events")]
        [DisplayName("Click")]
        public event EventHandler Click;

        public ArrowButtonMiddle()
        {
            InitializeComponent();
        }

        private void MainButtonUI_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
                this.Click(sender, e);
        }

    }
}
