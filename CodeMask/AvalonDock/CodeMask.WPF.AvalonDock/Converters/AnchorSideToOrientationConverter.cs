/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Converters
{
    [ValueConversion(typeof (AnchorSide), typeof (Orientation))]
    public class AnchorSideToOrientationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var side = (AnchorSide) value;
            if (side == AnchorSide.Left ||
                side == AnchorSide.Right)
                return Orientation.Vertical;

            return Orientation.Horizontal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}