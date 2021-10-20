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
        List<NormalCommandModel> AllCommands;
        string commandText = "";
        //以存储日期+号头确定唯一命令
        string commandID = "";
        string nowDate = DateTime.Now.ToShortDateString();
        //存储的文件名
        string fileName = "";
        IWorkbook WorkBook;
        //0为新增命令模式，1为修改现有命令模式，增加删除命令按钮和功能
        int EditMode;
        string dataFileName = Application.StartupPath + "\\Data.xls";
        public AnalyseForm(List<NormalCommandModel> _allCM,string _commandText,IWorkbook _wb,int _editMode)
        {
            //EditMode为0则为新增命令，为1则为编辑现有命令
            WorkBook = _wb;
            AllCommands = _allCM;
            EditMode = _editMode;
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
                    ch.Width = 80;
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
            analysisListView.Items.Clear();
            this.analysisListView.BeginUpdate();
            //添加数据
            List<NormalCommandModel> _allCM = AllCommands;

            for (int q = 0; q < _allCM.Count; q++)
            {
                NormalCommandModel _cm = _allCM[q];
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
        private TrainModel  getTrainAndDateTime(string _titleID,string _ID)
        {
            string _tempTitle = _titleID.Replace("第", "").Replace("项", "");
            int titleID = 0;
            int.TryParse(_tempTitle, out titleID);
            titleID = titleID - 1;
            int selectedID = int.Parse(_ID) - 1;

            this.DateListView.BeginUpdate();
            DateListView.Items.Clear();
            //添加数据
            List<NormalCommandModel> _allCM = AllCommands;
            NormalCommandModel _cm = _allCM[titleID];
            TrainModel _tm = _cm.allTrainModel[selectedID];
            //找到车了，返回这个车，刷新它的时间列表
            foreach(DateTime _dt in _tm.effectiveDates)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = _dt.ToLongDateString();
                DateListView.Items.Add(item);
            }
            this.DateListView.EndUpdate();
            return _tm;
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(analysisListView.SelectedItems.Count != 0)
            {
                getTrainAndDateTime(analysisListView.SelectedItems[0].SubItems[0].Text, analysisListView.SelectedItems[0].SubItems[1].Text);
            }

        }

        private void DateListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private int saveData()
        {
            try
            {
                List<NormalCommandModel> _allCM = AllCommands;
                //号头
                //存储时备份一份
                IWorkbook workbook = WorkBook;

                ISheet sheetCommand = workbook.GetSheet("Commands");
                ISheet sheetTrain = workbook.GetSheet("Trains");
                //命令表是否保存，0不保存，1保存，2遇到当日重复文件，不保存txt
                int continueSave = -1;

                //存列车表(列车表优先级高，列车表不存了命令表也不存了)
                //先检索一遍有没有冲突情况
                //找相同车次，若时间有相同，但运行状态不同，则提醒使用新的或保持旧的
                //车次的ID
                int IDCounter = 1;
                for (int cmCounter = 0; cmCounter < _allCM.Count; cmCounter++)
                {
                    NormalCommandModel _cm = _allCM[cmCounter];
                    bool continueSaveTrain = true;
                    string modelID = _cm.ID + commandID;
                    _cm.commandID = commandID;
                    _cm.ID = modelID;

                    for (int trainCounter = 0; trainCounter < _cm.allTrainModel.Count; trainCounter++)
                    {
                        TrainModel _tm = _cm.allTrainModel[trainCounter];
                        //_tm.ID = commandID;
                        _tm.ID += commandID + IDCounter.ToString("D3");
                        IDCounter++;
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
                                string fir = row.GetCell(3).ToString();
                                string sec = row.GetCell(4).ToString();
                                if ((fir.Trim().Equals(_tm.firstTrainNum) ||
                                    fir.Trim().Equals(_tm.secondTrainNum) ||
                                    sec.Trim().Equals(_tm.firstTrainNum) ||
                                    sec.Trim().Equals(_tm.secondTrainNum))&&
                                    fir.Trim().Length!=0&&sec.Trim().Length!=0&&
                                    _tm.firstTrainNum.Trim().Length!=0&&
                                    _tm.secondTrainNum.Trim().Length!=0)
                                {
                                    //有重复的，找时间是否有相同的
                                    //有相同的问是否保存
                                    ICell cell = row.GetCell(6);
                                    bool hasGotSameTime = false;
                                    string sameDateList = "";
                                    foreach (DateTime _dt in _tm.effectiveDates)
                                    {
                                        //有日期交叉
                                        string[] allDates = cell.ToString().Split(',');
                                        if(allDates.Length != 0)
                                        {
                                            for(int ij = 0; ij < allDates.Length; ij++)
                                            {
                                                DateTime _targetDate = DateTime.Parse(allDates[ij]);
                                                if (_targetDate.Year.Equals(_dt.Year)&&
                                                    _targetDate.Month.Equals(_dt.Month) &&
                                                    _targetDate.Day.Equals(_dt.Day) )
                                                {
                                                    sameDateList = sameDateList + _dt.ToShortDateString() + "\n";
                                                    hasGotSameTime = true;
                                                }
                                            }

                                        }
                             
                                    }
                                    if(hasGotSameTime)
                                    {
                                        string previousDate = "";
                                        if (row.GetCell(0) != null)
                                        {
                                            if (row.GetCell(1) == null)
                                            {
                                                row.GetCell(1).SetCellValue(" ");
                                            }
                                            previousDate = row.GetCell(0).ToString().Trim();
                                            DialogResult dr;

                                            //if(previousDate)
                                            dr = MessageBox.Show("您于" + previousDate + "添加的" + row.GetCell(1).ToString() + "命令中，\n" +
                                                "与当前命令存在车次时间交叉冲突，\n" +
                                                "车次：" + _tm.firstTrainNum +
                                                "\n日期：\n" + sameDateList +
                                                "点击确定将继续添加车次，取消则停止", "提醒", MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                            if (dr == DialogResult.Yes)
                                            {
                                                //开始储存
                                                continueSaveTrain = true;
                                                break;
                                            }
                                            else
                                            {
                                                continueSaveTrain = false;
                                                continueSave = 0;
                                                break;
                                            }
                                        }
                                    }

                                    //continueSaveTrain = false;
                                }
                            }
                        }

                    }
                    //继续保存
                    //
                    //
                    //此时如果是编辑新表，则是查找后更改内容
                    //
                    //
                    if (continueSaveTrain)
                    {
                        int lastRowTrain = sheetTrain.LastRowNum;
                        for (int counter = 0; counter < _cm.allTrainModel.Count; counter++)
                        {
                            TrainModel _tm = _cm.allTrainModel[counter];
                            IRow row = sheetTrain.CreateRow(lastRowTrain + counter + 1);
                            row.CreateCell(0).SetCellValue(_tm.createTime.ToShortDateString());
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
                            row.CreateCell(7).SetCellValue(_tm.ID);
                        }
                    }
                    
                }

                //存命令表
                //遇到重复号头时，是否继续保存

                bool hasSameCommand = false;
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
                            if(row.GetCell(i-1) != null)
                            {
                                previousDate = row.GetCell(i - 1).ToString().Trim();
                                //出现同名命令，是否覆盖
                                DialogResult dr;
                                //如果同一天添加了两次相同命令，则不存储CommandModel，避免重复
                                //同时写continuesave = 2，保存时不保存txt
                                if (previousDate.Equals(DateTime.Now.ToShortDateString()))
                                {
                                    hasSameCommand = true;
                                }

                                //if(previousDate)
                                dr = MessageBox.Show("您已于"+previousDate+"添加过号头为"+commandID+"的调度命令\n"+
                                    "点击“确定”将继续添加该命令", "提醒", MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                if (dr == DialogResult.Yes)
                                {
                                    //开始储存
                                    continueSave = 1;
                                    break;
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
                    return 0;
                }
                if (!hasSameCommand)
                {
                    int lastRow = sheetCommand.LastRowNum;
                    {
                        //在最后一行的下一行开始填写
                        int currentRowNumber = 1 + lastRow;
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
                            ICell cellID = row.CreateCell(3);
                            cellID.SetCellValue(_allCM[0].ID);
                        }
                    }
                }
  

                //重新修改文件指定单元格样式
                FileStream fs1 = File.OpenWrite(dataFileName);
                workbook.Write(fs1);
                fs1.Close();
                Console.ReadLine();
                workbook.Close();
                if (hasSameCommand)
                {
                    continueSave = 2;
                }
                return continueSave;
            }
            catch(Exception e)
            {

            }
            return 0;
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



        private void EditTrainData()
        {
            if (analysisListView.SelectedItems.Count != 0)
            {
                TrainModel _tempTrainModel = getTrainAndDateTime(analysisListView.SelectedItems[0].SubItems[0].Text, analysisListView.SelectedItems[0].SubItems[1].Text);
                EditForm _ef = new EditForm(_tempTrainModel, 1);
                _ef.Owner = this;
                _ef.Show();
            }
        }

        //添加编辑完成后，从子窗口中调用该方法
        //new为0 ，edit为1
        public void EditComplete(TrainModel _tm,int newOrEdit)
        {
            TrainModel trainModel = _tm;
            if(AllCommands.Count == 0)
            {
                NormalCommandModel _cm = new NormalCommandModel();
                _cm.createTime = DateTime.Now;
                _cm.commandID = commandID;
                AllCommands.Add(_cm);
            }
            AllCommands[0].allTrainModel.Add(trainModel);
            RefreshData();
            int a = 9;
        }

        private void editCM_btn_Click(object sender, EventArgs e)
        {
            EditTrainData();
        }

        private void DateListView_DoubleClick(object sender, EventArgs e)
        {
            EditTrainData();
        }

        private void analysisListView_DoubleClick(object sender, EventArgs e)
        {
            EditTrainData();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入命令号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
            }
            else
            {
                string txtFile = "";
                int continueSave = -1;
                txtFile = Application.StartupPath + "\\客调命令\\" + DateTime.Now.ToString("yyyyMMdd-") + commandID + ".txt";
                fileName = txtFile;
                if (AllCommands.Count == 0)
                {
                    MessageBox.Show("未找到车次，命令内容已保存至\n" + txtFile, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //保存成excel，判断有没有点取消
                    continueSave = saveData();
                    //返回OK，模型存入excel，回主界面，（主界面重新从excel读取一次数据）
                    //continuesave为2时，说明当日有相同命令，不添加txt
                    if(continueSave == 1 ||
                        continueSave == -1)
                    {
                        MessageBox.Show("保存成功，命令内容已存储至\n" + txtFile, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                //命令原文存txt
                try
                {
                    if(continueSave == 1 ||
                        continueSave == -1)
                    {
                        FileStream file = new FileStream(txtFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                        StreamWriter writer = new StreamWriter(file);
                        if (commandText.Trim().Length != 0)
                        {
                            writer.WriteLine("命令号：" + commandID + "\n\n" + commandText.Replace("\r", "\n"));
                        }
                        else
                        {
                            writer.WriteLine("命令号：" + commandID + "\n" + "该命令车次为人工添加，无命令内容，可双击打开粘贴命令内容");
                        }

                        writer.Close();
                        file.Close();
                    }
                }
                catch (Exception _e)
                {

                }
                Main mainForm = (Main)this.Owner;
                mainForm.RefreshUI();
                if(continueSave != 0)
                {
                    this.Close();
                }

            }
        }

        private void addCM_btn_Click_1(object sender, EventArgs e)
        {
            TrainModel _tm = new TrainModel();
            EditForm _ef = new EditForm(_tm, 0);
            _ef.Owner = this;
            _ef.Show();
        }

        private void deleteCM_btn_Click(object sender, EventArgs e)
        {
            if (analysisListView.SelectedItems.Count != 0)
            {
                TrainModel _tempTrainModel = getTrainAndDateTime(analysisListView.SelectedItems[0].SubItems[0].Text, analysisListView.SelectedItems[0].SubItems[1].Text);
                foreach(NormalCommandModel _cm in AllCommands)
                {
                    _cm.allTrainModel.Remove(_tempTrainModel);
                }

                RefreshData();
            }
        }
    }
}
