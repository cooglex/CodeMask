using System.Windows;

namespace ThemeTest
{
    /// <summary>
    ///     App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //XamlXmlReader xr = new XamlXmlReader("Brushes.xaml");
            //ResourceDictionary re = new ResourceDictionary();
            //re.Source = new Uri("pack://siteOfOrigin:,,,/LightBrushes.xaml", UriKind.RelativeOrAbsolute);
            //Application.Current.Resources.MergedDictionaries.Add(re);
            base.OnStartup(e);
        }
    }
}