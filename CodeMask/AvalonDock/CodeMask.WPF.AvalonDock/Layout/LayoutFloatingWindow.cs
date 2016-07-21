/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CodeMask.WPF.AvalonDock.Layout
{
    [Serializable]
    [XmlInclude(typeof (LayoutAnchorableFloatingWindow))]
    [XmlInclude(typeof (LayoutDocumentFloatingWindow))]
    public abstract class LayoutFloatingWindow : LayoutElement, ILayoutContainer
    {
        public abstract bool IsValid { get; }


        public abstract IEnumerable<ILayoutElement> Children { get; }

        public abstract void RemoveChild(ILayoutElement element);

        public abstract void ReplaceChild(ILayoutElement oldElement, ILayoutElement newElement);

        public abstract int ChildrenCount { get; }
    }
}