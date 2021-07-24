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
		private ArrayList _items;
		private int _selectIndex;
		private int _topIndex;
		private int _itemHeight;

		/// <summary>
		/// Height of each item
		/// </summary>
		public int ItemHeight
		{
			get { return _itemHeight; }
			set { _itemHeight = value; Invalidate(); }
		}

		public SimpleListBox()
		{
			_items = new ArrayList();
		
			InitializeComponent();

			_itemHeight = (int)Font.GetHeight() + 2;
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

			int itemCount = _items.Count;
			if (itemCount == 0)
				return;

			var g = e.Graphics;
			var rect = DisplayRectangle;
			var font = Font;
			float itemHeight = _itemHeight;
			int indexMax = (int)Math.Floor(ClientSize.Height / itemHeight) + _topIndex;
			if (indexMax > itemCount)
				indexMax = itemCount;
			var foreBrush = new SolidBrush(ForeColor);
			var selectedBrush = SystemBrushes.HighlightText;
			var selectedBackBrush = SystemBrushes.Highlight;

			g.TranslateTransform(rect.X, rect.Y);
			g.SetClip(new Rectangle(0, 0, rect.Width, _itemHeight));

			for (int i = _topIndex; i < indexMax; i++)
			{
				var item = _items[i];
				if(_selectIndex == i)
				{
					g.FillRectangle(selectedBackBrush, 0, 0, rect.Width, _itemHeight);
					g.DrawString(item.ToString(), font, selectedBrush, 0, 0);
				}
				else
					g.DrawString(item.ToString(), font, foreBrush, 0, 0);

				g.TranslateTransform(0, itemHeight);
				g.TranslateClip(0, _itemHeight);
			}
		}
	}
}
