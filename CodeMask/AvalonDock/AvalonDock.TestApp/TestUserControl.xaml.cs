/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.Windows;
using System.Windows.Controls;

namespace AvalonDock.TestApp
{
    /// <summary>
    ///     Interaction logic for TestUserControl.xaml
    /// </summary>
    public partial class TestUserControl : UserControl
    {
        public TestUserControl()
        {
            InitializeComponent();

            Loaded += TestUserControl_Loaded;
            Unloaded += TestUserControl_Unloaded;
        }

        private void TestUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void TestUserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}