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
            this.components = new System.ComponentModel.Container();
            this.command_rtb = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addCommandManually_btn = new CCWin.SkinControl.SkinButton();
            this.edit_btn = new CCWin.SkinControl.SkinButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TimeTableFileName_lbl = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.today_rb = new System.Windows.Forms.RadioButton();
            this.tomorrow_rb = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.OriginalText_rtb = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.searchCommand_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.commandListView = new System.Windows.Forms.ListView();
            this.dataAnalyse_btn = new CCWin.SkinControl.SkinButton();
            this.importTimeTable_btn = new CCWin.SkinControl.SkinButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.searchTrainNumber_tb = new System.Windows.Forms.TextBox();
            this.secondListTitle_lbl = new System.Windows.Forms.Label();
            this.searchResult_rtb = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // command_rtb
            // 
            this.command_rtb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.command_rtb.Location = new System.Drawing.Point(5, 19);
            this.command_rtb.Margin = new System.Windows.Forms.Padding(2);
            this.command_rtb.Name = "command_rtb";
            this.command_rtb.Size = new System.Drawing.Size(303, 495);
            this.command_rtb.TabIndex = 0;
            this.command_rtb.Text = "";
            this.command_rtb.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addCommandManually_btn);
            this.groupBox1.Controls.Add(this.edit_btn);
            this.groupBox1.Controls.Add(this.command_rtb);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox1.Location = new System.Drawing.Point(24, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 578);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加命令-调度命令粘贴框";
            // 
            // addCommandManually_btn
            // 
            this.addCommandManually_btn.BackColor = System.Drawing.Color.Transparent;
            this.addCommandManually_btn.BaseColor = System.Drawing.Color.DarkGreen;
            this.addCommandManually_btn.BorderColor = System.Drawing.Color.Green;
            this.addCommandManually_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.addCommandManually_btn.DownBack = null;
            this.addCommandManually_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addCommandManually_btn.ForeColor = System.Drawing.Color.White;
            this.addCommandManually_btn.Location = new System.Drawing.Point(19, 520);
            this.addCommandManually_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.addCommandManually_btn.MouseBack = null;
            this.addCommandManually_btn.Name = "addCommandManually_btn";
            this.addCommandManually_btn.NormlBack = null;
            this.addCommandManually_btn.Size = new System.Drawing.Size(136, 44);
            this.addCommandManually_btn.TabIndex = 40;
            this.addCommandManually_btn.Text = "手动添加命令";
            this.addCommandManually_btn.UseVisualStyleBackColor = false;
            this.addCommandManually_btn.Click += new System.EventHandler(this.addCommandManually_btn_Click);
            // 
            // edit_btn
            // 
            this.edit_btn.BackColor = System.Drawing.Color.Transparent;
            this.edit_btn.BaseColor = System.Drawing.Color.OrangeRed;
            this.edit_btn.BorderColor = System.Drawing.Color.OrangeRed;
            this.edit_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.edit_btn.DownBack = null;
            this.edit_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.edit_btn.ForeColor = System.Drawing.Color.White;
            this.edit_btn.Location = new System.Drawing.Point(157, 520);
            this.edit_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.edit_btn.MouseBack = null;
            this.edit_btn.Name = "edit_btn";
            this.edit_btn.NormlBack = null;
            this.edit_btn.Size = new System.Drawing.Size(139, 44);
            this.edit_btn.TabIndex = 39;
            this.edit_btn.Text = "编辑";
            this.edit_btn.UseVisualStyleBackColor = false;
            this.edit_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TimeTableFileName_lbl);
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.dataAnalyse_btn);
            this.groupBox2.Controls.Add(this.importTimeTable_btn);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox2.Location = new System.Drawing.Point(349, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(742, 578);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已添加的命令";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // TimeTableFileName_lbl
            // 
            this.TimeTableFileName_lbl.AutoSize = true;
            this.TimeTableFileName_lbl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimeTableFileName_lbl.Location = new System.Drawing.Point(270, 494);
            this.TimeTableFileName_lbl.Name = "TimeTableFileName_lbl";
            this.TimeTableFileName_lbl.Size = new System.Drawing.Size(56, 17);
            this.TimeTableFileName_lbl.TabIndex = 42;
            this.TimeTableFileName_lbl.Text = "已选择：";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.White;
            this.groupBox6.Controls.Add(this.today_rb);
            this.groupBox6.Controls.Add(this.tomorrow_rb);
            this.groupBox6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.ForeColor = System.Drawing.Color.DarkOrange;
            this.groupBox6.Location = new System.Drawing.Point(12, 476);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox6.Size = new System.Drawing.Size(95, 93);
            this.groupBox6.TabIndex = 41;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "日期";
            // 
            // today_rb
            // 
            this.today_rb.AutoSize = true;
            this.today_rb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.today_rb.Location = new System.Drawing.Point(23, 31);
            this.today_rb.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.today_rb.Name = "today_rb";
            this.today_rb.Size = new System.Drawing.Size(50, 21);
            this.today_rb.TabIndex = 23;
            this.today_rb.TabStop = true;
            this.today_rb.Text = "当日";
            this.today_rb.UseVisualStyleBackColor = true;
            // 
            // tomorrow_rb
            // 
            this.tomorrow_rb.AutoSize = true;
            this.tomorrow_rb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tomorrow_rb.Location = new System.Drawing.Point(23, 60);
            this.tomorrow_rb.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tomorrow_rb.Name = "tomorrow_rb";
            this.tomorrow_rb.Size = new System.Drawing.Size(50, 21);
            this.tomorrow_rb.TabIndex = 22;
            this.tomorrow_rb.TabStop = true;
            this.tomorrow_rb.Text = "次日";
            this.tomorrow_rb.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.OriginalText_rtb);
            this.groupBox5.ForeColor = System.Drawing.Color.OrangeRed;
            this.groupBox5.Location = new System.Drawing.Point(305, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(229, 452);
            this.groupBox5.TabIndex = 40;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "命令原文（双击文本框打开）";
            // 
            // OriginalText_rtb
            // 
            this.OriginalText_rtb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OriginalText_rtb.Location = new System.Drawing.Point(5, 21);
            this.OriginalText_rtb.Margin = new System.Windows.Forms.Padding(2);
            this.OriginalText_rtb.Name = "OriginalText_rtb";
            this.OriginalText_rtb.ReadOnly = true;
            this.OriginalText_rtb.Size = new System.Drawing.Size(219, 426);
            this.OriginalText_rtb.TabIndex = 1;
            this.OriginalText_rtb.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.searchCommand_tb);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.commandListView);
            this.groupBox4.ForeColor = System.Drawing.Color.OrangeRed;
            this.groupBox4.Location = new System.Drawing.Point(6, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(293, 449);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "全部命令及车次";
            // 
            // searchCommand_tb
            // 
            this.searchCommand_tb.Location = new System.Drawing.Point(174, 16);
            this.searchCommand_tb.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.searchCommand_tb.Name = "searchCommand_tb";
            this.searchCommand_tb.Size = new System.Drawing.Size(113, 26);
            this.searchCommand_tb.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(107, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "搜索命令";
            // 
            // commandListView
            // 
            this.commandListView.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.commandListView.HideSelection = false;
            this.commandListView.Location = new System.Drawing.Point(6, 46);
            this.commandListView.Name = "commandListView";
            this.commandListView.Size = new System.Drawing.Size(281, 397);
            this.commandListView.TabIndex = 33;
            this.commandListView.UseCompatibleStateImageBehavior = false;
            this.commandListView.View = System.Windows.Forms.View.Details;
            this.commandListView.SelectedIndexChanged += new System.EventHandler(this.commandListView_SelectedIndexChanged);
            // 
            // dataAnalyse_btn
            // 
            this.dataAnalyse_btn.BackColor = System.Drawing.Color.Transparent;
            this.dataAnalyse_btn.BaseColor = System.Drawing.Color.DeepPink;
            this.dataAnalyse_btn.BorderColor = System.Drawing.Color.DeepPink;
            this.dataAnalyse_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.dataAnalyse_btn.DownBack = null;
            this.dataAnalyse_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataAnalyse_btn.ForeColor = System.Drawing.Color.White;
            this.dataAnalyse_btn.Location = new System.Drawing.Point(117, 526);
            this.dataAnalyse_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.dataAnalyse_btn.MouseBack = null;
            this.dataAnalyse_btn.Name = "dataAnalyse_btn";
            this.dataAnalyse_btn.NormlBack = null;
            this.dataAnalyse_btn.Size = new System.Drawing.Size(412, 43);
            this.dataAnalyse_btn.TabIndex = 38;
            this.dataAnalyse_btn.Text = "查看列车运行情况";
            this.dataAnalyse_btn.UseVisualStyleBackColor = false;
            // 
            // importTimeTable_btn
            // 
            this.importTimeTable_btn.BackColor = System.Drawing.Color.Transparent;
            this.importTimeTable_btn.BaseColor = System.Drawing.Color.DodgerBlue;
            this.importTimeTable_btn.BorderColor = System.Drawing.Color.DodgerBlue;
            this.importTimeTable_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.importTimeTable_btn.DownBack = null;
            this.importTimeTable_btn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importTimeTable_btn.ForeColor = System.Drawing.Color.White;
            this.importTimeTable_btn.Location = new System.Drawing.Point(117, 480);
            this.importTimeTable_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.importTimeTable_btn.MouseBack = null;
            this.importTimeTable_btn.Name = "importTimeTable_btn";
            this.importTimeTable_btn.NormlBack = null;
            this.importTimeTable_btn.Size = new System.Drawing.Size(127, 43);
            this.importTimeTable_btn.TabIndex = 37;
            this.importTimeTable_btn.Text = "导入时刻表文件";
            this.importTimeTable_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.searchTrainNumber_tb);
            this.groupBox3.Controls.Add(this.secondListTitle_lbl);
            this.groupBox3.Controls.Add(this.searchResult_rtb);
            this.groupBox3.ForeColor = System.Drawing.Color.OrangeRed;
            this.groupBox3.Location = new System.Drawing.Point(546, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(190, 545);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查找";
            // 
            // searchTrainNumber_tb
            // 
            this.searchTrainNumber_tb.Location = new System.Drawing.Point(74, 16);
            this.searchTrainNumber_tb.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.searchTrainNumber_tb.Name = "searchTrainNumber_tb";
            this.searchTrainNumber_tb.Size = new System.Drawing.Size(104, 26);
            this.searchTrainNumber_tb.TabIndex = 30;
            // 
            // secondListTitle_lbl
            // 
            this.secondListTitle_lbl.AutoSize = true;
            this.secondListTitle_lbl.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.secondListTitle_lbl.Location = new System.Drawing.Point(10, 19);
            this.secondListTitle_lbl.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.secondListTitle_lbl.Name = "secondListTitle_lbl";
            this.secondListTitle_lbl.Size = new System.Drawing.Size(65, 20);
            this.secondListTitle_lbl.TabIndex = 28;
            this.secondListTitle_lbl.Text = "搜索车次";
            // 
            // searchResult_rtb
            // 
            this.searchResult_rtb.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.searchResult_rtb.Location = new System.Drawing.Point(10, 45);
            this.searchResult_rtb.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.searchResult_rtb.Name = "searchResult_rtb";
            this.searchResult_rtb.ReadOnly = true;
            this.searchResult_rtb.Size = new System.Drawing.Size(168, 487);
            this.searchResult_rtb.TabIndex = 29;
            this.searchResult_rtb.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(987, 638);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "build01-202110";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.White;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1115, 682);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "郑州站列车运行计划辅助系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox command_rtb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox searchTrainNumber_tb;
        private System.Windows.Forms.Label secondListTitle_lbl;
        private System.Windows.Forms.RichTextBox searchResult_rtb;
        private System.Windows.Forms.ListView commandListView;
        private System.Windows.Forms.RichTextBox OriginalText_rtb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchCommand_tb;
        private CCWin.SkinControl.SkinButton importTimeTable_btn;
        private CCWin.SkinControl.SkinButton edit_btn;
        private CCWin.SkinControl.SkinButton dataAnalyse_btn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private CCWin.SkinControl.SkinButton addCommandManually_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton today_rb;
        private System.Windows.Forms.RadioButton tomorrow_rb;
        private System.Windows.Forms.Label TimeTableFileName_lbl;
    }
}

