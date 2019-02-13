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
			set
			{
				_image = value;
				RefreshDrawRect();
			}
		}
		private Image _image;

		public bool ResizeImage
		{
			get { return _resize; }
			set
			{
				_resize = value;
				RefreshDrawRect();
			}
		}
		private bool _resize = true;

		public Color NormalColor
		{
			get { return MatrixToColor(_cmxNormal); }
			set { ColorToMatrix(_cmxNormal, value); }
		}
		private ColorMatrix _cmxNormal = new ColorMatrix();

		public Color HoverColor
		{
			get { return MatrixToColor(_cmxHover); }
			set { ColorToMatrix(_cmxHover, value); }
		}
		private ColorMatrix _cmxHover = new ColorMatrix();

		public Color PressColor
		{
			get { return MatrixToColor(_cmxPress); }
			set { ColorToMatrix(_cmxPress, value); }
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
				ChangeAdditive(_cmxNormal);
				ChangeAdditive(_cmxHover);
				ChangeAdditive(_cmxPress);
				Invalidate();
			}
		}
		private bool _tintAdd = false;

		private ImageAttributes _imgAttrs = new ImageAttributes();
		private Rectangle _rDest;

		public ImageButton()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (Image != null)
			{
				e.Graphics.DrawImage(
					Image,
					_rDest,
					0, 0, Image.Width, Image.Height,
					GraphicsUnit.Pixel,
					_imgAttrs
				);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			SetColorMatrix(_cmxPress);
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			SetColorMatrix(_cmxHover);
			base.OnMouseUp(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			SetColorMatrix(_cmxHover);
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			SetColorMatrix(_cmxNormal);
			base.OnMouseLeave(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			_imgAttrs.SetColorMatrix(_cmxNormal);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			RefreshDrawRect();
		}

		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			RefreshDrawRect();
		}

		protected void SetColorMatrix(ColorMatrix cmx)
		{
			_imgAttrs.SetColorMatrix(cmx);
			Invalidate();
		}

		private void RefreshDrawRect()
		{
			if (_image == null)
				return;

			var pad = Padding;
			int imgW = _image.Width;
			int imgH = _image.Height;
			int cW = Width - pad.Horizontal;
			int cH = Height - pad.Vertical;

			if (_resize)
			{
				if (cW <= 0 || cH <= 0)
				{
					_rDest = new Rectangle();
					Invalidate();
					return;
				}

				double sw = (double)imgW / cW;
				double sh = (double)imgH / cH;
				if (sw < sh)
					sw = sh;
				imgW = (int)Math.Round(imgW / sw);
				imgH = (int)Math.Round(imgH / sw);
			}

			_rDest = new Rectangle(
				pad.Left + (cW - imgW) / 2,
				pad.Top + (cH - imgH) / 2,
				imgW,
				imgH
			);

			Invalidate();
		}

		protected Color MatrixToColor(ColorMatrix cmx)
		{
			if (_tintAdd)
			{
				return Color.FromArgb(
					(int)(cmx.Matrix40 * 255),
					(int)(cmx.Matrix41 * 255),
					(int)(cmx.Matrix42 * 255)
				);
			}
			else
			{
				return Color.FromArgb(
					(int)(cmx.Matrix00 * 255),
					(int)(cmx.Matrix11 * 255),
					(int)(cmx.Matrix22 * 255)
				);
			}
		}

		protected void ColorToMatrix(ColorMatrix cmx, Color color)
		{
			if (_tintAdd)
			{
				cmx.Matrix40 = color.R / 255f;
				cmx.Matrix41 = color.G / 255f;
				cmx.Matrix42 = color.B / 255f;
			}
			else
			{
				cmx.Matrix00 = color.R / 255f;
				cmx.Matrix11 = color.G / 255f;
				cmx.Matrix22 = color.B / 255f;
			}

			Invalidate();
		}

		protected void ChangeAdditive(ColorMatrix cmx)
		{
			if(_tintAdd)
			{
				cmx.Matrix40 = cmx.Matrix00;
				cmx.Matrix41 = cmx.Matrix11;
				cmx.Matrix42 = cmx.Matrix22;
				cmx.Matrix00 = 1f;
				cmx.Matrix11 = 1f;
				cmx.Matrix22 = 1f;
			}
			else
			{
				cmx.Matrix00 = cmx.Matrix40;
				cmx.Matrix11 = cmx.Matrix41;
				cmx.Matrix12 = cmx.Matrix42;
				cmx.Matrix40 = 1f;
				cmx.Matrix41 = 1f;
				cmx.Matrix42 = 1f;
			}
		}
	}
}
