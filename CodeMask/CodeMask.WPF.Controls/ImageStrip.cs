using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeMask.WPF.Controls
{
    /// <summary>
    /// </summary>
    public class ImageStrip : Control
    {
        #region override methods

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Image != null)
            {
                var rect = new Rect(0, 0, RenderSize.Width, RenderSize.Height);

                var brush = new ImageBrush(Image);
                brush.Stretch = Stretch.None;
                brush.Viewbox = (Orientation == Orientation.Vertical)
                    ? new Rect(0, (((Frame + 0.5)*FrameSize)/Image.Height) - 0.5, 1, 1)
                    : new Rect((((Frame + 0.5)*FrameSize)/Image.Width) - 0.5, 0, 1, 1);

                drawingContext.DrawRectangle(brush, null, rect);
            }
        }

        #endregion

        #region properties

        #region Frame

        /// <summary>
        ///     获取或设置当前帧。
        /// </summary>
        public int Frame
        {
            get { return (int) GetValue(FrameProperty); }
            set { SetValue(FrameProperty, value); }
        }

        public static readonly DependencyProperty FrameProperty =
            DependencyProperty.Register("Frame", typeof (int), typeof (ImageStrip),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region FrameSize

        /// <summary>
        ///     帧大小
        /// </summary>
        public double FrameSize
        {
            get { return (double) GetValue(FrameSizeProperty); }
            set { SetValue(FrameSizeProperty, value); }
        }

        public static readonly DependencyProperty FrameSizeProperty =
            DependencyProperty.Register("FrameSize", typeof (double), typeof (ImageStrip),
                new FrameworkPropertyMetadata(0D, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region Image

        /// <summary>
        ///     帧方式存储的图片。
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource) GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof (ImageSource), typeof (ImageStrip),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #region Orientation

        /// <summary>
        ///     获取或设置帧图片的排列方式。
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof (Orientation), typeof (ImageStrip),
                new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsRender));

        #endregion

        #endregion
    }
}