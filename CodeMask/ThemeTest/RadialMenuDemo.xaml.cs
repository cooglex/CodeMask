using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThemeTest
{
    /// <summary>
    /// RadialMenuDemo.xaml 的交互逻辑
    /// </summary>
    public partial class RadialMenuDemo : UserControl
    {
        public RadialMenuDemo()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Canvas.SetLeft(radialMenu,e.GetPosition(root).X);
            Canvas.SetTop(radialMenu, e.GetPosition(root).Y);
            radialMenu.IsOpen = true;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            radialMenu.IsOpen = false;
        }
    }
}
