using System;
using System.Windows.Controls;
using System.Windows;

namespace GTDLibrary.utils
{
    class AriadneThreadElementBuilderButton : AriadneThreadElementBuilderAbstract
    {
        public override AriadneThread.AriadneThreadCallback HomeCallback
        {
            get
            {
                return (this.elements[0] as ArrowButtonBody).Callback;
            }
            set
            {
                (this.elements[0] as ArrowButtonBody).Callback = value;
            }
        }

        public AriadneThreadElementBuilderButton(Panel pan, long maximumElement, bool executeAllCallback)
            : base(pan, maximumElement, executeAllCallback)
        {
            this.BuildHome();
        }

        private void BuildHome()
        {
            ArrowButtonBody bHome = new ArrowButtonBody();
            bHome.Text = "HOME";
            bHome.Click += new EventHandler(Clicked);
            this.pan.Children.Add(bHome);
            this.elements.Add(bHome);
            ArrowButtonRight bRight = new ArrowButtonRight();
            this.pan.Children.Add(bRight);
            this.elements.Add(bRight);
        }

        public override void MoveNext(AriadneThread.AriadneThreadCallback call, String name)
        {
            ArrowButtonMiddle bMid = new ArrowButtonMiddle();
            this.elements.Insert(this.elements.Count - 1, bMid);
            this.pan.Children.Insert(this.pan.Children.Count - 1, bMid);

            ArrowButtonBody bBody = new ArrowButtonBody();
            bBody.Text = name;
            bBody.Callback = call;
            this.elements.Insert(this.elements.Count - 1, bBody);
            this.pan.Children.Insert(this.pan.Children.Count - 1, bBody);
            bBody.Click += new EventHandler(Clicked);

            if (this.pan.Children.Count > this.MaximumElement * 2 + 2)
                this.pan.Children.RemoveRange(1, 2);
        }

        public override void MoveBack()
        {
            this.MoveBack(true);
        }

        protected override void MoveBack(bool executeCallback)
        {
            if (this.elements.Count > 2)
            {
                this.elements.RemoveRange(this.elements.Count - 3, 2);
                this.pan.Children.RemoveRange(this.pan.Children.Count - 3, 2);

                long pos = this.elements.Count - this.MaximumElement * 2;
                if (pos >= 2)
                {
                    ArrowButtonMiddle mid = this.elements[(int)pos - 1] as ArrowButtonMiddle;
                    this.pan.Children.Insert(1, mid);
                    ArrowButtonBody bod = this.elements[(int)pos] as ArrowButtonBody;
                    this.pan.Children.Insert(2, bod);
                }

                ArrowButtonBody body = this.pan.Children[this.pan.Children.Count - 2] as ArrowButtonBody;
                if (executeCallback && body.Callback != null)
                    body.Callback();
            }
        }

        protected override void Clicked(Object sender, EventArgs e)
        {
            if (sender != this.elements[this.elements.Count - 2] && this.elements.Count > 2)
            {
                while (sender != this.elements[this.elements.Count - 4])
                    this.MoveBack(this.ExecuteAllCallbacks);
                this.MoveBack();
            }
        }

    }
}
