using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace TrainProjectAnalyse
{
    public partial class AnalyseForm : Skin_Mac
    {
        List<CommandModel> AllCommands;
        string commandText = "";
        public AnalyseForm(List<CommandModel> _allCM,string _commandText)
        {
            AllCommands = _allCM;
            commandText = _commandText;
            InitializeComponent();
        }


        private void AnalyseForm_Load(object sender, EventArgs e)
        {
            InitView();
        }

        private void InitView()
        {
            //初始化界面与内容
            analysisListView.View = View.Details;
            analysisListView.ShowGroups = false;
            string[] analysisListViewTitle = new string[] { "条目","序号","车次" , "运行状态","日期" };
            for (int i = 0; i < analysisListViewTitle.Length; i++)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = analysisListViewTitle[i];   //设置列标题 
                if(i == 0 || i == 1)
                {
                    ch.Width = 45;
                }
                else
                {
                    ch.Width = 70;
                }

                this.analysisListView.Columns.Add(ch);    //将列头添加到ListView控件。
                analysisListView.FullRowSelect = true;
            }

            DateListView.View = View.Details;
            DateListView.ShowGroups = false;
            string[] dateListViewTitle = new string[] { "日期" };
            for (int i = 0; i < dateListViewTitle.Length; i++)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = dateListViewTitle[i];   //设置列标题 
                ch.Width = 130;
                DateListView.FullRowSelect = true;
                this.DateListView.Columns.Add(ch);    //将列头添加到ListView控件。
            }
            RefreshData();
         
        }

        private void RefreshData()
        {
            this.analysisListView.BeginUpdate();
            //添加数据
            List<CommandModel> _allCM = AllCommands;

            for (int q = 0; q < _allCM.Count; q++)
            {
                CommandModel _cm = _allCM[q];
                for (int j = 0; j < _cm.allTrainModel.Count; j++)
                {
                    TrainModel _tm = _cm.allTrainModel[j];
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = "第" + (q + 1).ToString() + "项";
                    item.SubItems.Add((j + 1).ToString());
                    if (_tm.secondTrainNum.Length != 0)
                    {
                        item.SubItems.Add(_tm.firstTrainNum + "/" + _tm.secondTrainNum);
                    }
                    else
                    {
                        item.SubItems.Add(_tm.firstTrainNum);
                    }
                    //0停运，1恢复开行，-1未定义
                    if (_tm.streamStatus == 0)
                    {
                        item.SubItems.Add("停运");
                    }
                    else if (_tm.streamStatus == 1)
                    {
                        item.SubItems.Add("恢复开行");
                    }
                    else if (_tm.streamStatus == -1)
                    {
                        item.SubItems.Add("未找到");
                    }
                    item.SubItems.Add("单击");
                    this.analysisListView.Items.Add(item);
                }
            }
            this.analysisListView.EndUpdate();
        }
        private void getDateTime(string _titleID,string _ID)
        {
            string _tempTitle = _titleID.Replace("第", "").Replace("项", "");
            int titleID = 0;
            int.TryParse(_tempTitle, out titleID);
            titleID = titleID - 1;
            int selectedID = int.Parse(_ID) - 1;

            this.DateListView.BeginUpdate();
            DateListView.Items.Clear();
            //添加数据
            List<CommandModel> _allCM = AllCommands;
            CommandModel _cm = _allCM[titleID];
            TrainModel _tm = _cm.allTrainModel[selectedID];
            foreach(DateTime _dt in _tm.effectiveDates)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = _dt.ToLongDateString();
                DateListView.Items.Add(item);
            }
            this.DateListView.EndUpdate();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(analysisListView.SelectedItems.Count != 0)
            {
                getDateTime(analysisListView.SelectedItems[0].SubItems[0].Text, analysisListView.SelectedItems[0].SubItems[1].Text);
            }

        }

        private void DateListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入命令号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
            }
            else
            {
                //命令原文存txt

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //号头只能为数字
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void label3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
