using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Collections;

namespace GTDLibrary.utils
{
    internal abstract class AriadneThreadElementBuilderAbstract : IAriadneThreadElementBuilder
    {
        /// <summary>
        /// Array containing every steps made so far.
        /// </summary>
        protected ArrayList elements = null;
        /// <summary>
        /// Panel on which to operate.
        /// </summary>
        protected Panel pan = null;
        /// <summary>
        /// Maximum amount of elements allowed to be displayed at once.
        /// </summary>
        public long MaximumElement { get; set; }
        /// <summary>
        /// Whether to execute all callbacks when going back in the Ariadne thread more than once.
        /// </summary>
        public bool ExecuteAllCallbacks { get; set; }
        /// <summary>
        /// Callback to execute when the home button is clicked.
        /// </summary>
        public abstract AriadneThread.AriadneThreadCallback HomeCallback { get; set; }

        public AriadneThreadElementBuilderAbstract(Panel pan, long maximumElement, bool executeAllCallback)
        {
            this.elements = new ArrayList();
            this.pan = pan;
            this.MaximumElement = maximumElement;
            this.ExecuteAllCallbacks = executeAllCallback;
        }

        public abstract void MoveNext(AriadneThread.AriadneThreadCallback call, String name);

        public abstract void MoveBack();

        protected abstract void MoveBack(bool executeCallback);

        protected abstract void Clicked(Object sender, EventArgs e);

    }
}
