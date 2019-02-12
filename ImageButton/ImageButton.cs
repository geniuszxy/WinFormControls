using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageButton
{
	public partial class ImageButton : UserControl
	{
		public Image Image
		{
			get { return _image; }
			set { _image = value; }
		}
		private Image _image;

		public bool ResizeImage
		{
			get { return _resize; }
			set { _resize = value; }
		}
		private bool _resize = true;

		public Color NormalColor
		{
			get { return MatrixToColor(_cmxNormal, _tintAdd); }
			set { ColorToMatrix(_cmxNormal, value, _tintAdd); }
		}
		private ColorMatrix _cmxNormal = new ColorMatrix();

		public Color HoverColor
		{
			get { return MatrixToColor(_cmxHover, _tintAdd); }
			set { ColorToMatrix(_cmxHover, value, _tintAdd); }
		}
		private ColorMatrix _cmxHover = new ColorMatrix();

		public Color PressColor
		{
			get { return MatrixToColor(_cmxPress, _tintAdd); }
			set { ColorToMatrix(_cmxPress, value, _tintAdd); }
		}
		private ColorMatrix _cmxPress = new ColorMatrix();

		public virtual bool TintAdditive
		{
			get { return _tintAdd; }
			set
			{
				if (_tintAdd == value)
					return;

				_tintAdd = value;
				ChangeAdditive(_cmxNormal, value);
				ChangeAdditive(_cmxHover, value);
				ChangeAdditive(_cmxPress, value);
			}
		}
		private bool _tintAdd = false;

		private ImageAttributes imageAttributes = new ImageAttributes();

		public ImageButton()
		{
			ColorToMatrix(_cmxNormal, Color.Black, _tintAdd);
			ColorToMatrix(_cmxHover, Color.White, _tintAdd);
			ColorToMatrix(_cmxPress, Color.Gray, _tintAdd);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (Image != null)
			{
				imageAttributes.SetColorMatrix(_cmxNormal);

				e.Graphics.DrawImage(
					Image,
					new Rectangle(0, 0, Width, Height),
					0, 0, Image.Width, Image.Height,
					GraphicsUnit.Pixel,
					imageAttributes
				);
			}
		}

		protected static Color MatrixToColor(ColorMatrix cmx, bool additive)
		{
			if (additive)
			{
				return Color.FromArgb(
					(byte)(cmx.Matrix43 * 255),
					(byte)(cmx.Matrix40 * 255),
					(byte)(cmx.Matrix41 * 255),
					(byte)(cmx.Matrix42 * 255)
				);
			}
			else
			{
				return Color.FromArgb(
					(byte)(cmx.Matrix33 * 255),
					(byte)(cmx.Matrix00 * 255),
					(byte)(cmx.Matrix11 * 255),
					(byte)(cmx.Matrix22 * 255)
				);
			}
		}

		protected static void ColorToMatrix(ColorMatrix cmx, Color color, bool additive)
		{
			if (additive)
			{
				cmx.Matrix43 = color.A / 255f;
				cmx.Matrix40 = color.R / 255f;
				cmx.Matrix41 = color.G / 255f;
				cmx.Matrix42 = color.B / 255f;
			}
			else
			{
				cmx.Matrix33 = color.A / 255f;
				cmx.Matrix00 = color.R / 255f;
				cmx.Matrix11 = color.G / 255f;
				cmx.Matrix22 = color.B / 255f;
			}
		}

		protected static void ChangeAdditive(ColorMatrix cmx, bool additive)
		{
			if(additive)
			{
				cmx.Matrix43 = cmx.Matrix33;
				cmx.Matrix40 = cmx.Matrix00;
				cmx.Matrix41 = cmx.Matrix11;
				cmx.Matrix42 = cmx.Matrix22;
				cmx.Matrix33 = 1f;
				cmx.Matrix00 = 1f;
				cmx.Matrix11 = 1f;
				cmx.Matrix22 = 1f;
			}
			else
			{
				cmx.Matrix33 = cmx.Matrix43;
				cmx.Matrix00 = cmx.Matrix40;
				cmx.Matrix11 = cmx.Matrix41;
				cmx.Matrix12 = cmx.Matrix42;
				cmx.Matrix43 = 1f;
				cmx.Matrix40 = 1f;
				cmx.Matrix41 = 1f;
				cmx.Matrix42 = 1f;
			}
		}
	}
}
