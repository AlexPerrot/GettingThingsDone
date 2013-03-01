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
using System.Collections;

namespace GTDLibrary
{
    /// <summary>
    /// Logique d'interaction pour ArianneThread.xaml
    /// </summary>
    public partial class ArianneThread : UserControl
    {
        private long maximumElement = 0;
        private ArrayList elements = null;
        
        public long GetMaximumElement()
        {
            return this.maximumElement;
        }

        public void SetMaximumElement(long max)
        {
            this.maximumElement = max;
        }

        public String GetCurrent()
        {
            if (this.elements.Count > 0)
                return (this.elements[this.elements.Count - 1] as Label).Content.ToString();
            return null;
        }

        public ArianneThread()
        {
            InitializeComponent();

            this.maximumElement = 1;
            this.elements = new ArrayList();
        }

        public ArianneThread(long maximumElement) : this()
        {
            this.maximumElement = maximumElement;
        }

        public void GoNext(String name)
        {
            if (this.elements.Count > 0)
            {
                Label l = (this.elements[this.elements.Count - 1] as Label);
                this.elements.Remove(l);
                this.stackPanelPath.Children.Remove(l);

                LinkLabel ll = new LinkLabel(l.Content.ToString());
                this.elements.Add(ll);
                this.stackPanelPath.Children.Add(ll);
            }

            Label lab = new Label();
            lab.Content = name;
            this.elements.Add(lab);
            this.stackPanelPath.Children.Add(lab);

            if (this.stackPanelPath.Children.Count > this.maximumElement)
            {
                this.stackPanelPath.Children.RemoveAt(0);
            }
        }

        public void GoBack()
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
                this.elements.Add(l);
                this.stackPanelPath.Children.Add(l);

                long pos = this.elements.Count - this.maximumElement;
                if (pos >= 0)
                {
                    LinkLabel llOld = this.elements[(int)(pos)] as LinkLabel;
                    this.stackPanelPath.Children.Insert(0, llOld);
                    /*UIElementCollection coll = this.stackPanelPath.Children;
                    this.stackPanelPath.Children.Clear();
                    this.stackPanelPath.Children.Add(llOld);
                    foreach (UIElement item in coll)
                    {
                        //this.stackPanelPath.Children.Add(item);
                    }*/
                }
            }
        }

        private void buttonHome_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

    }

}
