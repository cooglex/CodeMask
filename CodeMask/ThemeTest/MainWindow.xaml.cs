using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CodeMask.WPF.Commands;
using CodeMask.WPF.Controls.CustomWindow;

namespace ThemeTest
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : CustomChromeWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private DelegateCommand ShowImage { get; set; }

        public DelegateCommand<object> ClickCommand { get; set; }

        public ObservableCollection<TestClass> CollList { get; set; } = new ObservableCollection<TestClass>();

        private void OnClick(object e)
        {
            MessageBox.Show("asdad");
        }

        private bool CanExecute(object e)
        {
            return true;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowImage = new DelegateCommand(Showimage);
            ClickCommand = new DelegateCommand<object>(OnClick, CanExecute);
            for (var i = 0; i < 100; i++)
                CollList.Add(new TestClass {ID = i*1000, Desc = "Index:" + i*100000, Note = "Note" + 10000});
            DataContext = this;
            var test = new TestClass();
        }

        private void Showimage()
        {
            MessageBox.Show("asdad");
        }
    }

    public class TestClass : DependencyObject
    {
        public int ID { get; set; }
        public string Desc { get; set; }
        public string Note { get; set; }
    }


    public class DotBorder : Border
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinesProperty =
            DependencyProperty.Register("Lines", typeof (BorderThickLines), typeof (DotBorder),
                new UIPropertyMetadata(new BorderThickLines()));

        public BorderThickLines Lines
        {
            get { return (BorderThickLines) GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }


        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawRectangle(Background, new Pen(), new Rect(RenderSize));
            if (Lines != null)
            {
                double leftOffset = 0;
                foreach (var line in Lines)
                {
                    if (line.Position == Position.Left)
                    {
                        dc.DrawLine(new Pen(line.Fill, line.Thickness), new Point(0, leftOffset),
                            new Point(0, leftOffset + line.Length));
                        leftOffset += line.Length;
                    }
                }
            }
        }
    }

    public class BorderThickLines : ObservableCollection<BorderThickLine>
    {
    }

    public class BorderThickLine
    {
        public Position Position { get; set; }
        public double Thickness { get; set; }
        public double Length { get; set; }
        public Brush Fill { get; set; }
    }


    public enum Position
    {
        Top,
        Left,
        Bottom,
        Right
    }
}