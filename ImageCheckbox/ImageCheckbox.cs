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
	/// <summary>
	/// A checkbox with image
	/// </summary>
	public class ImageCheckbox : ImageButton
	{
		[Category("Image Button States")]
		[DisplayName("Checked")]
		public State CheckedState
		{
			get { return GetState(3); }
			set { SetState(3, value); }
		}
		private void ResetCheckedState() { _ResetState(3); }
		private bool ShouldSerializeCheckedState() { return _ShouldSerializeState(3); }

		private bool _checked;

		private void SetChecked(bool isChecked)
		{
			_checked = isChecked;
			OnCheckedChanged(EventArgs.Empty);
		}

		public bool Checked
		{
			get { return _checked; }
			set { if (_checked != value) SetChecked(value); }
		}

		public event EventHandler CheckedChanged;

		protected virtual void OnCheckedChanged(EventArgs e)
		{
			CheckedChanged?.Invoke(this, e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			SetChecked(!_checked);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			ChangeCurrentState(_checked ? 3 : 0);
		}

		protected override int StateCount { get { return 4; } }
	}
}
