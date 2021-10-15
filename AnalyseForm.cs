using CCWin;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TrainProjectAnalyse
{
    public partial class AnalyseForm : Skin_Mac
    {
        List<CommandModel> AllCommands;
        string commandText = "";
        //以存储日期+号头确定唯一命令
        string commandID = "";
        string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
        //存储的文件名
        string fileName = "";
        IWorkbook WorkBook;
        string dataFileName = Application.StartupPath + "\\Data.xls";
        public AnalyseForm(List<CommandModel> _allCM,string _commandText,IWorkbook _wb)
        {
            WorkBook = _wb;
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
                string txtFile = "";
                try
                {
                    txtFile = Application.StartupPath + "\\客调命令\\" + DateTime.Now.ToString("yyyyMMdd-") + commandID + ".txt";
                    fileName = txtFile;
                    FileStream file = new FileStream(txtFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    StreamWriter writer = new StreamWriter(file);
                    writer.WriteLine("命令号：" + commandID + "\n\n" + commandText);
                    writer.Close();
                    file.Close();
                }
                catch (Exception _e)
                {

                }
                
                if(AllCommands.Count == 0)
                {
                    MessageBox.Show("未找到车次，命令内容已保存至\n"+txtFile, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    saveData();
                    //返回OK，模型存入excel，回主界面，（主界面重新从excel读取一次数据）
                    MessageBox.Show("保存成功，命令内容已存储至\n" + txtFile, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
        }

        private void saveData()
        {
            try
            {
                List<CommandModel> _allCM = AllCommands;
                //号头
                //存储时备份一份
                IWorkbook workbook = WorkBook;

                ISheet sheetCommand = workbook.GetSheet("Commands");
                ISheet sheetTrain = workbook.GetSheet("Trains");

                //先存命令表
                //遇到重复号头时，是否继续保存
                int continueSave = -1;
                foreach(IRow row in sheetCommand)
                {
                    if(row == null)
                    {
                        continue;
                    }
                    for(int i=0;i<=row.LastCellNum;i++)
                    {
                        ICell cell = row.GetCell(i);
                        if(cell == null)
                        {
                            continue;
                        }
                        if(cell.ToString().Trim().Equals(commandID))
                        {
                            string previousDate = "";
                            if(row.GetCell(i+1) != null)
                            {
                                previousDate = row.GetCell(i + 1).ToString().Trim();
                                //出现同名命令，是否覆盖
                                DialogResult dr;
                                dr = MessageBox.Show("您已于"+previousDate+"添加过号头为"+commandID+"的调度命令\n"+
                                    "点击“确定”将继续添加该命令", "提醒", MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                if (dr == DialogResult.Yes)
                                {
                                    //开始储存
                                    continueSave = 1;
                                }
                                else
                                {
                                    continueSave = 0;
                                    break;
                                }
                            }
                        }
                    }
                    if(continueSave == 0)
                    {
                        break;
                    }
                }
                if(continueSave == 0)
                {
                    return;
                }
                int lastRow = sheetCommand.LastRowNum;
                {
                    //在最后一行的下一行开始填写
                    int currentRowNumber = 1 + lastRow ;
                    {
                        if (sheetCommand.GetRow(currentRowNumber) == null)
                        {
                            sheetCommand.CreateRow(currentRowNumber);
                        }
                        IRow row = sheetCommand.GetRow(currentRowNumber);
                        ICell cellDate = row.CreateCell(0);
                        cellDate.SetCellValue(nowDate);
                        ICell cellNumber = row.CreateCell(1);
                        cellNumber.SetCellValue(commandID);
                        ICell cellFileName = row.CreateCell(2);
                        cellFileName.SetCellValue(fileName);
                    }
                }

                //存列车表
                //先检索一遍有没有冲突情况
                //找相同车次，若时间有相同，但运行状态不同，则提醒使用新的或保持旧的
                for(int cmCounter = 0;cmCounter<_allCM.Count;cmCounter++)
                {
                    CommandModel _cm = _allCM[cmCounter];
                    bool continueSaveTrain = true;
                    for (int trainCounter = 0; trainCounter < _cm.allTrainModel.Count; trainCounter++)
                    {
                        TrainModel _tm = _cm.allTrainModel[trainCounter];

                        foreach (IRow row in sheetTrain)
                        {
                            if (row != null)
                            {
                                //车次为null的新建
                                if (row.GetCell(3) == null)
                                {
                                    row.CreateCell(3);
                                }
                                if (row.GetCell(4) == null)
                                {
                                    row.CreateCell(4);
                                }
                                if (row.GetCell(3).ToString().Trim().Equals(_tm.firstTrainNum)||
                                    row.GetCell(3).ToString().Trim().Equals(_tm.secondTrainNum)||
                                    row.GetCell(4).ToString().Trim().Equals(_tm.firstTrainNum)||
                                    row.GetCell(4).ToString().Trim().Equals(_tm.secondTrainNum))
                                {
                                    //有重复的，找时间是否有相同的

                                    //continueSaveTrain = false;
                                }
                            }
                        }

                    }
                    //继续保存
                    if (continueSaveTrain)
                    {
                        int lastRowTrain = sheetTrain.LastRowNum;
                        for (int counter = 0; counter < _cm.allTrainModel.Count; counter++)
                        {
                            TrainModel _tm = _cm.allTrainModel[counter];
                            IRow row = sheetTrain.CreateRow(lastRowTrain + counter + 1);
                            row.CreateCell(0).SetCellValue(_tm.createTime.ToString("yyyy/MM/dd"));
                            row.CreateCell(1).SetCellValue(commandID);
                            row.CreateCell(2).SetCellValue(cmCounter);
                            row.CreateCell(3).SetCellValue(_tm.firstTrainNum);
                            row.CreateCell(4).SetCellValue(_tm.secondTrainNum);
                            row.CreateCell(5).SetCellValue(_tm.streamStatus);
                            //日期格式yyyy/MM/dd，以英文逗号区分
                            string dateStr = "";
                            foreach (DateTime _dt in _tm.effectiveDates)
                            {
                                dateStr += _dt.ToString("yyyy/MM/dd") + ",";
                            }
                            dateStr = dateStr.TrimEnd(',');
                            row.CreateCell(6).SetCellValue(dateStr);
                        }
                    }
                }
                //重新修改文件指定单元格样式
                FileStream fs1 = File.OpenWrite(dataFileName);
                workbook.Write(fs1);
                fs1.Close();
                Console.ReadLine();
                workbook.Close();

            }
            catch(Exception e)
            {

            }




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            commandID = textBox1.Text;
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
