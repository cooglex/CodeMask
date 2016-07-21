/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Globalization;
using System.Windows;
using System.Xml;

namespace CodeMask.WPF.AvalonDock.Layout
{
    [Serializable]
    public abstract class LayoutPositionableGroup<T> : LayoutGroup<T>, ILayoutPositionableElement,
        ILayoutPositionableElementWithActualSize where T : class, ILayoutElement
    {
        private static readonly GridLengthConverter _gridLengthConverter = new GridLengthConverter();

        [NonSerialized] private double _actualHeight;


        [NonSerialized] private double _actualWidth;

        private GridLength _dockHeight = new GridLength(1.0, GridUnitType.Star);

        private GridLength _dockWidth = new GridLength(1.0, GridUnitType.Star);

        public GridLength DockWidth
        {
            get { return _dockWidth; }
            set
            {
                if (DockWidth != value)
                {
                    RaisePropertyChanging("DockWidth");
                    _dockWidth = value;
                    RaisePropertyChanged("DockWidth");

                    OnDockWidthChanged();
                }
            }
        }

        public GridLength DockHeight
        {
            get { return _dockHeight; }
            set
            {
                if (DockHeight != value)
                {
                    RaisePropertyChanging("DockHeight");
                    _dockHeight = value;
                    RaisePropertyChanged("DockHeight");

                    OnDockHeightChanged();
                }
            }
        }

        double ILayoutPositionableElementWithActualSize.ActualWidth
        {
            get { return _actualWidth; }
            set { _actualWidth = value; }
        }

        double ILayoutPositionableElementWithActualSize.ActualHeight
        {
            get { return _actualHeight; }
            set { _actualHeight = value; }
        }


        protected virtual void OnDockWidthChanged()
        {
        }

        protected virtual void OnDockHeightChanged()
        {
        }

        public override void WriteXml(XmlWriter writer)
        {
            if (DockWidth.Value != 1.0 || !DockWidth.IsStar)
                writer.WriteAttributeString("DockWidth", _gridLengthConverter.ConvertToInvariantString(DockWidth));
            if (DockHeight.Value != 1.0 || !DockHeight.IsStar)
                writer.WriteAttributeString("DockHeight", _gridLengthConverter.ConvertToInvariantString(DockHeight));

            if (DockMinWidth != 25.0)
                writer.WriteAttributeString("DocMinWidth", DockMinWidth.ToString(CultureInfo.InvariantCulture));
            if (DockMinHeight != 25.0)
                writer.WriteAttributeString("DockMinHeight", DockMinHeight.ToString(CultureInfo.InvariantCulture));

            if (FloatingWidth != 0.0)
                writer.WriteAttributeString("FloatingWidth", FloatingWidth.ToString(CultureInfo.InvariantCulture));
            if (FloatingHeight != 0.0)
                writer.WriteAttributeString("FloatingHeight", FloatingHeight.ToString(CultureInfo.InvariantCulture));
            if (FloatingLeft != 0.0)
                writer.WriteAttributeString("FloatingLeft", FloatingLeft.ToString(CultureInfo.InvariantCulture));
            if (FloatingTop != 0.0)
                writer.WriteAttributeString("FloatingTop", FloatingTop.ToString(CultureInfo.InvariantCulture));
            if (IsMaximized)
                writer.WriteAttributeString("IsMaximized", IsMaximized.ToString());

            base.WriteXml(writer);
        }

        public override void ReadXml(XmlReader reader)
        {
            if (reader.MoveToAttribute("DockWidth"))
                _dockWidth = (GridLength) _gridLengthConverter.ConvertFromInvariantString(reader.Value);
            if (reader.MoveToAttribute("DockHeight"))
                _dockHeight = (GridLength) _gridLengthConverter.ConvertFromInvariantString(reader.Value);

            if (reader.MoveToAttribute("DocMinWidth"))
                _dockMinWidth = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute("DocMinHeight"))
                _dockMinHeight = double.Parse(reader.Value, CultureInfo.InvariantCulture);

            if (reader.MoveToAttribute("FloatingWidth"))
                _floatingWidth = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute("FloatingHeight"))
                _floatingHeight = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute("FloatingLeft"))
                _floatingLeft = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute("FloatingTop"))
                _floatingTop = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute("IsMaximized"))
                _isMaximized = bool.Parse(reader.Value);

            base.ReadXml(reader);
        }

        #region DockMinWidth

        private double _dockMinWidth = 25.0;

        public double DockMinWidth
        {
            get { return _dockMinWidth; }
            set
            {
                if (_dockMinWidth != value)
                {
                    MathHelper.AssertIsPositiveOrZero(value);
                    RaisePropertyChanging("DockMinWidth");
                    _dockMinWidth = value;
                    RaisePropertyChanged("DockMinWidth");
                }
            }
        }

        #endregion

        #region DockMinHeight

        private double _dockMinHeight = 25.0;

        public double DockMinHeight
        {
            get { return _dockMinHeight; }
            set
            {
                if (_dockMinHeight != value)
                {
                    MathHelper.AssertIsPositiveOrZero(value);
                    RaisePropertyChanging("DockMinHeight");
                    _dockMinHeight = value;
                    RaisePropertyChanged("DockMinHeight");
                }
            }
        }

        #endregion

        #region FloatingWidth

        private double _floatingWidth;

        public double FloatingWidth
        {
            get { return _floatingWidth; }
            set
            {
                if (_floatingWidth != value)
                {
                    RaisePropertyChanging("FloatingWidth");
                    _floatingWidth = value;
                    RaisePropertyChanged("FloatingWidth");
                }
            }
        }

        #endregion

        #region FloatingHeight

        private double _floatingHeight;

        public double FloatingHeight
        {
            get { return _floatingHeight; }
            set
            {
                if (_floatingHeight != value)
                {
                    RaisePropertyChanging("FloatingHeight");
                    _floatingHeight = value;
                    RaisePropertyChanged("FloatingHeight");
                }
            }
        }

        #endregion

        #region FloatingLeft

        private double _floatingLeft;

        public double FloatingLeft
        {
            get { return _floatingLeft; }
            set
            {
                if (_floatingLeft != value)
                {
                    RaisePropertyChanging("FloatingLeft");
                    _floatingLeft = value;
                    RaisePropertyChanged("FloatingLeft");
                }
            }
        }

        #endregion

        #region FloatingTop

        private double _floatingTop;

        public double FloatingTop
        {
            get { return _floatingTop; }
            set
            {
                if (_floatingTop != value)
                {
                    RaisePropertyChanging("FloatingTop");
                    _floatingTop = value;
                    RaisePropertyChanged("FloatingTop");
                }
            }
        }

        #endregion

        #region IsMaximized

        private bool _isMaximized;

        public bool IsMaximized
        {
            get { return _isMaximized; }
            set
            {
                if (_isMaximized != value)
                {
                    _isMaximized = value;
                    RaisePropertyChanged("IsMaximized");
                }
            }
        }

        #endregion
    }
}