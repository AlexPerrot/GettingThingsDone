using System;
using System.Windows.Controls;
using GTDLibrary.utils;

namespace GTDLibrary
{
    internal class AriadneThreadElementBuilderLinkLabel : AriadneThreadElementBuilderAbstract
    {
        public override AriadneThread.AriadneThreadCallback HomeCallback
        {
            get
            {
                return (this.elements[0] as LinkLabel).Callback;
            }
            set
            {
                (this.elements[0] as LinkLabel).Callback = value;
            }
        }

        public AriadneThreadElementBuilderLinkLabel(Panel pan, long maximumElement, bool executeAllCallback)
            : base(pan, maximumElement, executeAllCallback)
        {
            this.BuildHome();
        }

        private void BuildHome()
        {
            LinkLabel lHome = new LinkLabel("HOME");
            lHome.Click += new EventHandler(Clicked);
            this.pan.Children.Add(lHome);
            this.elements.Add(lHome);
        }

        public override void MoveNext(AriadneThread.AriadneThreadCallback call, String name)
        {
            LinkLabel lab = new LinkLabel();
            lab.Text = "> " + name;
            lab.Callback = call;
            this.elements.Add(lab);
            this.pan.Children.Add(lab);
            lab.Click += new EventHandler(Clicked);

            if (this.pan.Children.Count > this.MaximumElement + 1)
                this.pan.Children.RemoveAt(1);
        }

        public override void MoveBack()
        {
            this.MoveBack(true);
        }

        protected override void MoveBack(bool executeCallback)
        {
            if (this.elements.Count > 1)
            {
                LinkLabel old = (this.elements[this.elements.Count - 1] as LinkLabel);
                this.elements.Remove(old);
                this.pan.Children.Remove(old);

                long pos = this.elements.Count - this.MaximumElement;
                if (pos >= 1)
                {
                    LinkLabel llOld = this.elements[(int)(pos)] as LinkLabel;
                    this.pan.Children.Insert(1, llOld);
                }

                LinkLabel ll = this.pan.Children[this.pan.Children.Count - 1] as LinkLabel;
                if (executeCallback && ll.Callback != null)
                    ll.Callback();
            }
        }

        protected override void Clicked(Object sender, EventArgs e)
        {
            if (sender != this.elements[this.elements.Count - 1] && this.elements.Count > 1)
            {
                while (sender != this.elements[this.elements.Count - 2])
                    this.MoveBack(this.ExecuteAllCallbacks);
                this.MoveBack();
            }
        }
    }
}
