namespace TrainProjectAnalyse
{
    partial class Main
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.check_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-5, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1200, 653);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(415, 728);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(171, 65);
            this.start_btn.TabIndex = 1;
            this.start_btn.Text = "识别";
            this.start_btn.UseVisualStyleBackColor = true;
            // 
            // check_btn
            // 
            this.check_btn.Location = new System.Drawing.Point(613, 728);
            this.check_btn.Name = "check_btn";
            this.check_btn.Size = new System.Drawing.Size(171, 65);
            this.check_btn.TabIndex = 2;
            this.check_btn.Text = "检视";
            this.check_btn.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 810);
            this.Controls.Add(this.check_btn);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Main";
            this.Text = "计划识别系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Button check_btn;
    }
}

