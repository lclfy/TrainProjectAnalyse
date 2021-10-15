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
    public partial class Main : Skin_Mac
    {
        //Excel内的命令
        List<CommandModel> allCurrentModels;
        List<TrainModel> allCurrentTrainModels;
        //新添加的命令
        List<CommandModel> allCommands;
        string commandText = "";
        IWorkbook WorkBook;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshData();
            WorkBook = loadExcel();
            if(WorkBook != null)
            {
                loadData();
            }
        }

        private void refreshData()
        {
            allCommands = new List<CommandModel>();
            allCurrentModels = new List<CommandModel>();
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
                CommandModel _cm = new CommandModel();
                if (row.GetCell(0) != null)
                {
                    _cm.createTime = DateTime.Parse(row.GetCell(0).ToString());
                }
                else
                {
                    _cm.createTime = DateTime.Parse("0001/01/01");
                }
                if (row.GetCell(1) != null)
                {
                    _cm.commandID = row.GetCell(1).ToString();
                }
                else
                {
                    _cm.commandID = "";
                }
                if (row.GetCell(2) != null)
                {
                    _cm.fileName = row.GetCell(2).ToString();
                }
                else
                {
                    _cm.fileName = "";
                }
                allCurrentModels.Add(_cm);
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
                        _dt.Add(DateTime.Parse(_date));
                    }
                    _tm.effectiveDates = _dt;
                }
                else
                {
                    _tm.effectiveDates = null;
                }
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

        //读取粘贴的命令
        private void getCommands(string commandText)
        {
            //去标题
            commandText = commandText.Replace("1.", "").Replace("2.", "").Replace("3.", "").Replace("4.", "").Replace("5.", "").Replace("6.", "");
            //去符号
            commandText = commandText.Replace("，", "").Replace("。", "").Replace("、", "").Replace("（", "").Replace("）", "").Replace("：","").Replace("~","").Replace("～","").Replace("〜","").Replace("—","").Replace("–","");
            commandText = commandText.Replace(",", "").Replace(".", "").Replace("(", "").Replace(")", "").Replace(":", "");
            string[] splitedCommandText = commandText.Split(new[] { "--第" }, StringSplitOptions.None);
            for(int sentence = 0; sentence < splitedCommandText.Length; sentence++)
            {
                splitedCommandText[sentence] = splitedCommandText[sentence].Replace("命令来源", "").Replace("命令外发", "");
            }
            //对每条命令进行从左至右读取
            List<CommandModel> _allCM = new List<CommandModel>();

            for (int count = 0; count < splitedCommandText.Length; count++)
            {
                CommandModel _cm = new CommandModel();
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
                    if (appendedStr.Contains("日") && i+1 < _commandChar.Length && !_commandChar[i+1].ToString().Contains("至"))
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
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[至]+$"))
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
                     else if(appendedStr.Contains("日") && i + 1 < _commandChar.Length && _commandChar[i + 1].ToString().Contains("至"))
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
                            while (!_commandChar[i+1].Equals('日') &&
                                !_commandChar[i+1].Equals('次'))
                            {
                                i = i + 1;
                                _tempStop = _tempStop + _commandChar[i];
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
                                    Regex.IsMatch(_dateChar[d].ToString(), @"^[至]+$"))
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
                        foreach(TrainModel _tm in _tempModels)
                        {
                            _tm.streamStatus = currentStatus;
                        }
                        _cm.allTrainModel = _tempModels;
                        //车模型清空，临时储存器清空，其他不变
                        currentTrainNumber = new List<string>();
                        _tempModels = new List<TrainModel>();
                        _allCM.Add(_cm);
                        _cm = new CommandModel();
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
            startdate = _date.Split('至')[0];
            stopdate = _date.Split('至')[1];
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
                stopdate =  startdate.Split('年')[0] + "年" + stopdate;
            }
            else if (stopdate.Contains("日"))
            {
                stopdate = startdate.Split('月')[0] + "月" + stopdate;
            }
            List<DateTime> li = new List<DateTime>();
            DateTime time1 = Convert.ToDateTime(startdate);
            DateTime time2 = Convert.ToDateTime(stopdate);
            while (time1 <= time2&& time1.Year >1990)
            {
                li.Add(time1);
                time1 = time1.AddDays(1);
            }
            return li;
        }


        private void getCommands_HighSpeed(string commandText)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //区分是不是高铁命令
            bool isHighSpeedCommand = false;

            if (isHighSpeedCommand)
            {
                getCommands_HighSpeed(richTextBox1.Text);
            }
            else
            {
                getCommands(richTextBox1.Text);
                commandText = richTextBox1.Text;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnalyseForm _af = new AnalyseForm(allCommands,commandText,WorkBook);
            _af.Owner = this;
            _af.Show();
        }
    }
}
