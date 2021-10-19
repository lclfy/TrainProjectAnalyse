namespace TrainProjectAnalyse
{
    partial class EditForm
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
            this.firstTrainNum_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.secondTrainNum_tb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.streamState_lb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addStartTime_dtp = new System.Windows.Forms.DateTimePicker();
            this.addEndTime_dtp = new System.Windows.Forms.DateTimePicker();
            this.addSingleTime_dtp = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.continuiesDay_addbutton = new System.Windows.Forms.Button();
            this.singleDay_addButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.save_btn = new System.Windows.Forms.Button();
            this.closeForm_btn = new System.Windows.Forms.Button();
            this.date_lv = new System.Windows.Forms.ListView();
            this.editDate_btn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.day_txt = new System.Windows.Forms.TextBox();
            this.month_txt = new System.Windows.Forms.TextBox();
            this.year_txt = new System.Windows.Forms.TextBox();
            this.stopDate_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // firstTrainNum_tb
            // 
            this.firstTrainNum_tb.Location = new System.Drawing.Point(69, 32);
            this.firstTrainNum_tb.Name = "firstTrainNum_tb";
            this.firstTrainNum_tb.Size = new System.Drawing.Size(100, 21);
            this.firstTrainNum_tb.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "车次";
            // 
            // secondTrainNum_tb
            // 
            this.secondTrainNum_tb.Location = new System.Drawing.Point(269, 32);
            this.secondTrainNum_tb.Name = "secondTrainNum_tb";
            this.secondTrainNum_tb.Size = new System.Drawing.Size(100, 21);
            this.secondTrainNum_tb.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "车次2(可空)";
            // 
            // streamState_lb
            // 
            this.streamState_lb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.streamState_lb.FormattingEnabled = true;
            this.streamState_lb.Items.AddRange(new object[] {
            "停运",
            "恢复开行"});
            this.streamState_lb.Location = new System.Drawing.Point(455, 32);
            this.streamState_lb.Margin = new System.Windows.Forms.Padding(6);
            this.streamState_lb.Name = "streamState_lb";
            this.streamState_lb.Size = new System.Drawing.Size(100, 20);
            this.streamState_lb.TabIndex = 214;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 215;
            this.label4.Text = "运行状态";
            // 
            // addStartTime_dtp
            // 
            this.addStartTime_dtp.Location = new System.Drawing.Point(89, 20);
            this.addStartTime_dtp.Name = "addStartTime_dtp";
            this.addStartTime_dtp.Size = new System.Drawing.Size(138, 21);
            this.addStartTime_dtp.TabIndex = 216;
            this.addStartTime_dtp.ValueChanged += new System.EventHandler(this.addStartTime_dtp_ValueChanged);
            // 
            // addEndTime_dtp
            // 
            this.addEndTime_dtp.Location = new System.Drawing.Point(89, 59);
            this.addEndTime_dtp.Name = "addEndTime_dtp";
            this.addEndTime_dtp.Size = new System.Drawing.Size(138, 21);
            this.addEndTime_dtp.TabIndex = 218;
            // 
            // addSingleTime_dtp
            // 
            this.addSingleTime_dtp.Location = new System.Drawing.Point(89, 140);
            this.addSingleTime_dtp.Name = "addSingleTime_dtp";
            this.addSingleTime_dtp.Size = new System.Drawing.Size(138, 21);
            this.addSingleTime_dtp.TabIndex = 219;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.firstTrainNum_tb);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.secondTrainNum_tb);
            this.groupBox1.Controls.Add(this.streamState_lb);
            this.groupBox1.Location = new System.Drawing.Point(25, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 75);
            this.groupBox1.TabIndex = 220;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列车信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.singleDay_addButton);
            this.groupBox2.Controls.Add(this.continuiesDay_addbutton);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.addStartTime_dtp);
            this.groupBox2.Controls.Add(this.addEndTime_dtp);
            this.groupBox2.Controls.Add(this.addSingleTime_dtp);
            this.groupBox2.Location = new System.Drawing.Point(12, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 189);
            this.groupBox2.TabIndex = 221;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "添加日期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 216;
            this.label5.Text = "至";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 220;
            this.label6.Text = "添加连续日期";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 221;
            this.label7.Text = "添加单独日期";
            // 
            // continuiesDay_addbutton
            // 
            this.continuiesDay_addbutton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.continuiesDay_addbutton.Location = new System.Drawing.Point(233, 39);
            this.continuiesDay_addbutton.Name = "continuiesDay_addbutton";
            this.continuiesDay_addbutton.Size = new System.Drawing.Size(53, 23);
            this.continuiesDay_addbutton.TabIndex = 222;
            this.continuiesDay_addbutton.Text = "添加";
            this.continuiesDay_addbutton.UseVisualStyleBackColor = true;
            this.continuiesDay_addbutton.Click += new System.EventHandler(this.continuiesDay_addbutton_Click);
            // 
            // singleDay_addButton
            // 
            this.singleDay_addButton.ForeColor = System.Drawing.SystemColors.Highlight;
            this.singleDay_addButton.Location = new System.Drawing.Point(233, 140);
            this.singleDay_addButton.Name = "singleDay_addButton";
            this.singleDay_addButton.Size = new System.Drawing.Size(53, 23);
            this.singleDay_addButton.TabIndex = 223;
            this.singleDay_addButton.Text = "添加";
            this.singleDay_addButton.UseVisualStyleBackColor = true;
            this.singleDay_addButton.Click += new System.EventHandler(this.singleDay_addButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(152, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 222;
            this.label8.Text = "或";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.stopDate_btn);
            this.groupBox3.Controls.Add(this.editDate_btn);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.day_txt);
            this.groupBox3.Controls.Add(this.month_txt);
            this.groupBox3.Controls.Add(this.year_txt);
            this.groupBox3.Controls.Add(this.date_lv);
            this.groupBox3.Location = new System.Drawing.Point(25, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(588, 244);
            this.groupBox3.TabIndex = 222;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "日期";
            // 
            // save_btn
            // 
            this.save_btn.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.save_btn.Location = new System.Drawing.Point(165, 387);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(147, 42);
            this.save_btn.TabIndex = 224;
            this.save_btn.Text = "保存";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // closeForm_btn
            // 
            this.closeForm_btn.ForeColor = System.Drawing.Color.Tomato;
            this.closeForm_btn.Location = new System.Drawing.Point(318, 387);
            this.closeForm_btn.Name = "closeForm_btn";
            this.closeForm_btn.Size = new System.Drawing.Size(147, 42);
            this.closeForm_btn.TabIndex = 225;
            this.closeForm_btn.Text = "关闭窗口";
            this.closeForm_btn.UseVisualStyleBackColor = true;
            this.closeForm_btn.Click += new System.EventHandler(this.closeForm_btn_Click);
            // 
            // date_lv
            // 
            this.date_lv.FullRowSelect = true;
            this.date_lv.HideSelection = false;
            this.date_lv.Location = new System.Drawing.Point(312, 19);
            this.date_lv.Name = "date_lv";
            this.date_lv.Size = new System.Drawing.Size(258, 161);
            this.date_lv.TabIndex = 225;
            this.date_lv.UseCompatibleStateImageBehavior = false;
            this.date_lv.View = System.Windows.Forms.View.List;
            this.date_lv.SelectedIndexChanged += new System.EventHandler(this.date_lv_SelectedIndexChanged);
            // 
            // editDate_btn
            // 
            this.editDate_btn.Location = new System.Drawing.Point(504, 185);
            this.editDate_btn.Name = "editDate_btn";
            this.editDate_btn.Size = new System.Drawing.Size(66, 23);
            this.editDate_btn.TabIndex = 231;
            this.editDate_btn.Text = "修改";
            this.editDate_btn.UseVisualStyleBackColor = true;
            this.editDate_btn.Click += new System.EventHandler(this.editDate_btn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(481, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 230;
            this.label9.Text = "日";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(365, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 229;
            this.label10.Text = "年";
            // 
            // day_txt
            // 
            this.day_txt.Location = new System.Drawing.Point(443, 187);
            this.day_txt.Name = "day_txt";
            this.day_txt.Size = new System.Drawing.Size(32, 21);
            this.day_txt.TabIndex = 228;
            this.day_txt.TextChanged += new System.EventHandler(this.day_txt_TextChanged);
            this.day_txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.day_txt_KeyPress);
            // 
            // month_txt
            // 
            this.month_txt.Location = new System.Drawing.Point(385, 187);
            this.month_txt.Name = "month_txt";
            this.month_txt.Size = new System.Drawing.Size(32, 21);
            this.month_txt.TabIndex = 227;
            this.month_txt.TextChanged += new System.EventHandler(this.month_txt_TextChanged);
            this.month_txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.month_txt_KeyPress);
            // 
            // year_txt
            // 
            this.year_txt.Location = new System.Drawing.Point(316, 187);
            this.year_txt.Name = "year_txt";
            this.year_txt.Size = new System.Drawing.Size(43, 21);
            this.year_txt.TabIndex = 226;
            this.year_txt.TextChanged += new System.EventHandler(this.year_txt_TextChanged);
            this.year_txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.year_txt_KeyPress);
            // 
            // stopDate_btn
            // 
            this.stopDate_btn.ForeColor = System.Drawing.Color.OrangeRed;
            this.stopDate_btn.Location = new System.Drawing.Point(408, 214);
            this.stopDate_btn.Name = "stopDate_btn";
            this.stopDate_btn.Size = new System.Drawing.Size(67, 23);
            this.stopDate_btn.TabIndex = 232;
            this.stopDate_btn.Text = "删除日期";
            this.stopDate_btn.UseVisualStyleBackColor = true;
            this.stopDate_btn.Click += new System.EventHandler(this.stopDate_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 233;
            this.label1.Text = "月";
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.White;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(635, 450);
            this.Controls.Add(this.closeForm_btn);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditForm";
            this.Text = "编辑列车信息";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox firstTrainNum_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox secondTrainNum_tb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox streamState_lb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker addStartTime_dtp;
        private System.Windows.Forms.DateTimePicker addEndTime_dtp;
        private System.Windows.Forms.DateTimePicker addSingleTime_dtp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button singleDay_addButton;
        private System.Windows.Forms.Button continuiesDay_addbutton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button closeForm_btn;
        private System.Windows.Forms.ListView date_lv;
        private System.Windows.Forms.Button stopDate_btn;
        private System.Windows.Forms.Button editDate_btn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox day_txt;
        private System.Windows.Forms.TextBox month_txt;
        private System.Windows.Forms.TextBox year_txt;
        private System.Windows.Forms.Label label1;
    }
}