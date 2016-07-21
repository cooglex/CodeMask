﻿/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Collections.Generic;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal interface IOverlayWindow
    {
        IEnumerable<IDropTarget> GetTargets();

        void DragEnter(LayoutFloatingWindowControl floatingWindow);
        void DragLeave(LayoutFloatingWindowControl floatingWindow);

        void DragEnter(IDropArea area);
        void DragLeave(IDropArea area);

        void DragEnter(IDropTarget target);
        void DragLeave(IDropTarget target);
        void DragDrop(IDropTarget target);
    }
}