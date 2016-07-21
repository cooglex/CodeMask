/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

namespace CodeMask.WPF.AvalonDock.Layout
{
    internal interface ILayoutPreviousContainer
    {
        ILayoutContainer PreviousContainer { get; set; }

        string PreviousContainerId { get; set; }
    }
}