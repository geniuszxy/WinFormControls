using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormControls
{
	public partial class SimpleListBox : UserControl
	{
		private List<ISimpleListBoxItem> _items;

		public SimpleListBox()
		{
			_items = new List<ISimpleListBoxItem>();
			InitializeComponent();
		}

		public void AddItem(ISimpleListBoxItem item)
		{
			_items.Add(item);
			Invalidate();
		}

		public void AddItems(IEnumerable<ISimpleListBoxItem> items)
		{
			_items.AddRange(items);
			Invalidate();
		}

		public void InsertItem(ISimpleListBoxItem item, int index)
		{
			_items.Insert(index, item);
			Invalidate();
		}

		public void RemoveItemAt(int index)
		{
			_items.RemoveAt(index);
			Invalidate();
		}

		public void ClearItems()
		{
			_items.Clear();
			Invalidate();
		}

		public ISimpleListBoxItem GetItemAt(int index)
		{
			return _items[index];
		}

		public int ItemCount
		{
			get { return _items.Count; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);


			//// Create rectangle for clipping region.
			//Rectangle clipRect = new Rectangle(0, 0, 100, 100);

			//// Set clipping region of graphics to rectangle.
			//e.Graphics.SetClip(clipRect);

			//e.Graphics.FillRectangle(Brushes.Red, 0, 0, 1000, 1000);

			//// Translate clipping region.
			//int dx = 50;
			//int dy = 50;
			//e.Graphics.TranslateClip(dx, dy);

			//// Fill rectangle to demonstrate translated clip region.
			//e.Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 500, 300);



			var g = e.Graphics;

			g.SetClip(new Rectangle(0, 0, 100, 100));
			e.Graphics.FillRectangle(Brushes.Red, 0, 0, 1000, 1000);
			float dy = 0f;


			g.TranslateClip(50, 50);
			//item.Draw(g);
			g.FillRectangle(Brushes.Black, 0, 0, 99, 100);

			//foreach (var item in _items)
			//{
			//	g.TranslateClip(50, 20);
			//	//item.Draw(g);
			//	g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 10, 10));
			//}
		}
	}
}
