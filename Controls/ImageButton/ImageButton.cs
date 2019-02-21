using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormControls
{
	public partial class ImageButton : UserControl
	{
		public class StateConverter : ExpandableObjectConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
					return string.Empty;
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		[TypeConverter(typeof(StateConverter))]
		public class State
		{
			private const int MaskTintAdditive = 1 << 31;
			private const int MaskResizeImage = 1 << 30;
			private const int MaskAffectBackground = 1 << 29;
			private const int MaskSizeDirty = 1 << 28;
			private const int MaskColor = 0xffffff;

			/*
			 * Store flags and color
			 *--------------------------
			 * bit    desc.
			 *--------------------------
			 * 31     Tint Addtive Flag
			 * 30     Resize Image Flag
			 * 29     Affect Background Flag
			 * 28     Size Dirty Flag
			 * 16-23  Red
			 * 8-15   Green
			 * 0-7    Blue
			 */
			private int data;

			[DefaultValue(false)]
			public bool TintAdditive
			{
				get { return (data & MaskTintAdditive) != 0; }
				set
				{
					if (value)
						data |= MaskTintAdditive;
					else
						data &= ~MaskTintAdditive;
				}
			}

			[DefaultValue(false)]
			public bool AffectBackground
			{
				get { return (data & MaskAffectBackground) != 0; }
				set
				{
					if (value)
						data |= MaskAffectBackground;
					else
						data &= ~MaskAffectBackground;
				}
			}

			[Browsable(false), DefaultValue(false)]
			public virtual bool ResizeImage
			{
				get { return (data & MaskResizeImage) != 0; }
				set
				{
					if (value)
						data |= MaskResizeImage;
					else
						data &= ~MaskResizeImage;
					SizeDirty = true;
				}
			}

			public Color Color
			{
				get { return Color.FromArgb(data | ~MaskColor); }
				set { data = (data & ~MaskColor) | (value.ToArgb() & MaskColor); }
			}
			private bool ShouldSerializeColor() { return (data & MaskColor) != 0; }

			[Browsable(false), DefaultValue(null)]
			public virtual Image Image
			{
				get { return null; }
				set { }
			}

			internal bool SizeDirty
			{
				get { return (data & MaskSizeDirty) != 0; }
				set
				{
					if (value)
						data |= MaskSizeDirty;
					else
						data &= ~MaskSizeDirty;
				}
			}

			public State()
			{
			}

			public State(State state)
			{
				data = state.data;
			}
		}

		private class StateImage : State
		{
			public Image image;
			public Rectangle area;

			[Browsable(true)]
			public override bool ResizeImage
			{
				get { return base.ResizeImage; }
				set { base.ResizeImage = value; }
			}

			[Browsable(true)]
			public override Image Image
			{
				get { return image; }
				set
				{
					image = value;
					SizeDirty = true;
				}
			}

			public StateImage()
			{
			}

			public StateImage(State state) : base(state)
			{
				if (state is StateImage)
				{
					var stateI = (StateImage)state;
					image = stateI.image;
					area = stateI.area;
				}
			}
		}

		#region Properties

		[Category("Image Button")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public State NormalState { get { return _states[0]; } }

		[Category("Image Button")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public State HoverState { get { return _states[1]; } }

		[Category("Image Button")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public State PressState { get { return _states[2]; } }

		[Category("Image Button"), DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool IndivHoverImage
		{
			get { return _states[1] is StateImage; }
			set { SetIndivState(1, value); }
		}

		[Category("Image Button"), DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool IndivPressImage
		{
			get { return _states[2] is StateImage; }
			set { SetIndivState(2, value); }
		}

		#endregion Properties

			#region Private Fields

		private static ImageAttributes _imgAttrs = new ImageAttributes();
		private static ColorMatrix _colorMx = new ColorMatrix();

		private int _state; //0: normal, 1: hover, 2: press
		private State[] _states;

		#endregion Private Fields

		#region Main

		public ImageButton()
		{
			InitializeComponent();

			//initialize color matrices
			int stateCount = StateCount;
			_states = new State[stateCount];
			_states[0] = new StateImage();
			for (int i = 1; i < stateCount; i++)
				_states[i] = new State();
		}

		/// <summary>
		/// Override this property if you need more states, default value is 3
		/// </summary>
		protected virtual int StateCount
		{
			get { return 3; }
		}

		#endregion Main

		//#region Events

		//protected override void OnPaint(PaintEventArgs e)
		//{
		//	base.OnPaint(e);

		//	if (Image != null)
		//	{
		//		e.Graphics.DrawImage(
		//			Image,
		//			_rDest,
		//			0, 0, Image.Width, Image.Height,
		//			GraphicsUnit.Pixel,
		//			_imgAttrs
		//		);
		//	}
		//}

		//protected override void OnMouseDown(MouseEventArgs e)
		//{
		//	SetColorMatrix(_cmxPress);
		//	base.OnMouseDown(e);
		//}

		//protected override void OnMouseUp(MouseEventArgs e)
		//{
		//	SetColorMatrix(_cmxHover);
		//	base.OnMouseUp(e);
		//}

		//protected override void OnMouseEnter(EventArgs e)
		//{
		//	SetColorMatrix(_cmxHover);
		//	base.OnMouseEnter(e);
		//}

		//protected override void OnMouseLeave(EventArgs e)
		//{
		//	SetColorMatrix(_cmxNormal);
		//	base.OnMouseLeave(e);
		//}

		//protected override void OnLoad(EventArgs e)
		//{
		//	base.OnLoad(e);
		//	_imgAttrs.SetColorMatrix(_cmxNormal);
		//}

		//protected override void OnResize(EventArgs e)
		//{
		//	base.OnResize(e);
		//	RefreshDrawRect();
		//}

		//protected override void OnPaddingChanged(EventArgs e)
		//{
		//	base.OnPaddingChanged(e);
		//	RefreshDrawRect();
		//}

		//#endregion Events

		#region Utilities

		protected void SetIndivState(int state, bool indiv)
		{
			var st = _states[state];

			if(indiv)
			{
				if(!(st is StateImage))
					_states[state] = new StateImage(st);
			}
			else if ((st is StateImage) && state > 0)
				_states[state] = new State(st);
		}

		//protected void SetState(int state)
		//{
		//	if (state < 0 || state >= StateCount)
		//		throw new ArgumentOutOfRangeException("state");

		//	_state = state;
		//	var cmx = _colors[state];
		//	_imgAttrs.SetColorMatrix(cmx);
		//	Invalidate();

		//}

		//private void RefreshDrawRect()
		//{
		//	if (_image == null)
		//		return;

		//	var pad = Padding;
		//	int imgW = _image.Width;
		//	int imgH = _image.Height;
		//	int cW = Width - pad.Horizontal;
		//	int cH = Height - pad.Vertical;

		//	if (_resize)
		//	{
		//		if (cW <= 0 || cH <= 0)
		//		{
		//			_rDest = new Rectangle();
		//			Invalidate();
		//			return;
		//		}

		//		double sw = (double)imgW / cW;
		//		double sh = (double)imgH / cH;
		//		if (sw < sh)
		//			sw = sh;
		//		imgW = (int)Math.Round(imgW / sw);
		//		imgH = (int)Math.Round(imgH / sw);
		//	}

		//	_rDest = new Rectangle(
		//		pad.Left + (cW - imgW) / 2,
		//		pad.Top + (cH - imgH) / 2,
		//		imgW,
		//		imgH
		//	);

		//	Invalidate();
		//}

		//protected Color MatrixToColor(ColorMatrix cmx)
		//{
		//	if (_tintAdd)
		//	{
		//		return Color.FromArgb(
		//			(int)(cmx.Matrix40 * 255),
		//			(int)(cmx.Matrix41 * 255),
		//			(int)(cmx.Matrix42 * 255)
		//		);
		//	}
		//	else
		//	{
		//		return Color.FromArgb(
		//			(int)(cmx.Matrix00 * 255),
		//			(int)(cmx.Matrix11 * 255),
		//			(int)(cmx.Matrix22 * 255)
		//		);
		//	}
		//}

		//protected void ColorToMatrix(ColorMatrix cmx, Color color)
		//{
		//	if (_tintAdd)
		//	{
		//		cmx.Matrix40 = color.R / 255f;
		//		cmx.Matrix41 = color.G / 255f;
		//		cmx.Matrix42 = color.B / 255f;
		//	}
		//	else
		//	{
		//		cmx.Matrix00 = color.R / 255f;
		//		cmx.Matrix11 = color.G / 255f;
		//		cmx.Matrix22 = color.B / 255f;
		//	}

		//	Invalidate();
		//}

		//protected void ChangeAdditive(ColorMatrix cmx)
		//{
		//	if(_tintAdd)
		//	{
		//		cmx.Matrix40 = cmx.Matrix00;
		//		cmx.Matrix41 = cmx.Matrix11;
		//		cmx.Matrix42 = cmx.Matrix22;
		//		cmx.Matrix00 = 1f;
		//		cmx.Matrix11 = 1f;
		//		cmx.Matrix22 = 1f;
		//	}
		//	else
		//	{
		//		cmx.Matrix00 = cmx.Matrix40;
		//		cmx.Matrix11 = cmx.Matrix41;
		//		cmx.Matrix12 = cmx.Matrix42;
		//		cmx.Matrix40 = 1f;
		//		cmx.Matrix41 = 1f;
		//		cmx.Matrix42 = 1f;
		//	}
		//}

		#endregion Utilities
	}
}
