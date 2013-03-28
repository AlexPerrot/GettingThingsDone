using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace GTDLibrary
{
    /// <summary>
    /// Logique d'interaction pour LinkLabel.xaml
    /// </summary>
    public partial class LinkLabel : UserControl
    {
        /// <summary>
        /// Event raised when the hyperlink is clicked.
        /// </summary>
        [Category("Events")]
        [DisplayName("Click")]
        public event EventHandler Click;

        /// <summary>
        /// Text shown inside the LinkLabel.
        /// </summary>
        [Category("GTD")]
        [DisplayName("Text")]
        public String Text { get { return this.text.Text; } set { this.text.Text = value; } }

        public AriadneThread.AriadneThreadCallback Callback { get; set; }

        #region "Constructors"
        
        public LinkLabel()
        {
            InitializeComponent();
            this.Callback = null;
            this.text.Text = "LinkLabel";
        }

        /// <summary>
        /// LinkLabel constructor.
        /// </summary>
        /// <param name="content"> String to be shown in the hyperlink. </param>
        public LinkLabel(String content)
            : this()
        {
            this.text.Text = content;
        }

        #endregion

        #region "Methods"
       
        /// <summary>
        /// When the hyperlink is clicked, raise an event to inform the parent, so that the callback can be executed.
        /// </summary>
        private void hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
                this.Click(this, e);
        }

        #endregion
    }
}
