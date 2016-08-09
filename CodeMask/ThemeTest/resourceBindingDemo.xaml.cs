using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ThemeTest
{
    /// <summary>
    ///     resourceBindingDemo.xaml 的交互逻辑
    /// </summary>
    public partial class resourceBindingDemo : UserControl
    {
        private readonly ObservableCollection<string> Buttons = new ObservableCollection<string>();

        public resourceBindingDemo()
        {
            InitializeComponent();
            Loaded += ResourceBindingDemo_Loaded;
        }

        private void ResourceBindingDemo_Loaded(object sender, RoutedEventArgs e)
        {
            Buttons.Add("TextBlockKey");
            //Buttons.Add("TextBlockKey");
            ic.ItemsSource = Buttons;
        }
    }
}