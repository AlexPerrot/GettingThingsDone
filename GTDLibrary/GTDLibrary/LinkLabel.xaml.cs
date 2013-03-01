using System;
using System.Windows;
using System.Windows.Controls;

namespace GTDLibrary
{
    /// <summary>
    /// Logique d'interaction pour LinkLabel.xaml
    /// </summary>
    public partial class LinkLabel : UserControl
    {
        /// <summary>
        /// Delegate of the callback that has to be called when the hyperlink is clicked.
        /// </summary>
        public delegate void LinkCallback();
        /// <summary>
        /// Event raised when the hyperlink is clicked.
        /// </summary>
        public event EventHandler LinkClickedEvent;

        /// <summary>
        /// Callback that has to be called when the hyperlink is clicked.
        /// </summary>
        private LinkCallback call = null;

        #region "Constructors"
        // -------------------------------------------------------
        //                       Constructors
        // -------------------------------------------------------

        public LinkLabel()
        {
            InitializeComponent();
            this.call = null;
        }

        /// <summary>
        /// LinkLabel constructor.
        /// </summary>
        /// <param name="call"> Callback that has to be called when the hyperlink is clicked. </param>
        public LinkLabel(LinkCallback call)
            : this()
        {
            this.call = call;
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

        /// <summary>
        /// LinkLabel constructor.
        /// </summary>
        /// <param name="call"> Callback that has to be called when the hyperlink is clicked. </param>
        /// <param name="content"> String to be shown in the hyperlink. </param>
        public LinkLabel(LinkCallback call, String content)
            : this(content)
        {
            this.call = call;
        }

        #endregion

        #region "Methods"
        // -------------------------------------------------------
        //                        Methods
        // -------------------------------------------------------

        /// <summary>
        /// Execute the callback of this LinkLabel.
        /// </summary>
        public void executeCallback()
        {
            this.call();
        }

        /// <summary>
        /// When the hyperlink is clicked, raise an event to prevent the parent, so that the callback can be executed.
        /// </summary>
        private void hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (this.LinkClickedEvent != null)
                this.LinkClickedEvent(this, new EventArgs());
        }

        #endregion
    }
}
