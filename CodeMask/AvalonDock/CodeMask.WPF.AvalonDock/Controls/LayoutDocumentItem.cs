/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutDocumentItem : LayoutItem
    {
        private LayoutDocument _document;

        internal LayoutDocumentItem()
        {
        }

        internal override void Attach(LayoutContent model)
        {
            _document = model as LayoutDocument;
            base.Attach(model);
        }

        protected override void Close()
        {
            var dockingManager = _document.Root.Manager;
            dockingManager._ExecuteCloseCommand(_document);
        }

        internal override void Detach()
        {
            _document = null;
            base.Detach();
        }

        #region Description

        /// <summary>
        ///     Description Dependency Property
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof (string), typeof (LayoutDocumentItem),
                new FrameworkPropertyMetadata(null,
                    OnDescriptionChanged));

        /// <summary>
        ///     Gets or sets the Description property.  This dependency property
        ///     indicates the description to display for the document item.
        /// </summary>
        public string Description
        {
            get { return (string) GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        /// <summary>
        ///     Handles changes to the Description property.
        /// </summary>
        private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LayoutDocumentItem) d).OnDescriptionChanged(e);
        }

        /// <summary>
        ///     Provides derived classes an opportunity to handle changes to the Description property.
        /// </summary>
        protected virtual void OnDescriptionChanged(DependencyPropertyChangedEventArgs e)
        {
            _document.Description = (string) e.NewValue;
        }

        #endregion
    }
}