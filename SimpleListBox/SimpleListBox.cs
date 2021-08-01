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
		private int _itemHeight;

		/// <summary>
		/// Scroll position changed event
		/// </summary>
		public event EventHandler ScrollPositionChanged;

		/// <summary>
		/// Height of each item
		/// </summary>
		public int ItemHeight
		{
			get { return _itemHeight; }
			set { _itemHeight = value; Invalidate(); }
		}

		/// <summary>
		/// Scroll position of content
		/// </summary>
		public Point ScrollPosition
		{
			get { return base.AutoScrollPosition; }
			set
			{
				base.AutoScrollPosition = value;
				OnScrollPositionChanged();
			}
		}

		#region Hidden

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get { return true; }
			set { }
		}

		private new Point AutoScrollPosition
		{
			get { return Point.Empty; }
			set { }
		}

		#endregion

		public SimpleListBox()
		{
			_items = new ArrayList();
			_selectIndex = -1;
			_itemHeight = (int)Font.GetHeight() + 2;
			base.AutoScroll = true;
			DoubleBuffered = true;

			InitializeComponent();
		}

		/// <summary>
		/// Add an item
		/// </summary>
		public void AddItem(object item)
		{
			_items.Add(item);
			CheckAutoSize();
		}

		/// <summary>
		/// Add many items
		/// </summary>
		public void AddItems(ICollection items)
		{
			_items.AddRange(items);
			CheckAutoSize();
		}

		public void InsertItem(object item, int index)
		{
			_items.Insert(index, item);
			CheckAutoSize();
		}

		public void RemoveItemAt(int index)
		{
			_items.RemoveAt(index);
			CheckAutoSize();
		}

		/// <summary>
		/// Remove all items
		/// </summary>
		public void ClearItems()
		{
			_items.Clear();
			_selectIndex = -1;
			AutoScrollMinSize = Size.Empty;
			base.AutoScrollPosition = Point.Empty;
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
		private void CheckAutoSize()
		{
			int count = _items.Count;
			int height = _itemHeight * count + Padding.Vertical;
			AutoScrollMinSize = new Size(1, height);
			Invalidate();
		}

		private void OnScrollPositionChanged()
		{
			ScrollPositionChanged?.Invoke(this, EventArgs.Empty);
			Invalidate();
		}

		#region Events

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int itemCount = _items.Count;
			if (itemCount == 0)
				return;

			var g = e.Graphics;
			var padding = Padding;
			var size = ClientSize;
			var y = -base.AutoScrollPosition.Y;

			size.Width -= padding.Horizontal;
			size.Height -= padding.Vertical;

			var startIndex = y / _itemHeight;
			var endIndex = (y + size.Height) / _itemHeight;
			if (endIndex >= itemCount)
				endIndex = itemCount - 1;
			if (startIndex > endIndex)
				startIndex = endIndex;

			var font = Font;
			var foreBrush = new SolidBrush(ForeColor);
			var selectedBrush = SystemBrushes.HighlightText;
			var selectedBackBrush = SystemBrushes.Highlight;

			g.SetClip(new Rectangle(padding.Left, padding.Top, size.Width, size.Height));
			g.TranslateTransform(padding.Left, padding.Top - y + startIndex * _itemHeight);
			//g.SetClip(new Rectangle(0, 0, size.Width, _itemHeight));

			for (int i = startIndex; i <= endIndex; i++)
			{
				var item = _items[i];
				if (_selectIndex == i)
				{
					g.FillRectangle(selectedBackBrush, 0, 0, size.Width, _itemHeight);
					g.DrawString(item.ToString(), font, selectedBrush, 0, 0);
				}
				else
					g.DrawString(item.ToString(), font, foreBrush, 0, 0);

				g.TranslateTransform(0, _itemHeight);
				//g.TranslateClip(0, _itemHeight);
			}
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

		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			OnScrollPositionChanged();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			OnScrollPositionChanged();
		}

		#endregion
	}
}
