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
    /// Logique d'interaction pour ArrowButtonBody.xaml
    /// </summary>
    public partial class ArrowButtonBody : UserControl
    {
        [Category("Events")]
        [DisplayName("Click")]
        public event EventHandler Click;

        [Category("GTD")]
        [DisplayName("Text")]
        public String Text
        {
            get
            {
                return this.MainButtonUI.Content as String;
            }
            set
            {
                this.MainButtonUI.Content = value;
            }
        }

        public AriadneThread.AriadneThreadCallback Callback { get; set; }

        public ArrowButtonBody()
        {
            InitializeComponent();
        }

        private void MainButtonUI_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
                this.Click(this, e);
        }

    }
}
