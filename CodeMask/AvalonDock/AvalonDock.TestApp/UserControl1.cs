/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Windows.Forms;

namespace AvalonDock.TestApp
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            label1.Text = textBox1.Handle.ToString();
            label2.Text = textBox2.Handle.ToString();
        }
    }
}