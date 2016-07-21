/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CodeMask.WPF.AvalonDock.Layout
{
    [Serializable]
    public abstract class LayoutGroup<T> : LayoutGroupBase, ILayoutContainer, ILayoutGroup, IXmlSerializable
        where T : class, ILayoutElement
    {
        internal LayoutGroup()
        {
            Children.CollectionChanged += _children_CollectionChanged;
        }

        public ObservableCollection<T> Children { get; } = new ObservableCollection<T>();

        IEnumerable<ILayoutElement> ILayoutContainer.Children
        {
            get { return Children.Cast<ILayoutElement>(); }
        }

        public void RemoveChild(ILayoutElement element)
        {
            Children.Remove((T) element);
        }

        public void ReplaceChild(ILayoutElement oldElement, ILayoutElement newElement)
        {
            var index = Children.IndexOf((T) oldElement);
            Children.Insert(index, (T) newElement);
            Children.RemoveAt(index + 1);
        }

        public int ChildrenCount
        {
            get { return Children.Count; }
        }

        public void RemoveChildAt(int childIndex)
        {
            Children.RemoveAt(childIndex);
        }

        public int IndexOfChild(ILayoutElement element)
        {
            return Children.Cast<ILayoutElement>().ToList().IndexOf(element);
        }

        public void InsertChildAt(int index, ILayoutElement element)
        {
            Children.Insert(index, (T) element);
        }

        public void ReplaceChildAt(int index, ILayoutElement element)
        {
            Children[index] = (T) element;
        }


        public XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            if (reader.IsEmptyElement)
            {
                reader.Read();
                ComputeVisibility();
                return;
            }
            var localName = reader.LocalName;
            reader.Read();
            while (true)
            {
                if (reader.LocalName == localName &&
                    reader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }

                XmlSerializer serializer = null;
                if (reader.LocalName == "LayoutAnchorablePaneGroup")
                    serializer = new XmlSerializer(typeof (LayoutAnchorablePaneGroup));
                else if (reader.LocalName == "LayoutAnchorablePane")
                    serializer = new XmlSerializer(typeof (LayoutAnchorablePane));
                else if (reader.LocalName == "LayoutAnchorable")
                    serializer = new XmlSerializer(typeof (LayoutAnchorable));
                else if (reader.LocalName == "LayoutDocumentPaneGroup")
                    serializer = new XmlSerializer(typeof (LayoutDocumentPaneGroup));
                else if (reader.LocalName == "LayoutDocumentPane")
                    serializer = new XmlSerializer(typeof (LayoutDocumentPane));
                else if (reader.LocalName == "LayoutDocument")
                    serializer = new XmlSerializer(typeof (LayoutDocument));
                else if (reader.LocalName == "LayoutAnchorGroup")
                    serializer = new XmlSerializer(typeof (LayoutAnchorGroup));
                else if (reader.LocalName == "LayoutPanel")
                    serializer = new XmlSerializer(typeof (LayoutPanel));

                Children.Add((T) serializer.Deserialize(reader));
            }

            reader.ReadEndElement();
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            foreach (var child in Children)
            {
                var type = child.GetType();
                var serializer = new XmlSerializer(type);
                serializer.Serialize(writer, child);
            }
        }

        private void _children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    foreach (LayoutElement element in e.OldItems)
                    {
                        if (element.Parent == this)
                            element.Parent = null;
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems != null)
                {
                    foreach (LayoutElement element in e.NewItems)
                    {
                        if (element.Parent != this)
                        {
                            if (element.Parent != null)
                                element.Parent.RemoveChild(element);
                            element.Parent = this;
                        }
                    }
                }
            }

            ComputeVisibility();
            OnChildrenCollectionChanged();
            NotifyChildrenTreeChanged(ChildrenTreeChange.DirectChildrenChanged);
            RaisePropertyChanged("ChildrenCount");
        }


        public void MoveChild(int oldIndex, int newIndex)
        {
            if (oldIndex == newIndex)
                return;
            Children.Move(oldIndex, newIndex);
            ChildMoved(oldIndex, newIndex);
        }

        protected virtual void ChildMoved(int oldIndex, int newIndex)
        {
        }

        #region IsVisible

        private bool _isVisible = true;

        public bool IsVisible
        {
            get { return _isVisible; }
            protected set
            {
                if (_isVisible != value)
                {
                    RaisePropertyChanging("IsVisible");
                    _isVisible = value;
                    OnIsVisibleChanged();
                    RaisePropertyChanged("IsVisible");
                }
            }
        }

        protected virtual void OnIsVisibleChanged()
        {
            UpdateParentVisibility();
        }

        private void UpdateParentVisibility()
        {
            var parentPane = Parent as ILayoutElementWithVisibility;
            if (parentPane != null)
                parentPane.ComputeVisibility();
        }


        public void ComputeVisibility()
        {
            IsVisible = GetVisibility();
        }

        protected abstract bool GetVisibility();

        protected override void OnParentChanged(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            base.OnParentChanged(oldValue, newValue);

            ComputeVisibility();
        }

        #endregion
    }
}