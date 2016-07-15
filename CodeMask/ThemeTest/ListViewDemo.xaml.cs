using System.Windows;
using System.Windows.Controls;

namespace ThemeTest
{
    /// <summary>
    ///     ListViewDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ListViewDemo : UserControl
    {
        public ListViewDemo()
        {
            InitializeComponent();
            Loaded += ListViewDemo_Loaded;
        }

        private void ListViewDemo_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = new DataGridDemoViewModel();
            for (var i = 0; i < 50; i++)
            {
                vm.Students.Add(new Student {ID = i*1000, Desc = "this is test", Name = "ID" + i});
            }
            //vm.Students.First
            DataContext = vm;
        }
    }
}