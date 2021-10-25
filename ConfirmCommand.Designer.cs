namespace TrainProjectAnalyse
{
    partial class ConfirmCommand
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmCommand));
            this.mainList = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.save_btn = new CCWin.SkinControl.SkinButton();
            this.timeTableDate_dtp = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.search_tb = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OriginalText_rtb = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.outputTB = new System.Windows.Forms.RichTextBox();
            this.searchHighSpeedCommand_tb = new System.Windows.Forms.TextBox();
            this.command_rTb = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox0 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainList
            // 
            this.mainList.HideSelection = false;
            this.mainList.Location = new System.Drawing.Point(6, 42);
            this.mainList.Name = "mainList";
            this.mainList.Size = new System.Drawing.Size(598, 525);
            this.mainList.TabIndex = 0;
            this.mainList.UseCompatibleStateImageBehavior = false;
            this.mainList.SelectedIndexChanged += new System.EventHandler(this.mainList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.save_btn);
            this.groupBox1.Controls.Add(this.timeTableDate_dtp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.search_tb);
            this.groupBox1.Controls.Add(this.mainList);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox1.Location = new System.Drawing.Point(367, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 573);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时刻表中的列车";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // save_btn
            // 
            this.save_btn.BackColor = System.Drawing.Color.Transparent;
            this.save_btn.BaseColor = System.Drawing.Color.OrangeRed;
            this.save_btn.BorderColor = System.Drawing.Color.OrangeRed;
            this.save_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.save_btn.DownBack = null;
            this.save_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.save_btn.ForeColor = System.Drawing.Color.White;
            this.save_btn.Location = new System.Drawing.Point(488, 13);
            this.save_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.save_btn.MouseBack = null;
            this.save_btn.Name = "save_btn";
            this.save_btn.NormlBack = null;
            this.save_btn.Size = new System.Drawing.Size(116, 25);
            this.save_btn.TabIndex = 42;
            this.save_btn.Text = "存储为表格";
            this.save_btn.UseVisualStyleBackColor = false;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // timeTableDate_dtp
            // 
            this.timeTableDate_dtp.Location = new System.Drawing.Point(111, 15);
            this.timeTableDate_dtp.Name = "timeTableDate_dtp";
            this.timeTableDate_dtp.Size = new System.Drawing.Size(119, 23);
            this.timeTableDate_dtp.TabIndex = 5;
            this.timeTableDate_dtp.ValueChanged += new System.EventHandler(this.timeTableDate_dtp_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label2.Location = new System.Drawing.Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "制作的时刻表日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "查找车次";
            // 
            // search_tb
            // 
            this.search_tb.Location = new System.Drawing.Point(392, 15);
            this.search_tb.Name = "search_tb";
            this.search_tb.Size = new System.Drawing.Size(86, 23);
            this.search_tb.TabIndex = 1;
            this.search_tb.TextChanged += new System.EventHandler(this.search_tb_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OriginalText_rtb);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox2.Location = new System.Drawing.Point(983, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(283, 573);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查看原文";
            // 
            // OriginalText_rtb
            // 
            this.OriginalText_rtb.Location = new System.Drawing.Point(6, 20);
            this.OriginalText_rtb.Name = "OriginalText_rtb";
            this.OriginalText_rtb.ReadOnly = true;
            this.OriginalText_rtb.Size = new System.Drawing.Size(271, 547);
            this.OriginalText_rtb.TabIndex = 0;
            this.OriginalText_rtb.Text = "";
            this.OriginalText_rtb.TextChanged += new System.EventHandler(this.OriginalText_rtb_TextChanged);
            this.OriginalText_rtb.DoubleClick += new System.EventHandler(this.OriginalText_rtb_DoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.outputTB);
            this.groupBox4.Controls.Add(this.searchHighSpeedCommand_tb);
            this.groupBox4.Controls.Add(this.command_rTb);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox4.Location = new System.Drawing.Point(17, 74);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(344, 573);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "↓高铁客调命令粘贴处";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 385);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "查找车次";
            // 
            // outputTB
            // 
            this.outputTB.Location = new System.Drawing.Point(6, 414);
            this.outputTB.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.outputTB.Name = "outputTB";
            this.outputTB.ReadOnly = true;
            this.outputTB.Size = new System.Drawing.Size(332, 153);
            this.outputTB.TabIndex = 12;
            this.outputTB.Text = "";
            // 
            // searchHighSpeedCommand_tb
            // 
            this.searchHighSpeedCommand_tb.Location = new System.Drawing.Point(151, 382);
            this.searchHighSpeedCommand_tb.Name = "searchHighSpeedCommand_tb";
            this.searchHighSpeedCommand_tb.Size = new System.Drawing.Size(101, 23);
            this.searchHighSpeedCommand_tb.TabIndex = 6;
            this.searchHighSpeedCommand_tb.TextChanged += new System.EventHandler(this.searchHighSpeedCommand_tb_TextChanged);
            // 
            // command_rTb
            // 
            this.command_rTb.ContextMenuStrip = this.contextMenuStrip1;
            this.command_rTb.Location = new System.Drawing.Point(6, 22);
            this.command_rTb.Name = "command_rTb";
            this.command_rTb.Size = new System.Drawing.Size(332, 354);
            this.command_rTb.TabIndex = 0;
            this.command_rTb.Text = "";
            this.command_rTb.TextChanged += new System.EventHandler(this.highSpeedCommand_rtb_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制toolStripMenuItem1,
            this.粘贴ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // 复制toolStripMenuItem1
            // 
            this.复制toolStripMenuItem1.Name = "复制toolStripMenuItem1";
            this.复制toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.复制toolStripMenuItem1.Text = "复制";
            this.复制toolStripMenuItem1.Click += new System.EventHandler(this.复制toolStripMenuItem1_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // checkBox0
            // 
            this.checkBox0.AutoSize = true;
            this.checkBox0.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox0.Location = new System.Drawing.Point(417, 653);
            this.checkBox0.Name = "checkBox0";
            this.checkBox0.Size = new System.Drawing.Size(84, 24);
            this.checkBox0.TabIndex = 43;
            this.checkBox0.Text = "显示停运";
            this.checkBox0.UseVisualStyleBackColor = true;
            this.checkBox0.CheckedChanged += new System.EventHandler(this.checkBox0_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(507, 653);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 24);
            this.checkBox1.TabIndex = 44;
            this.checkBox1.Text = "显示开行";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ConfirmCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.White;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1292, 697);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkBox0);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfirmCommand";
            this.Text = "确认列车运行计划";
            this.Load += new System.EventHandler(this.ConfirmCommand_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView mainList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox OriginalText_rtb;
        private System.Windows.Forms.DateTimePicker timeTableDate_dtp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox search_tb;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox command_rTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox outputTB;
        private System.Windows.Forms.TextBox searchHighSpeedCommand_tb;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private CCWin.SkinControl.SkinButton save_btn;
        private System.Windows.Forms.CheckBox checkBox0;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}