/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Globalization;
using System.Windows.Data;
using CodeMask.WPF.AvalonDock.Layout;

namespace CodeMask.WPF.AvalonDock.Converters
{
    [ValueConversion(typeof (AnchorSide), typeof (double))]
    public class AnchorSideToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var side = (AnchorSide) value;
            if (side == AnchorSide.Left ||
                side == AnchorSide.Right)
                return 90.0;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}