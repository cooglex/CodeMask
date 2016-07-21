﻿/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.ComponentModel;

namespace CodeMask.WPF.AvalonDock.Layout.Serialization
{
    public class LayoutSerializationCallbackEventArgs : CancelEventArgs
    {
        public LayoutSerializationCallbackEventArgs(LayoutContent model, object previousContent)
        {
            Cancel = false;
            Model = model;
            Content = previousContent;
        }

        public LayoutContent Model { get; private set; }

        public object Content { get; set; }
    }
}