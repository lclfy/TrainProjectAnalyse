namespace TrainProjectAnalyse
{
    partial class AnalyseForm
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
            this.analysisListView = new System.Windows.Forms.ListView();
            this.editCM_btn = new System.Windows.Forms.Button();
            this.deleteCM_btn = new System.Windows.Forms.Button();
            this.addCM_btn = new System.Windows.Forms.Button();
            this.save_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.DateListView = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.year_txt = new System.Windows.Forms.TextBox();
            this.month_txt = new System.Windows.Forms.TextBox();
            this.day_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.editDate_btn = new System.Windows.Forms.Button();
            this.stopDate_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // analysisListView
            // 
            this.analysisListView.HideSelection = false;
            this.analysisListView.Location = new System.Drawing.Point(16, 64);
            this.analysisListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.analysisListView.Name = "analysisListView";
            this.analysisListView.Size = new System.Drawing.Size(323, 389);
            this.analysisListView.TabIndex = 0;
            this.analysisListView.UseCompatibleStateImageBehavior = false;
            this.analysisListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // editCM_btn
            // 
            this.editCM_btn.Location = new System.Drawing.Point(119, 472);
            this.editCM_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.editCM_btn.Name = "editCM_btn";
            this.editCM_btn.Size = new System.Drawing.Size(93, 35);
            this.editCM_btn.TabIndex = 1;
            this.editCM_btn.Text = "编辑";
            this.editCM_btn.UseVisualStyleBackColor = true;
            // 
            // deleteCM_btn
            // 
            this.deleteCM_btn.Location = new System.Drawing.Point(313, 472);
            this.deleteCM_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteCM_btn.Name = "deleteCM_btn";
            this.deleteCM_btn.Size = new System.Drawing.Size(93, 35);
            this.deleteCM_btn.TabIndex = 2;
            this.deleteCM_btn.Text = "删除";
            this.deleteCM_btn.UseVisualStyleBackColor = true;
            // 
            // addCM_btn
            // 
            this.addCM_btn.Location = new System.Drawing.Point(216, 472);
            this.addCM_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addCM_btn.Name = "addCM_btn";
            this.addCM_btn.Size = new System.Drawing.Size(93, 35);
            this.addCM_btn.TabIndex = 3;
            this.addCM_btn.Text = "添加";
            this.addCM_btn.UseVisualStyleBackColor = true;
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(120, 511);
            this.save_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(286, 35);
            this.save_btn.TabIndex = 4;
            this.save_btn.Text = "保存";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "填写命令号";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // DateListView
            // 
            this.DateListView.HideSelection = false;
            this.DateListView.Location = new System.Drawing.Point(344, 64);
            this.DateListView.Name = "DateListView";
            this.DateListView.Size = new System.Drawing.Size(179, 335);
            this.DateListView.TabIndex = 7;
            this.DateListView.UseCompatibleStateImageBehavior = false;
            this.DateListView.SelectedIndexChanged += new System.EventHandler(this.DateListView_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(342, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "日期（点击车次条目显示）";
            // 
            // year_txt
            // 
            this.year_txt.Location = new System.Drawing.Point(345, 405);
            this.year_txt.Name = "year_txt";
            this.year_txt.Size = new System.Drawing.Size(56, 21);
            this.year_txt.TabIndex = 9;
            // 
            // month_txt
            // 
            this.month_txt.Location = new System.Drawing.Point(430, 405);
            this.month_txt.Name = "month_txt";
            this.month_txt.Size = new System.Drawing.Size(32, 21);
            this.month_txt.TabIndex = 10;
            // 
            // day_txt
            // 
            this.day_txt.Location = new System.Drawing.Point(468, 405);
            this.day_txt.Name = "day_txt";
            this.day_txt.Size = new System.Drawing.Size(32, 21);
            this.day_txt.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 409);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "年";
            this.label3.TextChanged += new System.EventHandler(this.label3_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(506, 409);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "日";
            // 
            // editDate_btn
            // 
            this.editDate_btn.Location = new System.Drawing.Point(344, 430);
            this.editDate_btn.Name = "editDate_btn";
            this.editDate_btn.Size = new System.Drawing.Size(98, 23);
            this.editDate_btn.TabIndex = 14;
            this.editDate_btn.Text = "修改";
            this.editDate_btn.UseVisualStyleBackColor = true;
            // 
            // stopDate_btn
            // 
            this.stopDate_btn.ForeColor = System.Drawing.Color.OrangeRed;
            this.stopDate_btn.Location = new System.Drawing.Point(448, 430);
            this.stopDate_btn.Name = "stopDate_btn";
            this.stopDate_btn.Size = new System.Drawing.Size(75, 23);
            this.stopDate_btn.TabIndex = 15;
            this.stopDate_btn.Text = "删除";
            this.stopDate_btn.UseVisualStyleBackColor = true;
            // 
            // AnalyseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 573);
            this.Controls.Add(this.stopDate_btn);
            this.Controls.Add(this.editDate_btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.day_txt);
            this.Controls.Add(this.month_txt);
            this.Controls.Add(this.year_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DateListView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.addCM_btn);
            this.Controls.Add(this.deleteCM_btn);
            this.Controls.Add(this.editCM_btn);
            this.Controls.Add(this.analysisListView);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AnalyseForm";
            this.Text = "检查与修改";
            this.Load += new System.EventHandler(this.AnalyseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView analysisListView;
        private System.Windows.Forms.Button editCM_btn;
        private System.Windows.Forms.Button deleteCM_btn;
        private System.Windows.Forms.Button addCM_btn;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView DateListView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox year_txt;
        private System.Windows.Forms.TextBox month_txt;
        private System.Windows.Forms.TextBox day_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button editDate_btn;
        private System.Windows.Forms.Button stopDate_btn;
    }
}