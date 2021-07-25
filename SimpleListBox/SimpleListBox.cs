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
			_selectIndex = -1;
			_itemHeight = (int)Font.GetHeight() + 2;

			InitializeComponent();
		}

		/// <summary>
		/// Add an item
		/// </summary>
		public void AddItem(object item)
		{
			_items.Add(item);
			CheckScrollbar();
		}

		/// <summary>
		/// Add many items
		/// </summary>
		public void AddItems(ICollection items)
		{
			_items.AddRange(items);
			CheckScrollbar();
		}

		public void InsertItem(object item, int index)
		{
			_items.Insert(index, item);
			CheckScrollbar();
		}

		public void RemoveItemAt(int index)
		{
			_items.RemoveAt(index);
			CheckScrollbar();
		}

		/// <summary>
		/// Remove all items
		/// </summary>
		public void ClearItems()
		{
			_items.Clear();
			_topIndex = 0;
			_selectIndex = -1;
			if (VScroll)
				VerticalScroll.Visible = false;
			Invalidate();
		}

		public object GetItemAt(int index)
		{
			return _items[index];
		}

		/// <summary>
		/// Get item count
		/// </summary>
		public int ItemCount
		{
			get { return _items.Count; }
		}

		/// <summary>
		/// 修改selectIndex
		/// </summary>
		private void Select(int index)
		{
			if (_selectIndex == index)
				return;

			_selectIndex = index;
			Invalidate();
		}

		/// <summary>
		/// 检查是否要显示滚动条
		/// </summary>
		private void CheckScrollbar()
		{
			int count = _items.Count;



			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int itemCount = _items.Count;
			if (itemCount == 0)
				return;

			var g = e.Graphics;
			var size = ClientSize;
			var padding = Padding;
			var font = Font;
			float itemHeight = _itemHeight;
			int indexMax = (int)Math.Floor(size.Height / itemHeight) + _topIndex;
			if (indexMax > itemCount)
				indexMax = itemCount;
			var foreBrush = new SolidBrush(ForeColor);
			var selectedBrush = SystemBrushes.HighlightText;
			var selectedBackBrush = SystemBrushes.Highlight;

			g.TranslateTransform(rect.X, rect.Y);
			//g.SetClip(new Rectangle(0, 0, rect.Width, _itemHeight));

			//for (int i = _topIndex; i < indexMax; i++)
			//{
			//	var item = _items[i];
			//	if(_selectIndex == i)
			//	{
			//		g.FillRectangle(selectedBackBrush, 0, 0, rect.Width, _itemHeight);
			//		g.DrawString(item.ToString(), font, selectedBrush, 0, 0);
			//	}
			//	else
			//		g.DrawString(item.ToString(), font, foreBrush, 0, 0);

			//	g.TranslateTransform(0, itemHeight);
			//	g.TranslateClip(0, _itemHeight);
			//}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button != MouseButtons.Left)
				return;

			int itemCount = _items.Count;
			if (itemCount == 0)
				return;

			var loc = e.Location;
			var rDisplay = DisplayRectangle;
			if (!rDisplay.Contains(loc))
				return;

			//select
			int select = (loc.Y - rDisplay.Top) / _itemHeight;
			select += _topIndex;
			if (select < itemCount)
				Select(select);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			int itemCount = _items.Count;
			if (itemCount > 0)
			{
				switch (keyData)
				{
					case Keys.Up:
						if (_selectIndex > 0)
						{
							Select(_selectIndex - 1);
							return true;
						}
						break;

					case Keys.Down:
						if (_selectIndex < itemCount - 1)
						{
							Select(_selectIndex + 1);
							return true;
						}
						break;
				}
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
