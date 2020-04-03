using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormControls
{
	public partial class TreeView : UserControl
	{
		private List<TreeNode> _roots;

		public TreeView()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var f = Font;
			var b = Brushes.Black;
			var y = 0f;

			foreach (var node in _roots)
				node.OnPaint(g, f, b, ref y);

			base.OnPaint(e);
		}

		public void AddNode(TreeNode node)
		{
			_roots.Add(node);		}
	}
}
