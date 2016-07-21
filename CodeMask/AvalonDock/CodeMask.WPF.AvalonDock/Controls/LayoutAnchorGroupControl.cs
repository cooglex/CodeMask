/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutAnchorGroupControl : Control, ILayoutControl
    {
        private readonly LayoutAnchorGroup _model;

        static LayoutAnchorGroupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LayoutAnchorGroupControl),
                new FrameworkPropertyMetadata(typeof (LayoutAnchorGroupControl)));
        }


        internal LayoutAnchorGroupControl(LayoutAnchorGroup model)
        {
            _model = model;
            CreateChildrenViews();

            _model.Children.CollectionChanged += (s, e) => OnModelChildrenCollectionChanged(e);
        }

        public ObservableCollection<LayoutAnchorControl> Children { get; } =
            new ObservableCollection<LayoutAnchorControl>();

        public ILayoutElement Model
        {
            get { return _model; }
        }

        private void CreateChildrenViews()
        {
            var manager = _model.Root.Manager;
            foreach (var childModel in _model.Children)
            {
                Children.Add(new LayoutAnchorControl(childModel) {Template = manager.AnchorTemplate});
            }
        }

        private void OnModelChildrenCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    {
                        foreach (var childModel in e.OldItems)
                            Children.Remove(Children.First(cv => cv.Model == childModel));
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Reset)
                Children.Clear();

            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems != null)
                {
                    var manager = _model.Root.Manager;
                    var insertIndex = e.NewStartingIndex;
                    foreach (LayoutAnchorable childModel in e.NewItems)
                    {
                        Children.Insert(insertIndex++,
                            new LayoutAnchorControl(childModel) {Template = manager.AnchorTemplate});
                    }
                }
            }
        }
    }
}