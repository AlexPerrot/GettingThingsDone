using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace GTDLibrary.utils
{
    internal class AriadneThreadElementBuilderFactory
    {
        public static IAriadneThreadElementBuilder Create(AriadneThreadStyle style, Panel pan, long maximumElement, bool executeAllCallback)
        {
            if (style == AriadneThreadStyle.Button)
                return new AriadneThreadElementBuilderButton(pan, maximumElement, executeAllCallback);
            if (style == AriadneThreadStyle.Link)
                return new AriadneThreadElementBuilderLinkLabel(pan, maximumElement, executeAllCallback);
            return null;
        }
    }
}
