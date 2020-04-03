using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormControls
{
	public class TreeNode
	{
		private List<TreeNode> _children;
		private TreeNode _parent;
		private TreeView _owner;
		private int _state;
		private object _data;

		internal void OnPaint(Graphics g, Font f, Brush b, ref float y)
		{
			var text = (_data as string) ?? "";
			var size = g.MeasureString(text, f);
			g.DrawString(text, f, b, 0f, y);
			y += size.Height;

			//Draw children
			if(_children != null)
				foreach (var node in _children)
					node.OnPaint(g, f, b, ref y);
		}
	}
}
