/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Collections.Generic;

namespace CodeMask.WPF.AvalonDock.Layout
{
    public interface ILayoutContainer : ILayoutElement
    {
        IEnumerable<ILayoutElement> Children { get; }
        int ChildrenCount { get; }
        void RemoveChild(ILayoutElement element);
        void ReplaceChild(ILayoutElement oldElement, ILayoutElement newElement);
    }
}