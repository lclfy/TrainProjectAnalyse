using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CCWin;

namespace TrainProjectAnalyse
{
    public partial class Main : Skin_Mac
    {
        List<CommandModel> allCommands;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            allCommands = new List<CommandModel>();
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
                //List<string> strDate = new List<string>();
                //List<string> strTrains = new List<string>();
                //List<int> Status = new List<int>();
                //当出现“停运”，“恢复开行”时，将前面的全部存起来
                string currentDate = "";
                List<string> currentTrainNumberAndDate = new List<string>();
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
                    if(appendedStr.Contains("年") &&
                        appendedStr.Contains("月") &&
                        appendedStr.Contains("日") &&
                        appendedStr.Contains("至"))
                    {//往后找到下一个“日”，填入strdate
                        while (!_commandChar[i].Equals('日'))
                        {
                            i = i + 1;
                            appendedStr = appendedStr + _commandChar[i];
                        }
                        string _date = "";
                        //_date = regChineseWord.Replace(_date.Replace("-","").Replace("年", "-").Replace("月", "-").Replace("日", "").Replace("至", "~").Trim(), "");
                        char[] _dateChar = appendedStr.ToCharArray();
                        for (int d = 0; d < _dateChar.Length; d++)
                        {
                            if (Regex.IsMatch(_dateChar[d].ToString(), @"^[0-9]+$") ||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[年]+$")||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[月]+$")||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[日]+$")||
                                Regex.IsMatch(_dateChar[d].ToString(), @"^[至]+$"))
                            {
                                _date = _date + _dateChar[d].ToString();
                            }
                        }
                        //strDate.Add(_date);
                        //currentStartDate = _date.Split('至')[0];
                        //currentStopDate = getStopDate(_date);
                        currentDate = _date;
                        appendedStr = "";
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
                        currentTrainNumberAndDate.Add(currentDate + "-" +_train);
                        /*

                        */
                        appendedStr = "";
                    }
                    //停运状态
                    if(appendedStr.Contains("停运") ||
                    appendedStr.Contains("停开") ||
                    appendedStr.Contains("恢复") ||
                    appendedStr.Contains("开行"))
                    {
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
                        //此时添加一个模型
                        for(int m = 0; m < currentTrainNumberAndDate.Count; m++)
                        {
                            TrainModel _tm = new TrainModel();
                            //日期是在车次信息中的
                            string currentStartDate = currentTrainNumberAndDate[m].Split('-')[0].Split('至')[0];
                            string currentStopDate = getStopDate(currentTrainNumberAndDate[m].Split('-')[0]);
                            string currentTrainNumber = currentTrainNumberAndDate[m].Split('-')[1];
                            DateTime _dt;
                            if(DateTime.TryParse(currentStartDate, out _dt))
                            {
                                _tm.startDate = _dt;
                            }
                            if(DateTime.TryParse(currentStopDate, out _dt))
                            {
                                _tm.stopDate = _dt;
                            }
                            string currentFirstTrainNumber, currentSecondTrainNumber = "";
                            currentFirstTrainNumber = currentTrainNumber.Split('/')[0];
                            _tm.firstTrainNum = currentFirstTrainNumber;
                            if (currentTrainNumber.Split('/').Length == 2)
                            {
                                currentSecondTrainNumber = getSecondTrainNum(currentTrainNumber);
                                _tm.secondTrainNum = currentSecondTrainNumber;
                            }
                            _tm.streamStatus = currentStatus;
                            _cm.allTrainModel.Add(_tm);
                        }
                        //车模型清空，其他不变
                        currentTrainNumberAndDate = new List<string>();
                    }
                }
                _allCM.Add(_cm);
            }

            int a = 0;
            //Regex regTrain = new Regex(@"\(([^)]*)\)");
            //Regex hasAlphabetAndNumber = Regex.IsMatch(appendedStr, "/^(?![^0-9]+$)(?![^a-zA-Z]+$).+$/");
        }

        //分割双车次
        private string getSecondTrainNum(string _trainNumber)
        {
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
        private string getStopDate(string _date)
        {
            string startdate = "";
            string stopdate = "";
            startdate = _date.Split('至')[0];
            stopdate = _date.Split('至')[1];
            if (!startdate.Contains("年") ||
                !startdate.Contains("月") ||
                !startdate.Contains("日"))
            {
                return "";
            }
            if (stopdate.Contains("年"))
            {
                return stopdate;
            }
            else if (stopdate.Contains("月"))
            {
                return startdate.Split('年')[0] + "年" + stopdate;
            }
            else if (stopdate.Contains("日"))
            {
                return startdate.Split('月')[0] + "月" + stopdate;
            }
            return stopdate; 
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
            }

        }
    }
}
