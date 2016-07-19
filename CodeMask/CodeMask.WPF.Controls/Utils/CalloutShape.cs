using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CodeMask.WPF.Controls.Utils
{
    /// <summary>
    /// </summary>
    public class CalloutShape : Shape
    {
        #region Fields 

        private Rect _rect;

        #endregion Fields 

        #region Protected Methods 

        protected override Size ArrangeOverride(Size finalSize)
        {
            var penThickness = GetStrokeThickness();
            var margin = penThickness/2;

            _rect = new Rect(margin, margin,
                Math.Max(0, finalSize.Width - penThickness),
                Math.Max(0, finalSize.Height - penThickness));

            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return GetNaturalSize();
        }

        #endregion Protected Methods 

        #region Private Methods 

        private Size GetNaturalSize()
        {
            var strokeThickness = GetStrokeThickness();
            return new Size(strokeThickness, strokeThickness);
        }

        private double GetStrokeThickness()
        {
            var strokeThickness = StrokeThickness;
            var isPenNoOp = true;
            if ((Stroke != null) && !double.IsNaN(strokeThickness))
            {
                isPenNoOp = (strokeThickness == 0);
            }
            return isPenNoOp ? 0.0d : Math.Abs(strokeThickness);
        }

        #endregion Private Methods 

        #region Properties 

        protected override Geometry DefiningGeometry
        {
            get
            {
                // 计算矩形边界

                var top = Math.Max(0, _rect.Top);
                var left = Math.Max(0, _rect.Left);
                var width = Math.Max(0, _rect.Width);
                var height = Math.Max(0, _rect.Height);

                switch (ArrowPlacement)
                {
                    case ArrowPlacement.Top:
                        top += ArrowHeight;
                        height -= ArrowHeight;
                        break;
                    case ArrowPlacement.Bottom:
                        height -= ArrowHeight;
                        break;
                    case ArrowPlacement.Left:
                        left += ArrowWidth;
                        width -= ArrowWidth;
                        break;
                    case ArrowPlacement.Right:
                        width -= ArrowWidth;
                        break;
                }

                height = Math.Max(height, 0);
                width = Math.Max(width, 0);

                var rectangle = new RectangleGeometry(new Rect(left, top, width, height), 0, 0);

                // 不合理的设置，返回
                if (ArrowWidth == 0 || ArrowHeight == 0 || ArrowPlacement == ArrowPlacement.None) return rectangle;

                // 开始计算箭头几何
                var p1 = new Point();
                var p2 = new Point();
                var p3 = new Point();
                var s = GetStrokeThickness();
                switch (ArrowStyle)
                {
                    case ArrowStyle.RightTriangle:
                        switch (ArrowPlacement)
                        {
                            case ArrowPlacement.Top:
                                if (ArrowAlignment == ArrowAlignment.Left)
                                {
                                    p1 = new Point(s/2, ArrowHeight + s/2);
                                    p2 = new Point(s/2, s);
                                    p3 = new Point(ArrowWidth, ArrowHeight + s);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Right)
                                {
                                    p1 = new Point(width - (ArrowWidth + s/2), ArrowHeight + s);
                                    p2 = new Point(width + s/2, s);
                                    p3 = new Point(width + s/2, ArrowHeight + s);
                                }
                                break;

                            case ArrowPlacement.Bottom:
                                if (ArrowAlignment == ArrowAlignment.Left)
                                {
                                    p1 = new Point(s/2, height - s);
                                    p2 = new Point(s/2, height + ArrowHeight);
                                    p3 = new Point(ArrowWidth, height - s/2);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Right)
                                {
                                    p1 = new Point(width - (ArrowWidth + StrokeThickness/2), height - s);
                                    p2 = new Point(width + s/2, height + ArrowHeight);
                                    p3 = new Point(width + s/2, height - s);
                                }
                                break;
                            case ArrowPlacement.Left:
                                if (ArrowAlignment == ArrowAlignment.Down)
                                {
                                    p1 = new Point(s/2, height + s/2);
                                    p2 = new Point(ArrowWidth + s/2, height + s/2);
                                    p3 = new Point(ArrowWidth + s/2, height - ArrowHeight);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Up)
                                {
                                    p1 = new Point(s, s/2);
                                    p2 = new Point(ArrowWidth + s/2, s/2);
                                    p3 = new Point(ArrowWidth + s/2, ArrowHeight);
                                }
                                break;
                            case ArrowPlacement.Right:
                                if (ArrowAlignment == ArrowAlignment.Down)
                                {
                                    p1 = new Point(width, height + s/2);
                                    p2 = new Point(width + ArrowWidth, height + s/2);
                                    p3 = new Point(width, height - ArrowHeight);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Up)
                                {
                                    p1 = new Point(width - s/2,s/2);
                                    p2 = new Point(width + ArrowWidth, s/2);
                                    p3 = new Point(width, ArrowHeight + s);
                                }
                                break;
                            default:
                                p1 = new Point();
                                p2 = new Point();
                                p3 = new Point();
                                break;
                        }
                        break;
                    case ArrowStyle.IsoscelesTriangle:
                        switch (ArrowPlacement)
                        {
                            case ArrowPlacement.Top:
                                if (ArrowAlignment == ArrowAlignment.Left)
                                {
                                    p1 = new Point(s/2, ArrowHeight + s/2);
                                    p2 = new Point(ArrowWidth/2 + s/2, s);
                                    p3 = new Point(ArrowWidth, ArrowHeight + s);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Right)
                                {
                                    p1 = new Point(width - (ArrowWidth + s/2), ArrowHeight + s);
                                    p2 = new Point(width - ArrowWidth/2 + s/2, s);
                                    p3 = new Point(width + s/2, ArrowHeight + s);
                                }
                                break;

                            case ArrowPlacement.Bottom:
                                if (ArrowAlignment == ArrowAlignment.Left)
                                {
                                    p1 = new Point(s/2, height - s);
                                    p2 = new Point(s/2 + ArrowWidth/2, height + ArrowHeight);
                                    p3 = new Point(ArrowWidth, height - s/2);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Right)
                                {
                                    p1 = new Point(width - (ArrowWidth + StrokeThickness/2), height - s);
                                    p2 = new Point(width - ArrowWidth/2 + s/2, height + ArrowHeight);
                                    p3 = new Point(width + s/2, height - s);
                                }
                                break;
                            case ArrowPlacement.Left:
                                if (ArrowAlignment == ArrowAlignment.Down)
                                {
                                    p1 = new Point(s/2, height - ArrowHeight/2 + s/2);
                                    p2 = new Point(ArrowWidth + s/2, height + s/2);
                                    p3 = new Point(ArrowWidth + s/2, height - ArrowHeight);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Up)
                                {
                                    p1 = new Point(s, ArrowHeight/2 + s/2);
                                    p2 = new Point(ArrowWidth + s/2, s/2);
                                    p3 = new Point(ArrowWidth + s/2, ArrowHeight);
                                }
                                break;
                            case ArrowPlacement.Right:
                                if (ArrowAlignment == ArrowAlignment.Down)
                                {
                                    p1 = new Point(width, height + s/2);
                                    p2 = new Point(width + ArrowWidth, height - ArrowHeight/2 + s/2);
                                    p3 = new Point(width, height - ArrowHeight);
                                }
                                else if (ArrowAlignment == ArrowAlignment.Up)
                                {
                                    p1 = new Point(width - s/2,  s/2);
                                    p2 = new Point(width + ArrowWidth, ArrowHeight / 2 + s /2);
                                    p3 = new Point(width, ArrowHeight + s);
                                }
                                break;
                            default:
                                p1 = new Point();
                                p2 = new Point();
                                p3 = new Point();
                                break;
                        }
                        break;
                    default:
                        break;
                }

                var p = new PathFigure
                {
                    StartPoint = p1,
                    Segments = new PathSegmentCollection
                    {
                        new LineSegment(p1, false),
                        new LineSegment(p2, true),
                        new LineSegment(p3, true),
                        new LineSegment(p1, false)
                    }
                };
                var arrow = new PathGeometry();
                arrow.Figures.Add(p);

                // 合并几何
                return new CombinedGeometry(GeometryCombineMode.Union, arrow, rectangle);
            }
        }

        #region ArrowWidth

        public static readonly DependencyProperty ArrowWidthProperty =
            DependencyProperty.Register("ArrowWidth", typeof (double), typeof (CalloutShape),
                new FrameworkPropertyMetadata(18.0,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     获取或设置箭头宽度。
        /// </summary>
        public double ArrowWidth
        {
            get { return (double) GetValue(ArrowWidthProperty); }
            set { SetValue(ArrowWidthProperty, value); }
        }

        #endregion

        #region ArrowHeight

        public static readonly DependencyProperty ArrowHeightProperty =
            DependencyProperty.Register("ArrowHeight", typeof (double), typeof (CalloutShape),
                new FrameworkPropertyMetadata(12.0,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     获取或设置箭头高度。
        /// </summary>
        public double ArrowHeight
        {
            get { return (double) GetValue(ArrowHeightProperty); }
            set { SetValue(ArrowHeightProperty, value); }
        }

        #endregion

        #region ArrowPlacement

        public static readonly DependencyProperty ArrowPlacementProperty =
            DependencyProperty.Register("ArrowPlacement", typeof (ArrowPlacement), typeof (CalloutShape),
                new FrameworkPropertyMetadata(ArrowPlacement.Top,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     获取或设置箭头的位置。
        /// </summary>
        public ArrowPlacement ArrowPlacement
        {
            get { return (ArrowPlacement) GetValue(ArrowPlacementProperty); }
            set { SetValue(ArrowPlacementProperty, value); }
        }

        #endregion

        #region ArrowAlignment

        public static readonly DependencyProperty ArrowAlignmentProperty =
            DependencyProperty.Register("ArrowAlignment", typeof (ArrowAlignment), typeof (CalloutShape),
                new FrameworkPropertyMetadata(ArrowAlignment.Left,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        ///     获取或设置箭头对齐方式。
        /// </summary>
        public ArrowAlignment ArrowAlignment
        {
            get { return (ArrowAlignment) GetValue(ArrowAlignmentProperty); }
            set { SetValue(ArrowAlignmentProperty, value); }
        }

        #endregion

        #region ArrowStyle

        /// <summary>
        ///     获取或设置箭头样式。
        /// </summary>
        public ArrowStyle ArrowStyle
        {
            get { return (ArrowStyle) GetValue(ArrowStyleProperty); }
            set { SetValue(ArrowStyleProperty, value); }
        }

        public static readonly DependencyProperty ArrowStyleProperty =
            DependencyProperty.Register("ArrowStyle", typeof (ArrowStyle), typeof (CalloutShape),
                new PropertyMetadata(ArrowStyle.RightTriangle));

        #endregion

        #endregion Properties 
    }

    /// <summary>
    ///     箭头位置。
    /// </summary>
    public enum ArrowPlacement
    {
        /// <summary>
        ///     顶部。
        /// </summary>
        Top,

        /// <summary>
        ///     底部。
        /// </summary>
        Bottom,

        /// <summary>
        ///     无。
        /// </summary>
        None,

        /// <summary>
        ///     左边。
        /// </summary>
        Left,

        /// <summary>
        ///     右边。
        /// </summary>
        Right
    }

    /// <summary>
    ///     箭头对齐方式。
    /// </summary>
    public enum ArrowAlignment
    {
        /// <summary>
        ///     居左。
        /// </summary>
        Left,

        /// <summary>
        ///     居右。
        /// </summary>
        Right,

        /// <summary>
        ///     顶部。
        /// </summary>
        Up,

        /// <summary>
        ///     底部。
        /// </summary>
        Down
    }

    /// <summary>
    ///     箭头样式。
    /// </summary>
    public enum ArrowStyle
    {
        /// <summary>
        ///     直角三角形。
        /// </summary>
        RightTriangle,

        /// <summary>
        ///     等腰三角形。
        /// </summary>
        IsoscelesTriangle
    }
}