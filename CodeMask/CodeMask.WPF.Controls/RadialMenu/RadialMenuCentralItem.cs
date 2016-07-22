using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.RadialMenu
{
    /// <summary>
    /// Interaction logic for RadialMenuCentralItem.xaml
    /// </summary>
    public class RadialMenuCentralItem : Button
    {
        static RadialMenuCentralItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadialMenuCentralItem), new FrameworkPropertyMetadata(typeof(RadialMenuCentralItem)));
        }
    }
}
