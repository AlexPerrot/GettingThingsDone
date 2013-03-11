using GettingThingsDone.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GettingThingsDone.src.view
{
    class TaskDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SingleTaskTemplate { get; set; }
        public DataTemplate ProjectTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IProject)
                return ProjectTemplate;
            else if (item is ISingleTask)
                return SingleTaskTemplate;
            else
                return base.SelectTemplate(item, container);
        }
    }
}
