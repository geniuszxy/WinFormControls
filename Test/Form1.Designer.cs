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
			this.imageButton1 = new WinFormControls.ImageButton();
			this.SuspendLayout();
			// 
			// imageButton1
			// 
			this.imageButton1.HoverState = new WinFormControls.ImageButton.State(-1326448385, null);
			this.imageButton1.Location = new System.Drawing.Point(210, 92);
			this.imageButton1.Name = "imageButton1";
			this.imageButton1.NormalState = new WinFormControls.ImageButton.State(-1609625616, global::Test.Properties.Resources._001);
			this.imageButton1.PressState = new WinFormControls.ImageButton.State(268435456, null);
			this.imageButton1.Size = new System.Drawing.Size(64, 64);
			this.imageButton1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.imageButton1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private WinFormControls.ImageButton imageButton1;
	}
}

