/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using AvalonDock.WinFormsTestApp.Properties;
using CodeMask.WPF.AvalonDock;
using CodeMask.WPF.AvalonDock.Layout.Serialization;
using CodeMask.WPF.AvalonDock.Themes;
using TextBox = System.Windows.Controls.TextBox;

namespace AvalonDock.WinFormsTestApp
{
    public partial class FormMain : Form
    {
        private readonly DockingManager _dockingManager = new DockingManager();

        public FormMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var serializer = new XmlLayoutSerializer(_dockingManager);

            serializer.LayoutSerializationCallback += (s, args) =>
            {
                switch (args.Model.ContentId)
                {
                    case "toolWindow1":
                        args.Content = new TextBlock {Text = args.Model.ContentId};
                        break;
                    default:
                        args.Content = new TextBox {Text = args.Model.ContentId};
                        break;
                }
            };

            serializer.Deserialize(
                new StringReader(
                    Settings.Default.DefaultLayout));


            //LayoutDocument doc = new LayoutDocument() { Title = "test" };
            //dockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().First().Children.Add(doc);

            dockingManagerHost.Child = _dockingManager;

            base.OnLoad(e);
        }


        private void menuItemAero_Click(object sender, EventArgs e)
        {
            _dockingManager.Theme = new AeroTheme();
            SetChecked(menuItemAero);
        }

        private void menuItemVS2010_Click(object sender, EventArgs e)
        {
            _dockingManager.Theme = new VS2010Theme();
            SetChecked(menuItemVS2010);
        }

        private void menuItemMetro_Click(object sender, EventArgs e)
        {
            _dockingManager.Theme = new MetroTheme();
            SetChecked(menuItemMetro);
        }

        private void menuItemGeneric_Click(object sender, EventArgs e)
        {
            _dockingManager.Theme = new GenericTheme();
            SetChecked(menuItemGeneric);
        }

        private void menuItemExpressionDark_Click(object sender, EventArgs e)
        {
            _dockingManager.Theme = new ExpressionDarkTheme();
            SetChecked(menuItemExpressionDark);
        }

        private void menuItemExpressionLight_Click(object sender, EventArgs e)
        {
            _dockingManager.Theme = new ExpressionLightTheme();
            SetChecked(menuItemExpressionLight);
        }

        private void SetChecked(ToolStripMenuItem toCheck)
        {
            menuItemAero.Checked = false;
            menuItemGeneric.Checked = false;
            menuItemVS2010.Checked = false;
            menuItemExpressionDark.Checked = false;
            menuItemMetro.Checked = false;

            toCheck.Checked = true;
        }
    }
}