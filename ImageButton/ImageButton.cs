using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageButton
{
	public partial class ImageButton : UserControl
	{
		public Image Image { get; set; }

		public bool ResizeImage { get; set; } = true;

		public Color NormalColor
		{
			get { return MatrixToColor(0); }
			set { ColorToMatrix(0, value); }
		}

		public Color HoverColor
		{
			get { return MatrixToColor(1); }
			set { ColorToMatrix(1, value); }
		}

		public Color PressColor
		{
			get { return MatrixToColor(2); }
			set { ColorToMatrix(2, value); }
		}

		public bool TintAdditive
		{
			get { return tintAdditive; }
			set
			{
				tintAdditive = value;
				Array.Clear(colorMatrices, 0, colorMatrices.Length);
			}
		}

		private bool tintAdditive;
		private ImageAttributes imageAttributes;
		private ColorMatrix[] colorMatrices;

		public ImageButton()
		{
			colorMatrices = new ColorMatrix[ColorCount];
			imageAttributes = new ImageAttributes();
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (Image != null)
			{
				var cm = colorMatrices[0];
				if(cm == null)
					cm = colorMatrices[0] = new ColorMatrix();

				imageAttributes.SetColorMatrix(cm);

				e.Graphics.DrawImage(
					Image,
					new Rectangle(0, 0, Width, Height),
					0, 0, Image.Width, Image.Height,
					GraphicsUnit.Pixel,
					imageAttributes
				);
			}
		}

		protected Color MatrixToColor(int index)
		{
			var cm = colorMatrices[index];
			if (cm == null)
				return Color.Black;

			if(tintAdditive)
			{

			}

			return Color.FromArgb(
				(byte)(cm.Matrix33 * 255),
				(byte)(cm.Matrix00 * 255),
				(byte)(cm.Matrix11 * 255),
				(byte)(cm.Matrix22 * 255)
			);
		}

		protected void ColorToMatrix(int index, Color color)
		{
			var cm = colorMatrices[index];
			if (cm == null)
				cm = colorMatrices[index] = new ColorMatrix();

			if (tintAdditive)
			{

			}

			cm.Matrix33 = color.A / 255f;
			cm.Matrix00 = color.R / 255f;
			cm.Matrix11 = color.G / 255f;
			cm.Matrix22 = color.B / 255f;
		}

		protected virtual int ColorCount { get { return 3; } }
	}
}
