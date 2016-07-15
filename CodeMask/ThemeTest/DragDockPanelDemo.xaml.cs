using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ThemeTest
{
    /// <summary>
    ///     DragDockPanelDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DragDockPanelDemo : UserControl
    {
        public DragDockPanelDemo()
        {
            InitializeComponent();
            Loaded += DragDockPanelDemo_Loaded;
        }

        private void DragDockPanelDemo_Loaded(object sender, RoutedEventArgs e)
        {
            var lists = new List<int>();
            lists.Add(1);
            lists.Add(2);
            lists.Add(3);
            lists.Add(4);
            dragDockPanelHostWithItemTemplate.ItemsSource = lists;
        }
    }
}