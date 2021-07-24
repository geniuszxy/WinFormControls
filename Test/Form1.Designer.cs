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
			this.simpleListBox1 = new WinFormControls.SimpleListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// simpleListBox1
			// 
			this.simpleListBox1.AutoScroll = true;
			this.simpleListBox1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.simpleListBox1.ItemHeight = 15;
			this.simpleListBox1.Location = new System.Drawing.Point(193, 104);
			this.simpleListBox1.Name = "simpleListBox1";
			this.simpleListBox1.Padding = new System.Windows.Forms.Padding(10);
			this.simpleListBox1.Size = new System.Drawing.Size(245, 190);
			this.simpleListBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(444, 104);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 190);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(112, 104);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 190);
			this.button2.TabIndex = 1;
			this.button2.Text = "button1";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(193, 300);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(245, 59);
			this.button3.TabIndex = 1;
			this.button3.Text = "button1";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(193, 39);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(245, 59);
			this.button4.TabIndex = 1;
			this.button4.Text = "button1";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(865, 591);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.simpleListBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private WinFormControls.SimpleListBox simpleListBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}

