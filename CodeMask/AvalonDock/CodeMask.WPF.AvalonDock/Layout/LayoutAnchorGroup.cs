/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace CodeMask.WPF.AvalonDock.Layout
{
    [ContentProperty("Children")]
    [Serializable]
    public class LayoutAnchorGroup : LayoutGroup<LayoutAnchorable>, ILayoutPreviousContainer, ILayoutPaneSerializable
    {
        private string _id;

        string ILayoutPaneSerializable.Id
        {
            get { return _id; }
            set { _id = value; }
        }

        string ILayoutPreviousContainer.PreviousContainerId { get; set; }

        protected override bool GetVisibility()
        {
            return Children.Count > 0;
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (_id != null)
                writer.WriteAttributeString("Id", _id);
            if (_previousContainer != null)
            {
                var paneSerializable = _previousContainer as ILayoutPaneSerializable;
                if (paneSerializable != null)
                {
                    writer.WriteAttributeString("PreviousContainerId", paneSerializable.Id);
                }
            }

            base.WriteXml(writer);
        }

        public override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToAttribute("Id"))
                _id = reader.Value;
            if (reader.MoveToAttribute("PreviousContainerId"))
                ((ILayoutPreviousContainer) this).PreviousContainerId = reader.Value;


            base.ReadXml(reader);
        }

        #region PreviousContainer

        [field: NonSerialized] private ILayoutContainer _previousContainer;

        [XmlIgnore]
        ILayoutContainer ILayoutPreviousContainer.PreviousContainer
        {
            get { return _previousContainer; }
            set
            {
                if (_previousContainer != value)
                {
                    _previousContainer = value;
                    RaisePropertyChanged("PreviousContainer");
                    var paneSerializable = _previousContainer as ILayoutPaneSerializable;
                    if (paneSerializable != null &&
                        paneSerializable.Id == null)
                        paneSerializable.Id = Guid.NewGuid().ToString();
                }
            }
        }

        #endregion
    }
}