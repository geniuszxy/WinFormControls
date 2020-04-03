using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WinFormControls
{
	public partial class TextboxWithLabel : UserControl
	{
		public TextboxWithLabel()
		{
			InitializeComponent();
		}

		public TextboxWithLabel(string label, string text = "")
			: this()
		{
			this.LabelText = label;
			this.TextBoxText = text;
		}

		public string LabelText
		{
			get
			{
				return label1.Text;
			}
			set
			{
				label1.Text = value;
				var textBoxX = label1.Width + 6;
				textBox1.Location = new Point(textBoxX, 0);
				this.Width = textBoxX + textBox1.Width;
			}
		}

		public int TextBoxWidth
		{
			get
			{
				return textBox1.Width;
			}
			set
			{
				textBox1.Width = value;
				this.Width = textBox1.Location.X + value;
			}
		}

		public string TextBoxText
		{
			get
			{
				return textBox1.Text;
			}
			set
			{
				textBox1.Text = value;
			}
		}

		public int TextBoxInt
		{
			get
			{
				int i;
				return int.TryParse(textBox1.Text, out i) ? i : 0;
			}
			set
			{
				textBox1.Text = value.ToString();
			}
		}
	}
}
