﻿namespace Test
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
			this.SuspendLayout();
			// 
			// simpleListBox1
			// 
			this.simpleListBox1.AutoScroll = true;
			this.simpleListBox1.ItemHeight = 11;
			this.simpleListBox1.Location = new System.Drawing.Point(0, 0);
			this.simpleListBox1.Name = "simpleListBox1";
			this.simpleListBox1.Padding = new System.Windows.Forms.Padding(10);
			this.simpleListBox1.Size = new System.Drawing.Size(245, 190);
			this.simpleListBox1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(865, 591);
			this.Controls.Add(this.simpleListBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private WinFormControls.SimpleListBox simpleListBox1;
	}
}

