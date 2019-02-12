namespace Test
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.imageButton2 = new ImageButton.ImageButton();
			this.imageButton1 = new ImageButton.ImageButton();
			this.SuspendLayout();
			// 
			// imageButton2
			// 
			this.imageButton2.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.imageButton2.Image = global::Test.Properties.Resources._002;
			this.imageButton2.Location = new System.Drawing.Point(350, 116);
			this.imageButton2.Name = "imageButton2";
			this.imageButton2.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.imageButton2.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.imageButton2.ResizeImage = true;
			this.imageButton2.Size = new System.Drawing.Size(150, 150);
			this.imageButton2.TabIndex = 1;
			this.imageButton2.TintAdditive = false;
			// 
			// imageButton1
			// 
			this.imageButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.imageButton1.Image = global::Test.Properties.Resources._001;
			this.imageButton1.Location = new System.Drawing.Point(107, 116);
			this.imageButton1.Name = "imageButton1";
			this.imageButton1.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.imageButton1.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.imageButton1.ResizeImage = true;
			this.imageButton1.Size = new System.Drawing.Size(150, 150);
			this.imageButton1.TabIndex = 0;
			this.imageButton1.TintAdditive = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.imageButton2);
			this.Controls.Add(this.imageButton1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private ImageButton.ImageButton imageButton2;
		private ImageButton.ImageButton imageButton1;
	}
}

