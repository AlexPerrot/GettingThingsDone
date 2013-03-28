using System;
using System.Collections;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Media;
using GTDLibrary.utils;

namespace GTDLibrary
{
    /// <summary>
    /// Logique d'interaction pour ArianneThread.xaml
    /// </summary>
    public partial class AriadneThread : UserControl
    {
        /// <summary>
        /// Delegate to be called when a button is clicked.
        /// </summary>
        public delegate void AriadneThreadCallback();

        /// <summary>
        /// Style builder.
        /// </summary>
        private IAriadneThreadElementBuilder builder = null;
       
        /// <summary>
        /// Maximum amount of elements allowed to be displayed at once.
        /// </summary>
        [Category("GTD")]
        [DisplayName("MaximumElement")]
        public long MaximumElement { get { return this.builder.MaximumElement; } set { this.builder.MaximumElement = Math.Max(value, 1); } }
        /// <summary>
        /// Whether to execute all callbacks when going back in the Ariadne thread more than once.
        /// </summary>
        [Category("GTD")]
        [DisplayName("ExecuteAllCallbacks")]
        public bool ExecuteAllCallbacks { get { return this.builder.ExecuteAllCallbacks; } set { this.builder.ExecuteAllCallbacks = value; } }
        /// <summary>
        /// Defines the style of the AriadneThread.
        /// </summary>
        [Category("GTD")]
        [DisplayName("ThreadStyle")]
        public AriadneThreadStyle ThreadStyle
        {
            get
            {
                return this.threadStyle;
            }
            set
            {
                if (value != this.threadStyle)
                {
                    this.stackPanelPath.Children.Clear();
                    this.builder = AriadneThreadElementBuilderFactory.Create(value, this.stackPanelPath, this.MaximumElement, this.ExecuteAllCallbacks);
                    this.threadStyle = value;
                }
            }
        }
        private AriadneThreadStyle threadStyle = AriadneThreadStyle.Link;
        /// <summary>
        /// Callback to be executed when the home button is clicked.
        /// </summary>
        public AriadneThreadCallback HomeCallback
        {
            get
            {
                return this.builder.HomeCallback;
            }
            set
            {
                this.builder.HomeCallback = value;
            }
        }

        #region "Constructors"

        public AriadneThread()
        {
            InitializeComponent();
            this.builder = new AriadneThreadElementBuilderLinkLabel(this.stackPanelPath, 1, false);
        }

        /// <summary>
        /// AriadneThread constructor.
        /// </summary>
        /// <param name="maximumElement"> Maximum elements allowed to be display at once. </param>
        public AriadneThread(long maximumElement)
            : this()
        {
            this.MaximumElement = maximumElement;
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Goes next in the Ariadne thread.
        /// </summary>
        /// <param name="call"> Callback to be executed when going back. </param>
        /// <param name="name"> Text that should be displayed in the Label to symbolize the step. </param>
        public void GoNext(AriadneThread.AriadneThreadCallback call, String name)
        {
            this.builder.MoveNext(call, name);
        }

        /// <summary>
        /// Goes back once in the Ariadne thread.
        /// </summary>
        public void GoBack()
        {
            this.builder.MoveBack();
        }

        #endregion
    }

}
