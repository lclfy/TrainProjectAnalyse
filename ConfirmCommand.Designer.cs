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
            this.mainList = new System.Windows.Forms.ListView();
            this.trainNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.streamStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commandID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timeTableDate_dtp = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.search_tb = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.originalText_rtb = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.commandID_list = new System.Windows.Forms.ListView();
            this.commandID_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.streamStatus_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.importTimeTable_btn = new CCWin.SkinControl.SkinButton();
            this.highSpeedCommand_rtb = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainList
            // 
            this.mainList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.trainNum,
            this.streamStatus,
            this.commandID});
            this.mainList.HideSelection = false;
            this.mainList.Location = new System.Drawing.Point(6, 42);
            this.mainList.Name = "mainList";
            this.mainList.Size = new System.Drawing.Size(378, 424);
            this.mainList.TabIndex = 0;
            this.mainList.UseCompatibleStateImageBehavior = false;
            // 
            // trainNum
            // 
            this.trainNum.Text = "车次";
            // 
            // streamStatus
            // 
            this.streamStatus.Text = "运行状态";
            // 
            // commandID
            // 
            this.commandID.Text = "所含命令";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timeTableDate_dtp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.search_tb);
            this.groupBox1.Controls.Add(this.mainList);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox1.Location = new System.Drawing.Point(209, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 483);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时刻表中的列车";
            // 
            // timeTableDate_dtp
            // 
            this.timeTableDate_dtp.Location = new System.Drawing.Point(111, 15);
            this.timeTableDate_dtp.Name = "timeTableDate_dtp";
            this.timeTableDate_dtp.Size = new System.Drawing.Size(119, 23);
            this.timeTableDate_dtp.TabIndex = 5;
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
            this.label1.Location = new System.Drawing.Point(236, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "查找车次";
            // 
            // search_tb
            // 
            this.search_tb.Location = new System.Drawing.Point(298, 15);
            this.search_tb.Name = "search_tb";
            this.search_tb.Size = new System.Drawing.Size(86, 23);
            this.search_tb.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.originalText_rtb);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox2.Location = new System.Drawing.Point(740, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 483);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查看原文";
            // 
            // originalText_rtb
            // 
            this.originalText_rtb.Location = new System.Drawing.Point(16, 20);
            this.originalText_rtb.Name = "originalText_rtb";
            this.originalText_rtb.Size = new System.Drawing.Size(326, 446);
            this.originalText_rtb.TabIndex = 0;
            this.originalText_rtb.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.commandID_list);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox3.Location = new System.Drawing.Point(605, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(129, 483);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "包含该车次的命令";
            // 
            // commandID_list
            // 
            this.commandID_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.commandID_,
            this.streamStatus_});
            this.commandID_list.HideSelection = false;
            this.commandID_list.Location = new System.Drawing.Point(6, 20);
            this.commandID_list.Name = "commandID_list";
            this.commandID_list.Size = new System.Drawing.Size(117, 446);
            this.commandID_list.TabIndex = 0;
            this.commandID_list.UseCompatibleStateImageBehavior = false;
            // 
            // commandID_
            // 
            this.commandID_.Text = "命令号";
            // 
            // streamStatus_
            // 
            this.streamStatus_.Text = "运行状态";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.importTimeTable_btn);
            this.groupBox4.Controls.Add(this.highSpeedCommand_rtb);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox4.Location = new System.Drawing.Point(26, 74);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(177, 483);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "粘贴当日高铁命令以读取";
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
            this.importTimeTable_btn.Location = new System.Drawing.Point(6, 416);
            this.importTimeTable_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.importTimeTable_btn.MouseBack = null;
            this.importTimeTable_btn.Name = "importTimeTable_btn";
            this.importTimeTable_btn.NormlBack = null;
            this.importTimeTable_btn.Size = new System.Drawing.Size(161, 50);
            this.importTimeTable_btn.TabIndex = 38;
            this.importTimeTable_btn.Text = "更新列车运行计划";
            this.importTimeTable_btn.UseVisualStyleBackColor = false;
            // 
            // highSpeedCommand_rtb
            // 
            this.highSpeedCommand_rtb.Location = new System.Drawing.Point(6, 20);
            this.highSpeedCommand_rtb.Name = "highSpeedCommand_rtb";
            this.highSpeedCommand_rtb.Size = new System.Drawing.Size(165, 387);
            this.highSpeedCommand_rtb.TabIndex = 0;
            this.highSpeedCommand_rtb.Text = "";
            // 
            // ConfirmCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.White;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1129, 623);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfirmCommand";
            this.Text = "ConfirmCommand";
            this.Load += new System.EventHandler(this.ConfirmCommand_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView mainList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox originalText_rtb;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView commandID_list;
        private System.Windows.Forms.ColumnHeader trainNum;
        private System.Windows.Forms.ColumnHeader streamStatus;
        private System.Windows.Forms.ColumnHeader commandID;
        private System.Windows.Forms.DateTimePicker timeTableDate_dtp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox search_tb;
        private System.Windows.Forms.ColumnHeader commandID_;
        private System.Windows.Forms.ColumnHeader streamStatus_;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox highSpeedCommand_rtb;
        private CCWin.SkinControl.SkinButton importTimeTable_btn;
    }
}