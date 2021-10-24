using CCWin;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TrainProjectAnalyse
{
    public partial class Main : Skin_Mac
    {
        //Excel内的命令
        List<NormalCommandModel> allCurrentModels;
        List<TrainModel> allCurrentTrainModels;
        //新添加的命令
        List<NormalCommandModel> allCommands;
        string commandText = "";
        //避免重复打开txt文件，只在变化时打开
        string currentTXTFileName = "";
        IWorkbook WorkBook;

        //时刻表Excel文件
        string ExcelFile;
        bool hasFilePath = false;

        public string highSpeedCommand;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            edit_btn.Enabled = false;
            RefreshUI();
            today_rb.Select();
            ExcelFile = "";
            dataAnalyse_btn.Enabled = false;
            highSpeedCommand = "";
        }

        //刷新内容
        public void RefreshUI()
        {

            refreshData();
            WorkBook = loadExcel();
            if (WorkBook != null)
            {
                loadData();
            }
            initList();
            RefreshCommandList(allCurrentModels,allCurrentTrainModels);

        }

        private void initList()
        {
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

            commandListView.View = View.Details;
            commandListView.ShowGroups = true;
            string[] commandListViewTitle = new string[] { "车次", "运行信息", "日期与命令" };
            for (int i = 0; i < commandListViewTitle.Length; i++)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = commandListViewTitle[i];   //设置列标题 
                if(i == 0 || i == 2)
                {
                    ch.Width = 90;
                }
                else if(i == 1)
                {
                    ch.Width = 70;
                }

                this.commandListView.Columns.Add(ch);    //将列头添加到ListView控件。
            }
        }

        //刷新总表
        private void RefreshCommandList(List<NormalCommandModel> _allCM,List<TrainModel> _allTM)
        {
            //附带的小组件清空
            DateListView.Items.Clear();
            OriginalText_rtb.Text = "";
            //先刷新总列表，以命令名为group名，group内为命令里的车
            commandListView.Items.Clear();
            commandListView.Groups.Clear();
            this.commandListView.BeginUpdate();
            bool hasTrains = true;
            //先倒着把分组加进去，展示的时候就是时间倒序的了
            for(int ij = _allCM.Count-1; ij >= 0; ij--)
            {
                //以"[命令名]-日期shortdate"作为组名，组内放车
                ListViewGroup _tempGroup = new ListViewGroup("[" + _allCM[ij].commandID + "]-" + _allCM[ij].createTime.ToString("yyyy/MM/dd"));
                this.commandListView.Groups.Add(_tempGroup);
            }
            for (int q = 0; q < _allCM.Count; q++)
            {
                for (int j = 0; j < _allTM.Count; j++)
                {
                    if (_allTM[j].commandID.Trim().Equals(_allCM[q].commandID) &&
                        _allTM[j].createTime.ToString("yyyy/MM/dd").Equals(_allCM[q].createTime.ToString("yyyy/MM/dd")))
                    {
                        TrainModel _tm = _allTM[j];
                        ListViewItem item = new ListViewItem();
                        string trainNum = _tm.firstTrainNum;
                        if (_tm.secondTrainNum.Trim().Length != 0)
                        {
                            trainNum = trainNum + "/" + _tm.secondTrainNum;
                        }
                        item.SubItems[0].Text = trainNum;
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
                        item.SubItems.Add("单击显示");
                        item.Group = commandListView.Groups[_allCM.Count - q -1];
                        commandListView.Items.Add(item);

                        //在此处把车和命令匹配在一起，变更任意一个都会变更两个项目
                        //如果allcm[q]没有车，再添加
                        if(_allCM[q].allTrainModel.Count == 0)
                        {
                            hasTrains = false;
                        }
                        if (!hasTrains)
                        {
                            _allCM[q].allTrainModel.Add(_allTM[j]);
                        }

                        //_tempGroup.Items.Add(item);

                    }
                }
            }
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高 
            commandListView.SmallImageList = imgList; //这里设置listView的SmallImageList ,用imgList将其撑大 
            this.commandListView.EndUpdate();
        }

        private void refreshData()
        {
            allCommands = new List<NormalCommandModel>();
            allCurrentModels = new List<NormalCommandModel>();
            allCurrentTrainModels = new List<TrainModel>();
        }


        private void loadData()
        {
            ISheet sheetCommands = WorkBook.GetSheet("Commands");
            ISheet sheetTrains = WorkBook.GetSheet("Trains");
            bool titleRow = true;
            foreach(IRow row in sheetCommands) 
            {
                //首行跳过
                if (titleRow)
                {
                    titleRow = false;
                    continue;
                }
                NormalCommandModel _cm = new NormalCommandModel();
                //如果全空，就不添加
                int nullCounter = 0;
                if (row.GetCell(0) != null)
                {
                    DateTime _tempDT = new DateTime();
                    string date = row.GetCell(0).ToString();
                    if(DateTime.TryParse(date, out _tempDT))
                    {
                        _cm.createTime = _tempDT;
                    }

                }
                else
                {
                    nullCounter += 1;
                    _cm.createTime = DateTime.Parse("0001/01/01");
                }
                if (row.GetCell(1) != null)
                {
                    _cm.commandID = row.GetCell(1).ToString();
                }
                else
                {
                    nullCounter += 1;
                    _cm.commandID = "";
                }
                if (row.GetCell(2) != null)
                {
                    _cm.fileName = row.GetCell(2).ToString();
                }
                else
                {
                    nullCounter += 1;
                    _cm.fileName = "";
                }
                if (row.GetCell(3) != null)
                {
                    _cm.ID = row.GetCell(3).ToString();
                }
                if (row.GetCell(4) != null)
                {
                    if (Regex.IsMatch(row.GetCell(4).ToString(), @"^[0-9]\d*$"))
                    {
                        _cm.TrainIDCount = int.Parse(row.GetCell(4).ToString());
                    }
                }
                //全空不添加
                if (nullCounter != 3)
                {
                    allCurrentModels.Add(_cm);
                }

            }
            titleRow = true;
            foreach (IRow row in sheetTrains)
            {
                //首行跳过
                if (titleRow)
                {
                    titleRow = false;
                    continue;
                }
                TrainModel _tm = new TrainModel();
                if (row.GetCell(0) != null)
                {
                    _tm.createTime = DateTime.Parse(row.GetCell(0).ToString());
                }
                else
                {
                    _tm.createTime = DateTime.Parse("0001/01/01");
                }
                if (row.GetCell(1) != null)
                {
                    _tm.commandID = row.GetCell(1).ToString();
                }
                else
                {
                    _tm.commandID = "";
                }
                if (row.GetCell(2) != null)
                {
                    _tm.placeInCommand = row.GetCell(2).ToString();
                }
                else
                {
                    _tm.placeInCommand = "";
                }
                if (row.GetCell(3) != null)
                {
                    _tm.firstTrainNum = row.GetCell(3).ToString();
                }
                else
                {
                    _tm.firstTrainNum = "";
                }
                if (row.GetCell(4) != null)
                {
                    _tm.secondTrainNum = row.GetCell(4).ToString();
                }
                else
                {
                    _tm.secondTrainNum = "";
                }
                if (row.GetCell(5) != null)
                {
                    _tm.streamStatus = int.Parse(row.GetCell(5).ToString());
                }
                else
                {
                    _tm.streamStatus = -1;
                }
                if (row.GetCell(6) != null)
                {
                    List<DateTime> _dt = new List<DateTime>();
                    string[] allDates = row.GetCell(6).ToString().Split(',');

                    foreach(string _date in allDates)
                    {
                        DateTime _tempDT = new DateTime();
                        if(DateTime.TryParse(_date, out _tempDT))
                        {
                            _dt.Add(_tempDT);
                        }

                    }
                    _tm.effectiveDates = _dt;
                }
                else
                {
                    _tm.effectiveDates = null;
                }
                if(row.GetCell(7) != null)
                {
                    _tm.ID = row.GetCell(7).ToString();
                }
                //有项目没有，不添加
                allCurrentTrainModels.Add(_tm);

            }
            int a = 0;
        }

        //获取总数据
        private IWorkbook loadExcel()
        {
            //读取文件
            IWorkbook workBook;
            string fileName = Application.StartupPath + "\\Data.xls";
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            try
            {
                workBook = new HSSFWorkbook(fileStream);  //xlsx数据读入workbook  
                if(workBook == null)
                {
                    MessageBox.Show("读取总数据时出现错误(Data.xls)\n" + fileName + "\n错误内容：" , "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                else
                {
                    return workBook;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("读取总数据时出现错误(Data.xls)\n" + fileName + "\n错误内容：" + e.ToString().Split('在')[0], "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private int searchAndHightlight(string find)
        {
            //type为0是长期命令，为1是高铁令
            RichTextBox targetRTB = new RichTextBox();
           targetRTB = OriginalText_rtb;
            int index = 0;
            index = targetRTB.Find(find, 0, RichTextBoxFinds.None);
            if (index == -1)
            {
                index = targetRTB.Find(find.ToUpper().Trim(), 0, RichTextBoxFinds.None);;
            }
            if (index == -1)
            {
                string textOnlyD = Regex.Replace(find, @"[^0-9]", "");
                index = targetRTB.Find(textOnlyD, 0, RichTextBoxFinds.None);
            }
            int startPos = index;
            int nextIndex = 0;
            while (nextIndex != startPos)//循环查找字符串，并用红色加粗12号Times New Roman标记之
            {
                {
                    if (index == -1)
                    {
                        break;
                    }
                    targetRTB.SelectionStart = index;
                    targetRTB.SelectionLength = find.Length;
                    targetRTB.SelectionColor = Color.OrangeRed;
                    targetRTB.SelectionFont = new Font("Times New Roman", (float)12, FontStyle.Bold);
                    targetRTB.Focus();
                    nextIndex = targetRTB.Find(find, index + find.Length, RichTextBoxFinds.None);
                    if (index == -1)
                    {
                        index = targetRTB.Find(find.ToUpper().Trim(), 0, RichTextBoxFinds.None);
                    }
                    if (index == -1)
                    {
                        index = targetRTB.Find(Regex.Replace(find, @"[^0-9]", ""), 0, RichTextBoxFinds.None);
                    }
                    if (nextIndex == -1)//若查到文件末尾，则重置nextIndex为初始位置的值，使其达到初始位置，顺利结束循环，否则会有异常。
                        nextIndex = startPos;
                    index = nextIndex;
                }
            }
            return -1;
        }

        //读取粘贴的命令
        private void getCommands(string commandText)
        {
            string commandID = "";
            string date = "";
            //去标题
            commandText = commandText.Replace("1.", "").Replace("2.", "").Replace("3.", "").Replace("4.", "").Replace("5.", "").Replace("6.", "");
            //去符号
            commandText = commandText.Replace("，", "、").Replace(",","、").Replace("。", "").Replace("（", "").Replace("）", "").Replace("：","").Replace("~","").Replace("～","").Replace("〜","").Replace("—","").Replace("–","");
            commandText = commandText.Replace(".", "").Replace("(", "").Replace(")", "").Replace(":", "");
            string[] splitedText =  commandText.Split(new[] { "--第" }, StringSplitOptions.None);
            List<string> splitedCommandText = new List<string>();
            for(int k = 0; k < splitedText.Length; k++)
            {
                splitedCommandText.Add(splitedText[k]);
            }
            //表格式
            if (splitedCommandText.Count < 2)
            {
                //内容删除，再重新进行添加
                splitedCommandText.Clear();
                //以\n数字\t进行切割，一位或者两位数字或者多位数字
                Regex.Split(Regex.Split(commandText, @"\n\d\t")[Regex.Split(commandText, @"\n\d\t").Length - 1], @"\n\d\d\t");
                string[] tempTexts;
                splitedText = Regex.Split(commandText, @"\n\d+\t");
                for(int k = 0; k < splitedText.Length; k++)
                {
                    splitedCommandText.Add(splitedText[k]);
                }
                //表格格式有号头和创建时间，添加上
                string title = splitedCommandText[0];
                if(title.Contains("第") && title.Contains("号"))
                {
                    commandID = title.Split('第')[1].Split('号')[0].Replace("\t","");
                }
                if(title.Contains("年") && title.Contains("月") && title.Contains("日"))
                {
                    Regex r = new Regex(@"\d+年\d+月\d+日");
                    bool ismatch = r.IsMatch(title);
                    MatchCollection mc = r.Matches(title);
                    string result = string.Empty;
                    for (int i = 0; i < mc.Count; i++)
                    {
                        result += mc[i];//匹配结果是完整的数字，此处可以不做拼接的
                    }
                    date = result;
                }
            }
            for(int sentence = 0; sentence < splitedCommandText.Count; sentence++)
            {
                splitedCommandText[sentence] = splitedCommandText[sentence].Replace("命令来源", "").Replace("命令外发", "").Replace("\n","").Replace("\t","");
            }
            //对每条命令进行从左至右读取
            List<NormalCommandModel> _allCM = new List<NormalCommandModel>();

            for (int count = 0; count < splitedCommandText.Count; count++)
            {
                NormalCommandModel _cm = new NormalCommandModel();
                _cm.commandID = commandID;
                DateTime _dtCreate = new DateTime();
                bool gotTime = false;
                if(DateTime.TryParse(date,out _dtCreate))
                {
                    gotTime = true;
                    _cm.createTime = _dtCreate;
                    _cm.ID = _dtCreate.ToString("yyyyMMdd");
                }

                //当出现“停运”，“恢复开行”时，将前面的全部存起来
                string currentYear = "";
                string currentMonth = "";
                string currentDay = "";
                string currentDateStr = "";
                //找到车之后，如果有新日期，则之前的存一个模型
                bool hasGotTrains = false;
                List<DateTime> currentDate_Datetime = new List<DateTime>();
                List<string> currentTrainNumber = new List<string>();
                //找到运行状态之前的模型添加到这里面
                List<TrainModel> _tempModels = new List<TrainModel>();
                int currentStatus = -1;
                //不含关键字，跳过
                if (!splitedCommandText[count].Contains("停运")&&
                    !splitedCommandText[count].Contains("停开") &&
                    !splitedCommandText[count].Contains("停") &&
                    !splitedCommandText[count].Contains("恢复") &&
                    !splitedCommandText[count].Contains("开行"))
                {
                    continue;
                }
                string appendedStr = "";
                Regex regChineseWord = new Regex(@"[\u4e00-\u9fa5]");
                char[] _commandChar = splitedCommandText[count].ToCharArray();
                for(int i = 0; i < _commandChar.Length; i++)
                {
                    //每过一个换行符如果上面没有任何有效内容，重置一次
                    if(appendedStr.IndexOf("\n") != -1)
                    {
                        appendedStr = "";
                    }
                    appendedStr = appendedStr + _commandChar[i];
                    //先找年月日
                    //不含“至”
                    //当出现新日期时，存一个旧日期模型，找到停运信息后，统一添加
                    //xx日至xx日
                    //xx至xx日
                    //xx-xx日
                    //xx日-xx日
                    string appendNextChar = "";
                    if (i + 1 < _commandChar.Length)
                    {
                        appendNextChar = appendedStr + _commandChar[i + 1].ToString().Replace(",", "、");
                    }
                    if (appendedStr.Contains("日") &&
                        !appendNextChar.Contains("日至") &&
                        !appendNextChar.Contains("日-") ||
                        Regex.IsMatch(appendNextChar,@"\d+[日][、]")||
                        Regex.IsMatch(appendNextChar, @"\d+[月]\d+[、]")||
                        Regex.IsMatch(appendNextChar, @"[、]\d+[、]"))
                    {//把找到的日期都存入，导入后清空，如果有“至”，则找结束日，中间的日期全部加上
                     //当已经存储了模型时，又发现了<首个>新的时间，则时间存储器清空
                        if (hasGotTrains)
                        {
                            currentDate_Datetime = new List<DateTime>();
                            hasGotTrains = false;
                        }
                        //没有月，设置为当前月份
                        string _date = "";
                        char[] _dateChar = appendedStr.ToCharArray();
                        for (int d = 0; d < _dateChar.Length; d++)
                        {
                            if (Regex.IsMatch(_dateChar[d].ToString(), @"^[0-9]+$") ||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[年]+$") ||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[月]+$") ||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[日]+$") ||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[至]+$")||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[-]+$"))
                            {
                                _date = _date + _dateChar[d].ToString();
                            }
                        }
                        //不含年月的，从上一个获取的年月中找，还没有的话再设定为当前年月
                        if (!_date.Contains("月"))
                        {
                            if(currentMonth.Length != 0)
                            {
                                _date = currentMonth + "月" + _date;
                            }
                            else
                            {
                                _date = DateTime.Now.ToString("MM") + "月" + _date;
                                currentMonth = DateTime.Now.ToString("MM");
                            }
                        }
                        else
                        {
                            if (_date.Contains("年"))
                            {
                                currentMonth = _date.Split('月')[0].Split('年')[1];
                            }
                            else
                            {
                                currentMonth = _date.Split('月')[0];
                            }

                        }
                        //如果没有年，则设置为当前年份,同时存入current
                        if (!_date.Contains("年"))
                        {
                            if(currentYear.Length != 0)
                            {
                                _date = currentYear + "年" + _date;
                            }
                            else 
                            {
                                _date = DateTime.Now.ToString("yyyy") + "年" + _date;
                                currentYear = DateTime.Now.ToString("yyyy");
                            }

                        }
                        else
                        {
                            currentYear = _date.Split('年')[0];
                        }
                        //存入临时数据
                        currentDateStr = _date;
                        DateTime _dt;
                        DateTime.TryParse(_date, out _dt);
                        if(_dt.Year > 1990)
                        {
                            currentDate_Datetime.Add(_dt);
                        }

                        int a1 = 0;
                        appendedStr = "";
                    }
                     if(appendNextChar.Contains("日至") ||
                        appendNextChar.Contains("日-") ||
                        Regex.IsMatch(appendNextChar, @"[日][-]") ||
                        Regex.IsMatch(appendNextChar, @"\d+-"))
                    {
                        //如果下一个字是“至”，则再找到结束日期
                        {//往后找到下一个“日”，填入strdate，如果没有下一个日期，则找到“次”后停止，但不添加至内容
                            //先重置日期
                            //
                            //当已经存储了模型时，又发现了<首个>新的时间，则时间存储器清空
                            if (hasGotTrains)
                            {
                                currentDate_Datetime = new List<DateTime>();
                                hasGotTrains = false;
                            }
                            
                            string _tempStop = "";
                            if (!appendedStr.Contains("日"))
                            {
                                appendedStr += "日";
                            }
                            while (!_commandChar[i+1].Equals('日') &&
                                !_commandChar[i+1].Equals('次'))
                            {
                                i = i + 1;
                                _tempStop = _tempStop + _commandChar[i];
                                if(i+1== _commandChar.Length)
                                {
                                    break;
                                }
                            }
                            if (!_tempStop.Contains("次"))
                            {
                                _tempStop = _tempStop + "日";
                            }
                            appendedStr = appendedStr + _tempStop;
                            string _date = "";
                            List<DateTime> _allDates = new List<DateTime>();

                            //没有月，设置为当前月份
                            if (!appendedStr.Contains("月"))
                            {
                                _date = DateTime.Now.ToString("MM") + "月" + _date;
                            }
                            //如果没有年，则设置为当前年份
                            if (!appendedStr.Contains("年"))
                            {
                                _date = DateTime.Now.ToString("yyyy") + "年" + _date;
                            }
                            //_date = regChineseWord.Replace(_date.Replace("-","").Replace("年", "-").Replace("月", "-").Replace("日", "").Replace("至", "~").Trim(), "");
                            char[] _dateChar = appendedStr.ToCharArray();
                            for (int d = 0; d < _dateChar.Length; d++)
                            {
                                if (Regex.IsMatch(_dateChar[d].ToString(), @"^[0-9]+$") ||
                                    Regex.IsMatch(_dateChar[d].ToString(), @"^[年]+$") ||
                                    Regex.IsMatch(_dateChar[d].ToString(), @"^[月]+$") ||
                                    Regex.IsMatch(_dateChar[d].ToString(), @"^[日]+$") ||
                                    Regex.IsMatch(_dateChar[d].ToString(), @"^[至]+$") ||
                                    _dateChar[d].ToString().Equals("-"))
                                {
                                    _date = _date + _dateChar[d].ToString();
                                }
                            }
                            //strDate.Add(_date);
                            //计算两个日期间的所有日期
                            _allDates = getAllThroughDate(_date);
                            foreach(DateTime _dt in _allDates)
                            {
                                currentDate_Datetime.Add(_dt);
                            }
                            
                            appendedStr = "";
                        }
                    }
                    //车次
                    if (appendedStr.Contains("次"))
                    {
                        string _train = "";
                        /*
                        _train = regChineseWord.Replace(_train, "").Trim();
                        */
                        char[] _trainNumChar = appendedStr.ToCharArray();
                        for(int c = 0; c < _trainNumChar.Length; c++)
                        {
                            if (Regex.IsMatch(_trainNumChar[c].ToString(), @"^[A-Za-z0-9]+$")||
                                Regex.IsMatch(_trainNumChar[c].ToString(), @"^[/]+$"))
                            {
                                _train = _train + _trainNumChar[c].ToString();
                            }
                        }
                        //strTrains.Add(_train);
                        currentTrainNumber.Add(_train);
                        //找到车次之后，添加一个时间-车次的模型，等出现停运标记后一起添加。
                        TrainModel _tempTM = new TrainModel();
                        _tempTM.effectiveDates = currentDate_Datetime;
                        string currentFirstTrainNumber, currentSecondTrainNumber = "";
                        currentFirstTrainNumber = _train.Split('/')[0];
                        _tempTM.firstTrainNum = currentFirstTrainNumber;
                        if (_train.Split('/').Length >= 2)
                        {
                            currentSecondTrainNumber = getSecondTrainNum(_train);
                            _tempTM.secondTrainNum = currentSecondTrainNumber;
                        }
                        //添加到模型
                        _tempModels.Add(_tempTM);
                        hasGotTrains = true;
                        appendedStr = "";
                    }
                    //停运状态
                    if(appendedStr.Contains("停运") ||
                    appendedStr.Contains("停开") ||
                    appendedStr.Contains("恢复运行") ||
                    appendedStr.Contains("开行"))
                    {
                        if(_tempModels.Count  == 0)
                        {
                            continue;
                        }
                        if (appendedStr.Contains("停"))
                        {
                            //Status.Add(0);
                            currentStatus = 0;
                        }
                        else if (appendedStr.Contains("恢复")||
                            appendedStr.Contains("开行"))
                        {
                            //Status.Add(1);
                            currentStatus = 1;
                        }
                        appendedStr = "";
                        //把前面添加的模型的停运状态都填上
                        //如果出现相同的车次，后面出现的合并一下
                        TrainModel _comparedModel = new TrainModel();
                        bool firstOne = true;
                        int allModelsCount = _tempModels.Count;
                        for(int k =0;k<allModelsCount;k++)
                        {
                            TrainModel _tm = _tempModels[k];
                            if (_tm.firstTrainNum.Equals(_comparedModel.firstTrainNum)||
                                _tm.firstTrainNum.Equals(_comparedModel.secondTrainNum))
                            {
                                foreach(DateTime _dt in _tm.effectiveDates)
                                {
                                    bool hasGotSameTime = false;
                                    foreach(DateTime _dtCompared in _comparedModel.effectiveDates)
                                    {
                                        if(_dtCompared.Year==_dt.Year&&
                                            _dtCompared.Month == _dt.Month &&
                                            _dtCompared.Day == _dt.Day)
                                        {
                                            hasGotSameTime = true;
                                            break;
                                        }
                                    }
                                    if (hasGotSameTime)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //没有相同时间，赋值
                                        _comparedModel.effectiveDates.Add(_dt);
                                    }
                                }
                                //时间赋值过去
                                //有相同的，不添加
                                _tempModels.Remove(_tm);
                                allModelsCount = allModelsCount - 1;
                            }
                            if (firstOne)
                            {
                                _comparedModel = _tm;
                                firstOne = false;
                            }
                            _tm.streamStatus = currentStatus;
                            //命令和创建日期也填上
                            _tm.commandID = commandID;
                            if (gotTime)
                            {
                                _tm.createTime = _dtCreate;
                                _tm.ID = _dtCreate.ToString("yyyyMMdd");
                            }
                        }
                        _cm.allTrainModel = _tempModels;
                        //车模型清空，临时储存器清空，其他不变
                        currentTrainNumber = new List<string>();
                        _tempModels = new List<TrainModel>();
                        _allCM.Add(_cm);
                        _cm = new NormalCommandModel();
                    }
                }
            }
            allCommands = _allCM;
            int a = 0;
        }

        //分割双车次
        private string getSecondTrainNum(string _trainNumber)
        {
            if(_trainNumber.Split('/').Length > 2)
            {
                _trainNumber = _trainNumber.Split('/')[0] +"/"+ _trainNumber.Split('/')[1];
            }
            string[] trainWithDoubleNumber = _trainNumber.Split('/');
            if(trainWithDoubleNumber.Length <2)
            {
                return _trainNumber;
            }
            char[] firstTrainWord = trainWithDoubleNumber[0].ToCharArray();
            string secondTrainWord = "";
            for (int q = 0; q < firstTrainWord.Length; q++)
            {
                if (q != firstTrainWord.Length - trainWithDoubleNumber[1].Length)
                {
                    secondTrainWord = secondTrainWord + firstTrainWord[q];
                }
                else
                {
                    secondTrainWord = secondTrainWord + trainWithDoubleNumber[1];
                    //添加第二个车次
                    break;
                }
            }
            //return _trainNumber.Split('/')[0] + "/" + secondTrainWord;
            return secondTrainWord;
        }

        //分割日期
        private List<DateTime> getAllThroughDate(string _date)
        {
            string startdate = "";
            string stopdate = "";
            try
            {
                if (_date.Contains("至"))
                {
                    startdate = _date.Split('至')[0];
                    stopdate = _date.Split('至')[1];
                }
                else if (_date.Contains("-"))
                {
                    startdate = _date.Split('-')[0];
                    stopdate = _date.Split('-')[1];
                }

                if (!startdate.Contains("年") ||
                    !startdate.Contains("月") ||
                    !startdate.Contains("日"))
                {
                    return new List<DateTime>();
                }
                if (stopdate.Contains("年"))
                {

                }
                else if (stopdate.Contains("月"))
                {
                    stopdate = startdate.Split('年')[0] + "年" + stopdate;
                }
                else if (stopdate.Contains("日"))
                {
                    stopdate = startdate.Split('月')[0] + "月" + stopdate;
                }
                List<DateTime> li = new List<DateTime>();
                DateTime time1 = Convert.ToDateTime(startdate);
                DateTime time2 = Convert.ToDateTime(stopdate);
                while (time1 <= time2 && time1.Year > 1990)
                {
                    li.Add(time1);
                    time1 = time1.AddDays(1);
                }
                return li;
            }
            catch(Exception e)
            {
                return new List<DateTime>();
            }

            return new List<DateTime>();
        }


        private void getCommands_HighSpeed(string commandText)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            {
                getCommands(command_rtb.Text);
                commandText = command_rtb.Text;
                if (allCommands.Count == 0)
                {
                    edit_btn.Enabled = false;
                }
                else
                {
                    edit_btn.Enabled = true;
                    //复制完后从最上面开始显示
                    this.command_rtb.Select(0, 0);
                    command_rtb.ScrollToCaret();
                    if (allCommands.Count != 0)
                    {
                        //最后参数为1则为编辑现有命令模式，为0为添加命令模式
                        AnalyseForm _af = new AnalyseForm(allCommands, commandText, WorkBook, 0);
                        _af.Owner = this;
                        _af.Show();
                    }
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(allCommands.Count != 0)
            {
                //最后参数为1则为编辑现有命令模式，为0为添加命令模式
                AnalyseForm _af = new AnalyseForm(allCommands, commandText, WorkBook,0);
                _af.Owner = this;
                _af.Show();
            }
        }

        //打开原文
        private void OpenOriginalCommand(string fileName)
        {
            try
            {
                if(fileName.Length != 0)
                {
                    System.Diagnostics.Process.Start("NotePad", fileName);
                    currentTXTFileName = fileName;
                }
            }
            catch (Exception e)
            {

            }

        }

        //找出命令原文
        private void ReadOriginalCommand(string fileName)
        {
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                if (!fileName.Equals(currentTXTFileName) || (OriginalText_rtb.Text.Length == 0))
                {
                    currentTXTFileName = fileName;
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string line = "";
                        line = line + sr.ReadLine();
                        OriginalText_rtb.Text = line;
                        string current;
                        // 从文件读取并显示行，直到文件的末尾
                        while ((current = sr.ReadLine()) != null)
                        {
                            line = line + current + "\n";
                            OriginalText_rtb.Text = line.Trim();
                        }
                    }
                }


            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                OriginalText_rtb.Text = "未找到命令原文";
            }
        }

        //从选中的项目中找对应的命令对象
        private NormalCommandModel  GetCommandFromSelect(string commandIDAndDate)
        {
            NormalCommandModel _tempCM = new NormalCommandModel();
            string commandID = commandIDAndDate.Split(']')[0].Replace("[","");
            string createDate = commandIDAndDate.Split('-')[1];
            //遍历日期和命令号都符合的命令
            foreach(NormalCommandModel _cm in allCurrentModels)
            {
                //找到了命令
                if(_cm.commandID.Equals(commandID) && _cm.createTime.ToString("yyyy/MM/dd").Equals(createDate))
                {
                    _tempCM = _cm;
                }
            }
            return _tempCM;
        }

        //从选中的项目中获取列车信息
        private TrainModel GetTrainFromSelect(NormalCommandModel _tempCM,string trainNumber)
        {
            //由于前期已经将车次和命令匹配了，所以只需要有命令就可以找到车次
            //注意车次需分开
            TrainModel _tempTM = new TrainModel();
            string firstTrainNum = "";
            string secondTrainNum = "";
            firstTrainNum = trainNumber.Split('/')[0];
            if (trainNumber.Contains("/"))
            {
                secondTrainNum = trainNumber.Split('/')[1];
            }
            foreach(TrainModel _tm in _tempCM.allTrainModel)
            {
                //找到了
                if(_tm.firstTrainNum.Equals(firstTrainNum)||
                    _tm.secondTrainNum.Equals(firstTrainNum)||
                    _tm.firstTrainNum.Equals(secondTrainNum)||
                    _tm.secondTrainNum.Equals(secondTrainNum))
                {
                    _tempTM = _tm;
                }
            }
            return _tempTM;
        }

        //填充时间
        private void RefreshDateList(TrainModel _tm)
        {
            DateListView.BeginUpdate();
            DateListView.Items.Clear();
            foreach (DateTime _dt in _tm.effectiveDates)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = _dt.ToLongDateString();
                DateListView.Items.Add(item);
            }
            DateListView.EndUpdate();
        }


        private void commandListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commandListView.SelectedItems.Count != 0)
            {
                //get命令的信息
                NormalCommandModel _tempCM = GetCommandFromSelect(commandListView.SelectedItems[0].Group.Header);
                ReadOriginalCommand(_tempCM.fileName);
                TrainModel _tempTM = GetTrainFromSelect(_tempCM,commandListView.SelectedItems[0].SubItems[0].Text);
                //填充时间
                RefreshDateList(_tempTM);
                //高亮显示
                searchAndHightlight(_tempTM.firstTrainNum);
                searchAndHightlight(_tempTM.secondTrainNum);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void addCommandManually_btn_Click(object sender, EventArgs e)
        {
            //最后参数为1则为编辑命令模式，为0为添加命令模式
            AnalyseForm _af = new AnalyseForm(new List<NormalCommandModel>(), commandText, WorkBook,0);
            _af.Owner = this;
            _af.Show();
        }

        private void OriginalText_rtb_TextChanged(object sender, EventArgs e)
        {

        }

        //双击打开原文
        private void OriginalText_rtb_DoubleClick(object sender, EventArgs e)
        {
            if (commandListView.SelectedItems.Count != 0)
            {
                //get命令的信息
                NormalCommandModel _tempCM = GetCommandFromSelect(commandListView.SelectedItems[0].Group.Header);
                OpenOriginalCommand(_tempCM.fileName);
            }
        }

        private void StartEditCommand()
        {
            if (commandListView.SelectedItems.Count != 0)
            {
                NormalCommandModel _tempCM = GetCommandFromSelect(commandListView.SelectedItems[0].Group.Header);
                List<NormalCommandModel> _listCM = new List<NormalCommandModel>();
                _listCM.Add(_tempCM);
                //最后参数为1则为编辑命令模式，为0为添加命令模式
                AnalyseForm _af = new AnalyseForm(_listCM, OriginalText_rtb.Text, WorkBook, 1);
                _af.Owner = this;
                _af.Show();
            }
        }

        //编辑命令
        private void EditSelectedCommand_btn_Click(object sender, EventArgs e)
        {
            StartEditCommand();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void commandListView_DoubleClick(object sender, EventArgs e)
        {
            StartEditCommand();
        }

        //搜索

        private void searchCommand_tb_TextChanged(object sender, EventArgs e)
        {
            if(searchCommand_tb.Text.Length != 0)
            {
                string text = searchCommand_tb.Text.ToUpper();
                //搜索命令，使用特定的模型刷新主表
                List<NormalCommandModel> _allCM = new List<NormalCommandModel>(0);
                List<TrainModel> _allTM = new List<TrainModel>();
                foreach (NormalCommandModel _cm in allCurrentModels)
                {
                    if (_cm.commandID.Contains(text.Trim())) 
                    {
                        _allCM.Add(_cm);
                        //把所有的车取出来，展示要用
                        foreach (TrainModel _tm in _cm.allTrainModel)
                        {
                            _allTM.Add(_tm);
                        }
                    }
                }
                RefreshCommandList(_allCM, _allTM);
            }
            else
            {
                RefreshCommandList(allCurrentModels, allCurrentTrainModels);
            }

        }

        private void searchTrain_tb_TextChanged(object sender, EventArgs e)
        {
            if (searchTrain_tb.Text.Length != 0)
            {
                string text = searchTrain_tb.Text.ToUpper();
                //搜索命令，使用特定的模型刷新主表
                List<NormalCommandModel> _allCM = new List<NormalCommandModel>(0);
                List<TrainModel> _allTM = new List<TrainModel>();
                foreach (NormalCommandModel _cm in allCurrentModels)
                {
                    {
                        //把所有的车取出来，展示要用
                        //同一个命令只添加一次就够了
                        bool hasGotCM = false;
                        foreach (TrainModel _tm in _cm.allTrainModel)
                        {
                            if(_tm.firstTrainNum.Contains(text) ||
                                _tm.secondTrainNum.Contains(text))
                            {
                                if(hasGotCM == false)
                                {
                                    _allCM.Add(_cm);
                                    hasGotCM = true;
                                }
                                _allTM.Add(_tm);
                            }

                        }
                    }
                }
                RefreshCommandList(_allCM, _allTM);
            }
            else
            {
                RefreshCommandList(allCurrentModels, allCurrentTrainModels);
            }
            //搜索车次
        }

        private void SelectPath()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
            openFileDialog1.Filter = "Excel 2003 文件 (*.xls)|*.xls";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                {
                    ExcelFile = "";
                    int fileCount = 0;
                    String fileNames = "已选择：";
                    foreach (string fileName in openFileDialog1.FileNames)
                    {
                        ExcelFile = fileName;
                        fileCount++;
                    }
                     this.TimeTableFileName_lbl.Text = "已选择：" + fileCount + "个文件";
                    hasFilePath = true;
                }
                dataAnalyse_btn.Enabled = true;
            }
        }

        //导入时刻表文件
        private void importTimeTable_btn_Click(object sender, EventArgs e)
        {
            SelectPath();
        }

        //分析列车运行情况
        private void dataAnalyse_btn_Click(object sender, EventArgs e)
        {
            //看选中的是今天还是明天，今天就传今天进去
            DateTime _dt = DateTime.Now.Date;
            if (tomorrow_rb.Checked)
            {
                _dt = DateTime.Today.AddDays(1).Date;
            }
            IWorkbook workbook = new HSSFWorkbook(new FileStream(ExcelFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite));
            ConfirmCommand _ccForm = new ConfirmCommand(workbook,_dt,allCurrentModels,highSpeedCommand);
            _ccForm.Owner = this;
            _ccForm.Show();
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
