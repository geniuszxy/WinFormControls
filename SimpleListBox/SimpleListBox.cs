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
		/// <summary>
		/// Height of each item
		/// </summary>
		public int ItemHeight { get; set; }

		private ArrayList _items;
		private int _selectIndex;
		private int _topIndex;

		public SimpleListBox()
		{
			_items = new ArrayList();
			InitializeComponent();
		}

		/// <summary>
		/// Add an item
		/// </summary>
		public void AddItem(object item)
		{
			_items.Add(item);
			Invalidate();
		}

		/// <summary>
		/// Add many items
		/// </summary>
		public void AddItems(ICollection items)
		{
			_items.AddRange(items);
			Invalidate();
		}

		public void InsertItem(object item, int index)
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

		public object GetItemAt(int index)
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

			var g = e.Graphics;
			var size = ClientSize;

			//g.SetClip(new Rectangle(0, 0, 100, 100));
			e.Graphics.FillRectangle(Brushes.Red, 0, 0, 1000, 1000);
			float dy = 0f;


			foreach (var item in _items)
			{
				g.FillRectangle(Brushes.Blue, new Rectangle(0, 0, 100, 150));
				var text = item.ToString();
				g.TranslateTransform(0, 20);
				g.TranslateClip(0, 20);
			}
		}
	}
}
