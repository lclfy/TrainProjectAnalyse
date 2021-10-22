using CCWin;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TrainProjectAnalyse
{
    public partial class AnalyseForm : Skin_Mac
    {
        List<NormalCommandModel> AllCommands;
        string commandText = "";
        //以存储日期+号头确定唯一命令
        string commandID = "";
        string nowDate = DateTime.Now.ToString("yyyy/MM/dd");
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
            textBox1.Focus();
        }

        private void InitView()
        {
            //初始化界面与内容
            if(EditMode == 0)
            {
                deleteCommand_btn.Visible = false;
            }
            else
            {
                deleteCommand_btn.Visible = true;
            }
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
                //编辑模式填写命令号头
                if(q== 0)
                {
                    textBox1.Text = _cm.commandID;
                }
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
                                                    sameDateList = sameDateList + _dt.ToString("yyyy/MM/dd") + "\n";
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
                            row.CreateCell(7).SetCellValue(_tm.ID);
                        }
                    }
                    
                }
                _allCM[0].TrainIDCount = IDCounter;
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
                                if (previousDate.Equals(DateTime.Now.ToString("yyyy/MM/dd")))
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
                            ICell cellTrainCounter = row.CreateCell(4);
                            cellTrainCounter.SetCellValue(_allCM[0].TrainIDCount.ToString());
                        }
                    }
                }
  

                //重新修改文件指定单元格样式
                FileStream fs1 = File.OpenWrite(dataFileName);
                workbook.Write(fs1);
                fs1.Close();
                Console.ReadLine();
                //workbook.Close();
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
        //注意EditMode，若为0时为新建，为1时则需要立即编辑excel
        public void EditComplete(TrainModel _tm,int newOrEdit)
        {
            TrainModel trainModel = _tm;
            
            if (AllCommands.Count == 0)
            {
                NormalCommandModel _cm = new NormalCommandModel();
                _cm.createTime = DateTime.Now;
                _cm.commandID = commandID;
                AllCommands.Add(_cm);
            }
            int IDCounter = 1;
            if (EditMode == 0)
            {
                IDCounter = AllCommands[0].TrainIDCount;
                trainModel.ID += commandID + (IDCounter).ToString("D3");
                //用完之后记得+1
                AllCommands[0].TrainIDCount = IDCounter + 1;
            }
            {
                if (newOrEdit == 0)
                {
                    AllCommands[0].allTrainModel.Add(trainModel);
                }
                else if (newOrEdit == 1)
                {
                    for (int i = 0; i < AllCommands[0].allTrainModel.Count; i++)
                    {
                        //找到了，修改内容
                        if (AllCommands[0].allTrainModel[i].ID.Equals(_tm.ID))
                        {
                            TrainModel _oriTM = AllCommands[0].allTrainModel[i];
                            _oriTM.firstTrainNum = _tm.firstTrainNum;
                            _oriTM.secondTrainNum = _tm.secondTrainNum;
                            _oriTM.effectiveDates = _tm.effectiveDates;
                            _oriTM.streamStatus = _tm.streamStatus;
                            break;
                        }
                    }
                }
            }
            //如果是编辑命令的话，还需要立即修改Excel
            if(EditMode == 1)
            {
                //新建
                if(newOrEdit == 0)
                {

                    ISheet sheetCommand = WorkBook.GetSheet("Commands");

                    ISheet sheetTrain = WorkBook.GetSheet("Trains");
                    int lastRowTrain = sheetTrain.LastRowNum;
                    {
                        IRow row = sheetTrain.CreateRow(lastRowTrain + 1);
                        row.CreateCell(0).SetCellValue(_tm.createTime.ToString("yyyy/MM/dd"));
                        row.CreateCell(1).SetCellValue(commandID);
                        row.CreateCell(2).SetCellValue("0");
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
                else if(newOrEdit == 1)
                {
                    int trainIndex = GetTrainDataIndex(_tm);
                    ISheet _s = WorkBook.GetSheet("Trains");
                    {
                        if (_s.GetRow(trainIndex) != null)
                        {
                            IRow row = _s.GetRow(trainIndex);
                            //更新excel
                            if (row.GetCell(1) == null)
                            {
                                row.CreateCell(1);
                            }
                            if (row.GetCell(3) == null)
                            {
                                row.CreateCell(3);
                            }
                            if (row.GetCell(4) == null)
                            {
                                row.CreateCell(4);
                            }
                            if (row.GetCell(5) == null)
                            {
                                row.CreateCell(5);
                            }
                            if (row.GetCell(6) == null)
                            {
                                row.CreateCell(6);
                            }
                            row.GetCell(1).SetCellValue(commandID);
                            row.GetCell(3).SetCellValue(_tm.firstTrainNum);
                            row.GetCell(4).SetCellValue(_tm.secondTrainNum);
                            row.GetCell(5).SetCellValue(_tm.streamStatus);
                            //日期格式yyyy/MM/dd，以英文逗号区分
                            string dateStr = "";
                            foreach (DateTime _dt in _tm.effectiveDates)
                            {
                                dateStr += _dt.ToString("yyyy/MM/dd") + ",";
                            }
                            dateStr = dateStr.TrimEnd(',');
                            row.GetCell(6).SetCellValue(dateStr);


                            //重新修改文件指定单元格样式
                            FileStream fs1 = File.OpenWrite(dataFileName);
                            WorkBook.Write(fs1);
                            fs1.Close();
                        }

                    }
                    
                }

            }
            RefreshData();
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
            if(EditMode == 0)
            {
                if (textBox1.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入五位数字命令号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();
                }
                else if (!Regex.IsMatch(textBox1.Text.Trim(), @"^(\d{5})$"))
                {
                    MessageBox.Show("请输入五位数字命令号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        if (continueSave == 1 ||
                            continueSave == -1)
                        {
                            //MessageBox.Show("保存成功，命令内容已存储至\n" + txtFile, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    //命令原文存txt
                    try
                    {
                        if (continueSave == 1 ||
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
                    if (continueSave != 0)
                    {
                        this.Close();
                    }

                }
            }
            else if(EditMode == 1)
            {
                //改号头
                ISheet sheetCommand = WorkBook.GetSheet("Commands");
                ISheet sheetTrain = WorkBook.GetSheet("Trains");
                int _cmIndex = GetCommandDataIndex(AllCommands[0]);
                string newCommandID = "";
                if (_cmIndex != -1)
                {
                    sheetCommand.GetRow(_cmIndex).GetCell(1).SetCellValue(commandID);
                    //改ID
                    string originalID = AllCommands[0].ID;
                    newCommandID = originalID.Substring(0, 8) + commandID;
                    sheetCommand.GetRow(_cmIndex).GetCell(3).SetCellValue(newCommandID);
                    //把IDCounter也存进Excel
                    int IDCounter1 = AllCommands[0].TrainIDCount;
                    sheetCommand.GetRow(_cmIndex).GetCell(4).SetCellValue(IDCounter1.ToString());
                }

                //改所有车的号头
                foreach(NormalCommandModel _cm in AllCommands)
                {
                    foreach(TrainModel _tm in _cm.allTrainModel)
                    {
                        int _tmIndex = GetTrainDataIndex(_tm);
                        if(_tmIndex != -1)
                        {
                            sheetTrain.GetRow(_tmIndex).GetCell(1).SetCellValue(commandID);
                            try
                            {
                                string trainID = newCommandID + _tm.ID.Substring(13, 3);
                                sheetTrain.GetRow(_tmIndex).GetCell(7).SetCellValue(trainID);
                            }
                            catch(Exception e1)
                            {

                            }
                        }

                    }
                }
                //删除空行
                DeleteEmptyRow(0);
                DeleteEmptyRow(1);
                //重新修改文件指定单元格样式
                FileStream fs1 = File.OpenWrite(dataFileName);
                WorkBook.Write(fs1);
                fs1.Close();
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main mainForm = (Main)this.Owner;
                mainForm.RefreshUI();
                this.Close();
            }
        }

        private void addCM_btn_Click_1(object sender, EventArgs e)
        {
            TrainModel _tm = new TrainModel();
            EditForm _ef = new EditForm(_tm, 0);
            _ef.Owner = this;
            _ef.Show();
        }

        //删除列车，不是删除命令，删除命令在下面
        private void deleteCM_btn_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("确认删除吗？", "提醒", MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {
                if (analysisListView.SelectedItems.Count != 0)
                {
                    TrainModel _tempTrainModel = getTrainAndDateTime(analysisListView.SelectedItems[0].SubItems[0].Text, analysisListView.SelectedItems[0].SubItems[1].Text);
                    //编辑模式，删除excel内的列车
                    if (EditMode == 1)
                    {
                        int trainRowNum = GetTrainDataIndex(_tempTrainModel);
                        DelRow(WorkBook.GetSheet("Trains"), trainRowNum, 1, 1);
                    }
                    foreach (NormalCommandModel _cm in AllCommands)
                    {
                        _cm.allTrainModel.Remove(_tempTrainModel);
                    }

                    RefreshData();
                }
            }

        }

        //返回命令所在的行编号（用来修改删除）
        private int GetCommandDataIndex(NormalCommandModel _cm)
        {
            string ID = _cm.ID.Trim();
            IWorkbook workbook = WorkBook;
            ISheet sheetCommand = workbook.GetSheet("Commands");
            if(ID.Length != 0)
            {
                for(int i=0;i<=sheetCommand.LastRowNum;i++)
                {
                    IRow row = sheetCommand.GetRow(i);
                    if(row == null)
                    {
                        continue;
                    }
                    if (row.GetCell(3) == null)
                    {
                        continue;
                    }
                    if (row.GetCell(3).ToString().Length != 0)
                    {
                        if (row.GetCell(3).ToString().Trim().Equals(ID))
                        {
                            return i;
                        }
                    }
                }
            }
            else
            {
                return -1;
            }
            return -1;
        }

        //返回车所在的行编号（用来修改删除）
        //一次只能找一个，避免循环出错
        private int GetTrainDataIndex(TrainModel _tm)
        {
            string ID = _tm.ID;
            IWorkbook workbook = WorkBook;
            ISheet sheetTrain = workbook.GetSheet("Trains");
            if (ID.Length != 0)
            {
                for (int i = 0; i <= sheetTrain.LastRowNum; i++)
                {
                    IRow row = sheetTrain.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    if (row.GetCell(7) == null)
                    {
                        continue;
                    }
                    if (row.GetCell(7).ToString().Length != 0)
                    {
                        if (row.GetCell(7).ToString().Trim().Equals(ID))
                        {
                            return i;
                        }
                    }
                }
            }
            else
            {
                return -1;
            }
            return -1;
        }

        //删除空行
        private void DeleteEmptyRow(int type)
        {
            ISheet sheet = null;
            if(type == 0)
            {
                sheet = WorkBook.GetSheet("Commands");
            }
            else if(type == 1)
            {
                sheet = WorkBook.GetSheet("Trains");
            }
            if(sheet == null)
            {
                return;
            }
            //删除空行
            int lastRow = sheet.LastRowNum;
            for (int ij = 0; ij <= lastRow; ij++)
            {
                if(ij>sheet.LastRowNum - 1)
                {
                    break;
                }
                if (sheet.GetRow(ij) == null)
                {
                    sheet.ShiftRows(ij + 1, sheet.LastRowNum, -1);
                    ij = ij - 1;
                    lastRow = lastRow - 1;
                }
                else
                {
                    if (sheet.GetRow(ij).GetCell(0) == null)
                    {
                        sheet.ShiftRows(ij + 1, sheet.LastRowNum, -1);
                        ij = ij - 1;
                        lastRow = lastRow - 1;
                    }
                    else
                    {
                        if (sheet.GetRow(ij).GetCell(0).ToString().Trim().Length == 0)
                        {
                            sheet.ShiftRows(ij + 1, sheet.LastRowNum, -1);
                            ij = ij - 1;
                            lastRow = lastRow - 1;
                        }
                    }
                }

            }
            //避免标题被删除

            if (sheet.GetRow(0) == null)
            {
                sheet.CreateRow(0);
            }
            if (type == 0)
            {
                sheet.GetRow(0).GetCell(0).SetCellValue("添加日期");
                sheet.GetRow(0).GetCell(1).SetCellValue("命令号");
                sheet.GetRow(0).GetCell(2).SetCellValue("文件名");
                sheet.GetRow(0).GetCell(3).SetCellValue("编号");
                sheet.GetRow(0).GetCell(4).SetCellValue("TrainIDCounter");
            }
            else if (type == 1)
            {
                sheet.GetRow(0).GetCell(0).SetCellValue("添加日期");
                sheet.GetRow(0).GetCell(1).SetCellValue("命令号");
                sheet.GetRow(0).GetCell(2).SetCellValue("命令位置");
                sheet.GetRow(0).GetCell(3).SetCellValue("车次1");
                sheet.GetRow(0).GetCell(4).SetCellValue("车次2");
                sheet.GetRow(0).GetCell(5).SetCellValue("运行状态");
                sheet.GetRow(0).GetCell(6).SetCellValue("日期列表");
                sheet.GetRow(0).GetCell(7).SetCellValue("编号");
            }
        }

        /// NPOI删除行
        /// <param name="sheet">处理的sheet</param>
        /// <param name="startRow">从第几行开始（0开始）</param>
        /// <param name="delCount">共删除N行</param>
        /// type：0为命令，1为列车
        /// 最后一行删不掉，最后一行用remove
        public bool DelRow(ISheet sheet, int startRow, int delCount,int type)
        {
            //sheet.ShiftRows(startRow + 1, sheet.LastRowNum, -1, false, false);//删除一行（为负数只能为-1）
            try
            {
                //最后一行用remove
                if (startRow == sheet.LastRowNum)
                {
                    sheet.RemoveRow(sheet.GetRow(startRow));
                }
                for (int i = 0; i < delCount; i++)
                {
                    sheet.ShiftRows(startRow+1, sheet.LastRowNum, -1);
                }

                DeleteEmptyRow(type);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        //删除整条命令
        private void DeleteCommand()
        {
            DialogResult dr;
            dr = MessageBox.Show("确认删除号头为"+commandID+"的命令吗？", "提醒", MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Yes)
            {

                ISheet sheetCommand = WorkBook.GetSheet("Commands");
                ISheet sheetTrain = WorkBook.GetSheet("Trains");
                //找出命令所在的行
                foreach (NormalCommandModel _cm in AllCommands)
                {
                     int commandRowNum =  GetCommandDataIndex(_cm);
                    //删除这一行
                    if (commandRowNum == -1)
                    {
                        //没找到
                    }
                    else
                    {
                        DelRow(sheetCommand, commandRowNum, 1,0);
                    }

                    //找出所有的车所在的行
                    foreach(TrainModel _tm in _cm.allTrainModel)
                    {
                        int trainRowNum = GetTrainDataIndex(_tm);
                        if (trainRowNum == -1)
                        {
                            //没找到
                            continue;
                        }
                        else
                        {
                            DelRow(sheetTrain, trainRowNum, 1,1);
                        }

                    }

                }
                
            }
            //重新修改文件指定单元格样式
            FileStream fs1 = File.OpenWrite(dataFileName);
            WorkBook.Write(fs1);
            fs1.Close();
            Main mainForm = (Main)this.Owner;
            mainForm.RefreshUI();
            this.Close();
        }

        private void deleteCommand_btn_Click(object sender, EventArgs e)
        {
            DeleteCommand();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                粘贴ToolStripMenuItem.Enabled = true;
            }
            else
                粘贴ToolStripMenuItem.Enabled = false;

            ((RichTextBox)contextMenuStrip1.SourceControl).Paste();
            //command_rTb.Paste(); //粘贴
        }



        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)contextMenuStrip1.SourceControl).Clear();
            //command_rTb.Clear(); //清空
        }

        private void 复制toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }
    }
}
