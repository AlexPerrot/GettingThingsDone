using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace GTDLibrary
{
    /// <summary>
    /// Logique d'interaction pour ArianneThread.xaml
    /// </summary>
    public partial class AriadneThread : UserControl
    {
        /// <summary>
        /// Statically constant defined minimum size the labels can have in the StackPanel.
        /// </summary>
        private const double MIN_LABEL_SIZE = 30;

        /// <summary>
        /// Delegate of the callback that has to be called when the home button is clicked.
        /// </summary>
        public delegate void AriadneCallback();

        /// <summary>
        /// Maximum amount of elements allowed to be displayed at once.
        /// </summary>
        private long maximumElement = 0;
        /// <summary>
        /// Array containing every steps made so far.
        /// </summary>
        private ArrayList elements = null;
        /// <summary>
        /// Callback to be executed when the home button is pressed.
        /// </summary>
        private AriadneCallback homeCall = null;
        /// <summary>
        /// Whether to execute all callbacks when going back in the Ariadne thread more than once (LinkLabel click).
        /// </summary>
        private bool executeAllCallbacks = false;
        /// <summary>
        /// Minimum size a Label is allowed to have.
        /// </summary>
        private double minLabelSize = 0;

        #region "Accessors"
        // -------------------------------------------------------
        //                       Accessors
        // -------------------------------------------------------

        /// <summary>
        /// Maximum amount of elements.
        /// </summary>
        /// <returns> The maximum amount of elements that the StackPanel is allowed to display. </returns>
        public long GetMaximumElement()
        {
            return this.maximumElement;
        }

        /// <summary>
        /// Maximum amount of elements.
        /// </summary>
        /// <param name="max"> The maximum amount of elements that the StackPanel is allowed to display. </param>
        /// <remarks> Changing this property does not alter the currently displayed elements. </remarks>
        public void SetMaximumElement(long max)
        {
            this.maximumElement = max;
        }

        /// <summary>
        /// The text of the current label leaf.
        /// </summary>
        /// <returns> The text contained in the current label leaf. </returns>
        public String GetCurrent()
        {
            if (this.elements.Count > 0)
                return (this.elements[this.elements.Count - 1] as Label).Content.ToString().Substring(2);
            return null;
        }

        /// <summary>
        /// Callback to execute when the home button is clicked.
        /// </summary>
        /// <param name="call"> Callback that should be executed when the home button is clicked. </param>
        public void SetHomeCallback(AriadneCallback call)
        {
            this.homeCall = call;
        }

        /// <summary>
        /// Boolean to execute all the callbacks when the Ariadne thread goes back more than once (LinkLabel click) and not only the last one.
        /// </summary>
        /// <param name="b"> True if all the callbacks should be executed, false otherwise. </param>
        public void SetExecuteAllCallbacks(bool b)
        {
            this.executeAllCallbacks = b;
        }

        /// <summary>
        /// Whether all the callbacks should be executed when the Ariadne thread goes back more than once (LinkLabel click) and not only the last one.
        /// </summary>
        /// <returns> True if all the callbacks should be executed, false otherwise. </returns>
        public bool GetExecuteAllCallbacks()
        {
            return this.executeAllCallbacks;
        }

        /// <summary>
        /// The minimum size a Label is allowed to have.
        /// </summary>
        /// <param name="min"> Minimum size a Label is allowed to have. </param>
        public void SetMinLabelSize(double min)
        {
            this.minLabelSize = min;
        }

        /// <summary>
        /// Double representing the minimum size a Label is allowed to have.
        /// </summary>
        /// <returns> The minimum size a Label is allowed to have. </returns>
        public double GetMinLabelSize()
        {
            return this.minLabelSize;
        }

        #endregion

        #region "Constructors"
        // -------------------------------------------------------
        //                       Constructors
        // -------------------------------------------------------

        public AriadneThread()
        {
            InitializeComponent();

            this.maximumElement = 1;
            this.elements = new ArrayList();
            this.homeCall = null;
            this.minLabelSize = MIN_LABEL_SIZE;
        }

        /// <summary>
        /// AriadneThread constructor.
        /// </summary>
        /// <param name="call"> Callback to be executed when the home button is clicked. </param>
        /// <param name="maximumElement"> Maximum elements allowed to be display at once. </param>
        public AriadneThread(AriadneCallback call, long maximumElement)
            : this()
        {
            this.maximumElement = maximumElement;
            this.homeCall = call;
        }

        #endregion

        #region "Methods"
        // -------------------------------------------------------
        //                        Methods
        // -------------------------------------------------------

        /// <summary>
        /// Sets the given control to the best size vis-a-vis of the StackPanel.
        /// </summary>
        /// <param name="cont"> Control to adjust. </param>
        private void setToBestWidth(Control cont)
        {
            double computedWidth = 0;
            computedWidth = Math.Max(this.minLabelSize, this.stackPanelPath.Width / this.maximumElement);
            cont.Width = computedWidth;
        }

        /// <summary>
        /// Goes next in the Ariadne thread.
        /// </summary>
        /// <param name="call"> Callback to be executed when going back. </param>
        /// <param name="name"> Text that should be displayed in the Label to symbolize the step. </param>
        public void GoNext(LinkLabel.LinkCallback call, String name)
        {
            if (this.elements.Count > 0)
            {
                Label l = (this.elements[this.elements.Count - 1] as Label);
                this.elements.Remove(l);
                this.stackPanelPath.Children.Remove(l);

                LinkLabel ll = new LinkLabel(call, l.Content.ToString());
                this.setToBestWidth(ll);
                this.elements.Add(ll);
                this.stackPanelPath.Children.Add(ll);
                ll.LinkClickedEvent += new EventHandler(LinkLabel_LinkClickedEvent);
            }

            Label lab = new Label();
            lab.Content = "> " + name;
            this.setToBestWidth(lab);
            this.elements.Add(lab);
            this.stackPanelPath.Children.Add(lab);

            if (this.stackPanelPath.Children.Count > this.maximumElement)
                this.stackPanelPath.Children.RemoveAt(0);
        }

        /// <summary>
        /// Goes back once in the Ariadne thread.
        /// </summary>
        /// <param name="executeCallback"> Whether all the callbacks should be executed when the Ariadne thread goes back more than once (LinkLabel click) and not only the last one. </param>
        private void GoBack(bool executeCallback)
        {
            if (this.elements.Count == 1)
            {
                Label old = (this.elements[this.elements.Count - 1] as Label);
                this.elements.Remove(old);
                this.stackPanelPath.Children.Remove(old);
            }

            if (this.elements.Count > 1)
            {
                Label old = (this.elements[this.elements.Count - 1] as Label);
                this.elements.Remove(old);
                this.stackPanelPath.Children.Remove(old);

                LinkLabel ll = (this.elements[this.elements.Count - 1] as LinkLabel);
                this.elements.Remove(ll);
                if (this.maximumElement > 1)
                    this.stackPanelPath.Children.Remove(ll);

                Label l = new Label();
                l.Content = ll.text.Text;
                this.setToBestWidth(l);
                this.elements.Add(l);
                this.stackPanelPath.Children.Add(l);

                long pos = this.elements.Count - this.maximumElement;
                if (pos >= 0)
                {
                    LinkLabel llOld = this.elements[(int)(pos)] as LinkLabel;
                    this.stackPanelPath.Children.Insert(0, llOld);
                }

                if (executeCallback)
                    ll.executeCallback();
            }
        }

        /// <summary>
        /// Goes back once in the Ariadne thread.
        /// </summary>
        public void GoBack()
        {
            this.GoBack(true);
        }

        /// <summary>
        /// Handles when the home button is clicked.
        /// </summary>
        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            if (this.homeCall != null)
                this.homeCall();
        }

        /// <summary>
        /// Handles when a LinkLabel is clicked.
        /// </summary>
        private void LinkLabel_LinkClickedEvent(Object sender, EventArgs e)
        {
            while (sender != this.elements[this.elements.Count - 2])
                this.GoBack(this.executeAllCallbacks);
            this.GoBack();
        }

        #endregion

    }

}
