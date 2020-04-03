using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WinFormControls
{
	public class AutoScrollTextBox : TextBox
	{
		#region Auto Scroll Bar

		private bool _checkingScrollBars;

		private void CheckScrollBars()
		{
			if (_checkingScrollBars)
				return;

			if (!IsHandleCreated)
				return;

			// Single-line
			if (!Multiline)
				return;

			try
			{
				_checkingScrollBars = true;

				// Too few text
				var textLength = TextLength;
				if (textLength <= 1)
				{
					ScrollBars = ScrollBars.None;
					return;
				}

				var pos0 = GetPositionFromCharIndex(0);
				var pos1 = GetPositionFromCharIndex(textLength - 1);
				ScrollBars = pos1.Y - pos0.Y > ClientSize.Height ? ScrollBars.Vertical : ScrollBars.None;
			}
			finally
			{
				_checkingScrollBars = false;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			CheckScrollBars();
			base.OnHandleCreated(e);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			CheckScrollBars();
			base.OnTextChanged(e);
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			CheckScrollBars();
			base.OnClientSizeChanged(e);
		}

		#endregion Auto Scroll Bar

		#region Drag and Drop

		private int _savedSelectionStart, _savedSelectionLength;

		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			_savedSelectionStart = SelectionStart;
			_savedSelectionLength = SelectionLength;
			SelectionLength = 0;
			base.OnDragEnter(drgevent);
		}

		protected override void OnDragOver(DragEventArgs drgevent)
		{
			if((drgevent.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
				drgevent.Effect = DragDropEffects.Copy;
			else if((drgevent.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
				drgevent.Effect = DragDropEffects.Move;
			else
				drgevent.Effect = DragDropEffects.None;

			if(drgevent.Effect != DragDropEffects.None)
				SelectionStart = GetDragCharPosition(drgevent);

			base.OnDragOver(drgevent);
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			var data = drgevent.Data;
			string text = null;

			// Get text
			if (data.GetDataPresent(DataFormats.Text))
			{
				text = (string)data.GetData(DataFormats.Text);
			}
			// Get file contents
			else if (data.GetDataPresent(DataFormats.FileDrop))
			{
				var sb = new StringBuilder();
				var filenames = (string[])data.GetData(DataFormats.FileDrop);
				foreach (var filename in filenames)
				{
					try { sb.Append(File.ReadAllText(filename)); }
					catch { }
				}
				text = sb.ToString();
			}

			// Insert text
			if (!string.IsNullOrEmpty(text))
				Text = Text.Insert(GetDragCharPosition(drgevent), text);

			base.OnDragDrop(drgevent);
		}

		protected override void OnDragLeave(EventArgs e)
		{
			SelectionStart = _savedSelectionStart;
			SelectionLength = _savedSelectionLength;
			base.OnDragLeave(e);
		}

		int GetDragCharPosition(DragEventArgs drgevent)
		{
			var dragPosition = PointToClient(new Point(drgevent.X, drgevent.Y));
			return GetCharIndexFromPosition(dragPosition);
		}

		#endregion Drag and Drop

		#region Drag Text

		private bool _dragText;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(SelectionLength > 0)
			{
				var pos = GetCharIndexFromPosition(PointToClient(e.Location)) - SelectionStart;
				if (pos >= 0 && pos < SelectionLength)
					_dragText = true;
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(_dragText)
			{
				DoDragDrop(SelectedText, DragDropEffects.Move | DragDropEffects.Copy);
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			_dragText = false;
			base.OnMouseUp(mevent);
		}

		#endregion Drag Text

		#region Select All

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			// Ctrl + A is disabled when multiline is enabled
			if (ShortcutsEnabled && Multiline && e.KeyData == (Keys.Control | Keys.A))
			{
				SelectAll();
				e.SuppressKeyPress = true;
			}
		}

		#endregion Select All

		#region Common

		public AutoScrollTextBox()
		{
			Multiline = true;
			AcceptsReturn = true;
			AcceptsTab = true;
			AllowDrop = true;
		}

		#endregion Common
	}
}
