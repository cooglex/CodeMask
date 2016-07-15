using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ThemeTest
{
    /// <summary>
    ///     DataGridDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridDemo : UserControl
    {
        public DataGridDemo()
        {
            InitializeComponent();
            Loaded += DataGridDemo_Loaded;
        }

        private void DataGridDemo_Loaded(object sender, RoutedEventArgs e)
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

    public class DataGridDemoViewModel
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}