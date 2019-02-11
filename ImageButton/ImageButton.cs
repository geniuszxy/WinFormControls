using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageButton
{
	public partial class ImageButton : UserControl
	{
		public Image Image { get; set; }

		public Color NormalColor { get; set; } = Color.Black;

		public Color HoverColor { get; set; } = Color.LightGray;

		public Color PressColor { get; set; } = Color.Gray;

		public bool ResizeImage { get; set; } = true;

		public bool TintAdditive { get; set; } = false;

		public ImageButton()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if(Image != null)
				e.Graphics.DrawImage(Image, new Point(0, 0));
		}
	}
}
