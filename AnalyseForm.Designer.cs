﻿namespace TrainProjectAnalyse
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
            this.components = new System.ComponentModel.Container();
            this.analysisListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.DateListView = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.addCM_btn = new CCWin.SkinControl.SkinButton();
            this.editCM_btn = new CCWin.SkinControl.SkinButton();
            this.deleteTrain_btn = new CCWin.SkinControl.SkinButton();
            this.save_btn = new CCWin.SkinControl.SkinButton();
            this.deleteCommand_btn = new CCWin.SkinControl.SkinButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // analysisListView
            // 
            this.analysisListView.HideSelection = false;
            this.analysisListView.Location = new System.Drawing.Point(16, 64);
            this.analysisListView.Margin = new System.Windows.Forms.Padding(2);
            this.analysisListView.Name = "analysisListView";
            this.analysisListView.Size = new System.Drawing.Size(323, 389);
            this.analysisListView.TabIndex = 0;
            this.analysisListView.UseCompatibleStateImageBehavior = false;
            this.analysisListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.analysisListView.DoubleClick += new System.EventHandler(this.analysisListView_DoubleClick);
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
            this.DateListView.Size = new System.Drawing.Size(179, 389);
            this.DateListView.TabIndex = 7;
            this.DateListView.UseCompatibleStateImageBehavior = false;
            this.DateListView.SelectedIndexChanged += new System.EventHandler(this.DateListView_SelectedIndexChanged);
            this.DateListView.DoubleClick += new System.EventHandler(this.DateListView_DoubleClick);
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
            // addCM_btn
            // 
            this.addCM_btn.BackColor = System.Drawing.Color.Transparent;
            this.addCM_btn.BaseColor = System.Drawing.Color.DodgerBlue;
            this.addCM_btn.BorderColor = System.Drawing.Color.DodgerBlue;
            this.addCM_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.addCM_btn.DownBack = null;
            this.addCM_btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addCM_btn.ForeColor = System.Drawing.Color.White;
            this.addCM_btn.Location = new System.Drawing.Point(120, 471);
            this.addCM_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.addCM_btn.MouseBack = null;
            this.addCM_btn.Name = "addCM_btn";
            this.addCM_btn.NormlBack = null;
            this.addCM_btn.Size = new System.Drawing.Size(94, 35);
            this.addCM_btn.TabIndex = 9;
            this.addCM_btn.Text = "添加";
            this.addCM_btn.UseVisualStyleBackColor = false;
            this.addCM_btn.Click += new System.EventHandler(this.addCM_btn_Click_1);
            // 
            // editCM_btn
            // 
            this.editCM_btn.BackColor = System.Drawing.Color.Transparent;
            this.editCM_btn.BaseColor = System.Drawing.Color.DodgerBlue;
            this.editCM_btn.BorderColor = System.Drawing.Color.DodgerBlue;
            this.editCM_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.editCM_btn.DownBack = null;
            this.editCM_btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.editCM_btn.ForeColor = System.Drawing.Color.White;
            this.editCM_btn.Location = new System.Drawing.Point(216, 471);
            this.editCM_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.editCM_btn.MouseBack = null;
            this.editCM_btn.Name = "editCM_btn";
            this.editCM_btn.NormlBack = null;
            this.editCM_btn.Size = new System.Drawing.Size(94, 35);
            this.editCM_btn.TabIndex = 10;
            this.editCM_btn.Text = "编辑";
            this.editCM_btn.UseVisualStyleBackColor = false;
            this.editCM_btn.Click += new System.EventHandler(this.editCM_btn_Click);
            // 
            // deleteTrain_btn
            // 
            this.deleteTrain_btn.BackColor = System.Drawing.Color.Transparent;
            this.deleteTrain_btn.BaseColor = System.Drawing.Color.Tomato;
            this.deleteTrain_btn.BorderColor = System.Drawing.Color.Tomato;
            this.deleteTrain_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.deleteTrain_btn.DownBack = null;
            this.deleteTrain_btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deleteTrain_btn.ForeColor = System.Drawing.Color.White;
            this.deleteTrain_btn.Location = new System.Drawing.Point(312, 471);
            this.deleteTrain_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.deleteTrain_btn.MouseBack = null;
            this.deleteTrain_btn.Name = "deleteTrain_btn";
            this.deleteTrain_btn.NormlBack = null;
            this.deleteTrain_btn.Size = new System.Drawing.Size(94, 35);
            this.deleteTrain_btn.TabIndex = 11;
            this.deleteTrain_btn.Text = "删除列车";
            this.deleteTrain_btn.UseVisualStyleBackColor = false;
            this.deleteTrain_btn.Click += new System.EventHandler(this.deleteCM_btn_Click);
            // 
            // save_btn
            // 
            this.save_btn.BackColor = System.Drawing.Color.Transparent;
            this.save_btn.BaseColor = System.Drawing.Color.DeepPink;
            this.save_btn.BorderColor = System.Drawing.Color.DeepPink;
            this.save_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.save_btn.DownBack = null;
            this.save_btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.save_btn.ForeColor = System.Drawing.Color.White;
            this.save_btn.Location = new System.Drawing.Point(120, 515);
            this.save_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.save_btn.MouseBack = null;
            this.save_btn.Name = "save_btn";
            this.save_btn.NormlBack = null;
            this.save_btn.Size = new System.Drawing.Size(286, 35);
            this.save_btn.TabIndex = 12;
            this.save_btn.Text = "保存";
            this.save_btn.UseVisualStyleBackColor = false;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // deleteCommand_btn
            // 
            this.deleteCommand_btn.BackColor = System.Drawing.Color.Transparent;
            this.deleteCommand_btn.BaseColor = System.Drawing.Color.Red;
            this.deleteCommand_btn.BorderColor = System.Drawing.Color.Red;
            this.deleteCommand_btn.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.deleteCommand_btn.DownBack = null;
            this.deleteCommand_btn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deleteCommand_btn.ForeColor = System.Drawing.Color.White;
            this.deleteCommand_btn.Location = new System.Drawing.Point(429, 515);
            this.deleteCommand_btn.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.deleteCommand_btn.MouseBack = null;
            this.deleteCommand_btn.Name = "deleteCommand_btn";
            this.deleteCommand_btn.NormlBack = null;
            this.deleteCommand_btn.Size = new System.Drawing.Size(94, 35);
            this.deleteCommand_btn.TabIndex = 13;
            this.deleteCommand_btn.Text = "删除命令";
            this.deleteCommand_btn.UseVisualStyleBackColor = false;
            this.deleteCommand_btn.Click += new System.EventHandler(this.deleteCommand_btn_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制toolStripMenuItem1,
            this.粘贴ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // 复制toolStripMenuItem1
            // 
            this.复制toolStripMenuItem1.Name = "复制toolStripMenuItem1";
            this.复制toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.复制toolStripMenuItem1.Text = "复制";
            this.复制toolStripMenuItem1.Click += new System.EventHandler(this.复制toolStripMenuItem1_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // AnalyseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.White;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(546, 573);
            this.Controls.Add(this.deleteCommand_btn);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.deleteTrain_btn);
            this.Controls.Add(this.editCM_btn);
            this.Controls.Add(this.addCM_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DateListView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.analysisListView);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AnalyseForm";
            this.Text = "编辑命令";
            this.Load += new System.EventHandler(this.AnalyseForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView analysisListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView DateListView;
        private System.Windows.Forms.Label label2;
        private CCWin.SkinControl.SkinButton addCM_btn;
        private CCWin.SkinControl.SkinButton editCM_btn;
        private CCWin.SkinControl.SkinButton deleteTrain_btn;
        private CCWin.SkinControl.SkinButton save_btn;
        private CCWin.SkinControl.SkinButton deleteCommand_btn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
    }
}