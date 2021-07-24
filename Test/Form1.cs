using System;
using System.Windows.Forms;

namespace Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			var items = new string[]
			{
				"1",
				"2",
				"3",
				"-------------",
				"5",
			};

			simpleListBox1.AddItems(items);
		}
	}
}
