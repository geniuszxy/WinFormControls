using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		class Item : WinFormControls.ISimpleListBoxItem
		{
			public string value;

			public void Draw(Graphics g)
			{
				g.DrawString(value, SystemFonts.DefaultFont, SystemBrushes.InfoText, 0, 0);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			simpleListBox1.AddItem(new Item { value = "1" });
			simpleListBox1.AddItem(new Item { value = "2" });
			simpleListBox1.AddItem(new Item { value = "3" });
			simpleListBox1.AddItem(new Item { value = "-------------" });
			simpleListBox1.AddItem(new Item { value = "5" });
		}
	}
}
