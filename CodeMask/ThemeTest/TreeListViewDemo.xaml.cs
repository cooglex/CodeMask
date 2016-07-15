using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ThemeTest
{
    /// <summary>
    ///     TreeListViewDemo.xaml 的交互逻辑
    /// </summary>
    public partial class TreeListViewDemo : UserControl
    {
        public TreeListViewDemo()
        {
            InitializeComponent();
            Loaded += TreeListViewDemo_Loaded;
        }

        public ObservableCollection<TestClass2> TestSource { get; set; } = new ObservableCollection<TestClass2>();

        private void TreeListViewDemo_Loaded(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 100; i++)
            {
                var tc = new TestClass2 {ID = i + "", Desc = "第1级", Note = "bote1"};
                for (var j = 0; j < 30; j++)
                {
                    var tcj = new TestClass2 {ID = i + "-" + j, Desc = "第2级", Note = "bote2"};
                    for (var k = 0; k < 30; k++)
                    {
                        var tck = new TestClass2 {ID = i + "-" + j + "-" + k, Desc = "第三级", Note = "bote3"};
                        tcj.Children.Add(tck);
                    }
                    tc.Children.Add(tcj);
                }
                TestSource.Add(tc);
            }
            DataContext = this;
        }
    }

    public class TestClass2 : DependencyObject
    {
        public string ID { get; set; }
        public string Desc { get; set; }
        public string Note { get; set; }

        public ObservableCollection<TestClass2> Children { get; set; } = new ObservableCollection<TestClass2>();
    }
}