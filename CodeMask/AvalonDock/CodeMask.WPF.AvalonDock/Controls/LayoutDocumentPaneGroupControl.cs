/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;
using System.Windows.Controls;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutDocumentPaneGroupControl : LayoutGridControl<ILayoutDocumentPane>, ILayoutControl
    {
        private readonly LayoutDocumentPaneGroup _model;

        internal LayoutDocumentPaneGroupControl(LayoutDocumentPaneGroup model)
            : base(model, model.Orientation)
        {
            _model = model;
        }

        protected override void OnFixChildrenDockLengths()
        {
            #region Setup DockWidth/Height for children

            if (_model.Orientation == Orientation.Horizontal)
            {
                for (var i = 0; i < _model.Children.Count; i++)
                {
                    var childModel = _model.Children[i] as ILayoutPositionableElement;
                    if (!childModel.DockWidth.IsStar)
                    {
                        childModel.DockWidth = new GridLength(1.0, GridUnitType.Star);
                    }
                }
            }
            else
            {
                for (var i = 0; i < _model.Children.Count; i++)
                {
                    var childModel = _model.Children[i] as ILayoutPositionableElement;
                    if (!childModel.DockHeight.IsStar)
                    {
                        childModel.DockHeight = new GridLength(1.0, GridUnitType.Star);
                    }
                }
            }

            #endregion
        }
    }
}