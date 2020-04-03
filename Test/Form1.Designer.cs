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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
<<<<<<< HEAD
			this.button1 = new System.Windows.Forms.Button();
			this.autoScrollTextBox1 = new WinFormControls.AutoScrollTextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// autoScrollTextBox1
			// 
			this.autoScrollTextBox1.AcceptsReturn = true;
			this.autoScrollTextBox1.AcceptsTab = true;
			this.autoScrollTextBox1.AllowDrop = true;
			this.autoScrollTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.autoScrollTextBox1.Location = new System.Drawing.Point(0, 0);
			this.autoScrollTextBox1.Multiline = true;
			this.autoScrollTextBox1.Name = "autoScrollTextBox1";
			this.autoScrollTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.autoScrollTextBox1.Size = new System.Drawing.Size(265, 163);
			this.autoScrollTextBox1.TabIndex = 0;
			this.autoScrollTextBox1.Text = resources.GetString("autoScrollTextBox1.Text");
=======
			this.imageButton1 = new WinFormControls.ImageButton();
			this.imageCheckbox1 = new WinFormControls.ImageCheckbox();
			this.SuspendLayout();
			// 
			// imageButton1
			// 
			this.imageButton1.HoverState = new WinFormControls.ImageButton.State(268435456, null);
			this.imageButton1.Location = new System.Drawing.Point(73, 12);
			this.imageButton1.Name = "imageButton1";
			this.imageButton1.NormalState = new WinFormControls.ImageButton.State(-1594880016, global::Test.Properties.Resources._001);
			this.imageButton1.PressState = new WinFormControls.ImageButton.State(-1879047952, null);
			this.imageButton1.Size = new System.Drawing.Size(48, 50);
			this.imageButton1.TabIndex = 0;
			// 
			// imageCheckbox1
			// 
			this.imageCheckbox1.Checked = false;
			this.imageCheckbox1.CheckedState = new WinFormControls.ImageButton.State(268438732, null);
			this.imageCheckbox1.HoverState = new WinFormControls.ImageButton.State(268439296, null);
			this.imageCheckbox1.Location = new System.Drawing.Point(354, 156);
			this.imageCheckbox1.Name = "imageCheckbox1";
			this.imageCheckbox1.NormalState = new WinFormControls.ImageButton.State(0, ((System.Drawing.Image)(resources.GetObject("imageCheckbox1.NormalState"))));
			this.imageCheckbox1.Size = new System.Drawing.Size(150, 150);
			this.imageCheckbox1.TabIndex = 1;
>>>>>>> 04dca1956c81afb3f69fa8a31e7650d93d3eafe6
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
			this.ClientSize = new System.Drawing.Size(265, 163);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.autoScrollTextBox1);
=======
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.imageCheckbox1);
			this.Controls.Add(this.imageButton1);
>>>>>>> 04dca1956c81afb3f69fa8a31e7650d93d3eafe6
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

<<<<<<< HEAD
		private WinFormControls.AutoScrollTextBox autoScrollTextBox1;
		private System.Windows.Forms.Button button1;
=======
		private WinFormControls.ImageButton imageButton1;
		private WinFormControls.ImageCheckbox imageCheckbox1;
>>>>>>> 04dca1956c81afb3f69fa8a31e7650d93d3eafe6
	}
}

