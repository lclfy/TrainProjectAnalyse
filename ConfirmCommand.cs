using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CCWin;
using NPOI.SS.UserModel;

namespace TrainProjectAnalyse
{
    public partial class ConfirmCommand : Skin_Mac
    {
        IWorkbook WorkBook;

        DateTime date;

        List<NormalCommandModel> allCommandModel;
        //高铁命令
        List<TrainModel> allHighSpeedCommandModel;

        List<TimeTableModel> timeTable;
        //搜索出的
        List<TimeTableModel> searchedTableModel;

        //判断txt是否重复
        string currentTXTFileName;
        string commandText;
        bool hasText;
        string addedTrainText;

        //选中项目的ID
        string selectedID;

        public ConfirmCommand(IWorkbook _workbook,DateTime _dt,List<NormalCommandModel> _ncm)
        {
            WorkBook = _workbook;
            date = _dt;
            allCommandModel = _ncm;
            InitializeComponent();
        }

        public void RefreshData()
        {
            commandText = "";
            addedTrainText = "";
            timeTableDate_dtp.Value = date;
            timeTable = new List<TimeTableModel>();
            allHighSpeedCommandModel = new List<TrainModel>();
            selectedID = "";
            searchedTableModel = new List<TimeTableModel>();
        }
        private void ConfirmCommand_Load(object sender, EventArgs e)
        {
            RefreshData();
            initList();
            if (WorkBook != null)
            {
                readTimeTable(WorkBook);
                matchTimeTableAndCommand(false);
            }
            else
            {
                MessageBox.Show("时刻表文件未读取成功，请检查是否导入了正确的文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void initList()
        {
            mainList.View = View.Details;
            mainList.FullRowSelect = true;
            string[] commandListViewTitle = new string[] { "序号", "车次", "运行状态", "关系命令", "运行区段", "到时", "发时", "股道" };
            for (int i = 0; i < commandListViewTitle.Length; i++)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = commandListViewTitle[i];   //设置列标题 
                if (i == 0 || i == 7)
                {
                    ch.Width = 40;
                }
                else if(i== 1)
                {
                    ch.Width = 100;
                }
                else if ( i == 5 || i == 6)
                {
                    ch.Width = 50;
                }
                else if (i==3 || i == 4)
                {
                    ch.Width = 115;
                }
                else
                {
                    ch.Width = 70;
                }
                this.mainList.Columns.Add(ch);    //将列头添加到ListView控件。
            }
        }
        private void analyseCommand(string detectedTrainRow = "")
        {   //分析客调命令
            //删除不需要的标点符号-字符
            int addedTrainCount = 0;
            //try
            {
                string wrongNumber = "";
                List<string> _commands = removeUnuseableWord();
                String[] AllCommand;
                if (detectedTrainRow.Length == 0)
                {//不是抽样调查
                    //所有\n前面加上句号
                    string testStr = _commands[0];
                    testStr = testStr.Replace('\n', '。').Replace("。。", "。"); ;
                    AllCommand = testStr.Split('。');
                }
                else
                {
                    //string[] mf3={"c","c++","c#"};
                    AllCommand = new string[1];
                    AllCommand[0] = detectedTrainRow;
                }
                List<TrainModel> AllModels = new List<TrainModel>();
                addedTrainText = "";
                for (int i = 0; i < AllCommand.Length; i++)
                {
                    if (AllCommand[i].Contains("站") &&
                        AllCommand[i].Contains("开") && (
                        AllCommand[i].Contains("001") ||
                        AllCommand[i].Contains("002") ||
                        AllCommand[i].Contains("003") ||
                        AllCommand[i].Contains("004") ||
                        AllCommand[i].Contains("005") ||
                        AllCommand[i].Contains("006") ||
                        AllCommand[i].Contains("007") ||
                        AllCommand[i].Contains("008") ||
                        AllCommand[i].Contains("009")))
                    {//加开车次，单独存储
                        string addedCommand = AllCommand[i];
                        if (addedCommand.Contains("月") && addedCommand.Contains("日"))
                        {
                            addedTrainCount++;
                            if ((addedCommand.Contains("郑州东") && addedCommand.Contains("徐州")) ||
                                (addedCommand.Contains("郑州东") && addedCommand.Contains("商丘")))
                            {
                                try
                                {
                                    addedTrainText = addedTrainText + addedTrainCount + "、" + addedCommand.Split('：')[addedCommand.Split('：').Length - 1].Remove(0, 2) + "。\n";
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                    //取行号，便于查找
                    string index = AllCommand[i].Split('、')[0].Trim().Replace("\n", "");
                    String[] command;
                    String[] AllTrainNumberInOneRaw;
                    string trainModel = "null";
                    int streamStatus = 1;
                    //用于某些情况下标记不正常车次避免重复添加
                    Boolean isNormal = true;
                    int trainType = 0;
                    command = AllCommand[i].Split('：');
                    if (command.Length > 1)
                    {//非常规情况找车次
                        if (!command[1].Contains("G") &&
                        !command[1].Contains("D") &&
                        !command[1].Contains("C") &&
                        !command[1].Contains("J"))
                        {                //特殊数据
                                         //304、2018年02月11日，null-G4326/7：18：50分出库11日当天请令：临客线-G4326/7。
                                         //305、2018年02月11日，null - G4328 / 5：18：50分出库11日当天请令：临客线-G4328/5。
                            for (int r = 0; r < command.Length; r++)
                            {//从后往前开始找车次
                                if (command[command.Length - r - 1].Contains("G") ||
                                    command[command.Length - r - 1].Contains("D") ||
                                    command[command.Length - r - 1].Contains("C") ||
                                    command[command.Length - r - 1].Contains("J"))
                                {//找到了就用该项作为车次
                                    command[1] = command[command.Length - r - 1];
                                    break;
                                }
                            }
                        }
                        if (command[1].Contains("，"))
                        {//有逗号-逗号换横杠
                            command[1] = command[1].Replace('，', '-');
                        }
                        if (command[1].Contains("高峰"))
                        {
                            trainType = 1;
                        }
                        else if (command[1].Contains("临客"))
                        {
                            trainType = 2;
                        }
                        else if (command[1].Contains("周末"))
                        {
                            trainType = 3;
                        }
                        else if (command[1].Contains("加开"))
                        {
                            trainType = 4;
                        }

                        for (int timeCount = 0; timeCount < command.Length; timeCount++)
                        {
                            if (command[timeCount].Contains("CR"))
                            {
                                for (int word = 0; word < command[timeCount].Split('，').Length; word++)
                                {
                                    if (command[timeCount].Split('，')[word].Contains("CR") ||
                                        command[timeCount].Split('，')[word].Contains("cr"))
                                    {
                                        trainModel = command[timeCount].Split('，')[word];
                                    }
                                }

                            }
                        }


                        //找停运标记-特殊标记则直接加入模型
                        for (int n = 0; n < command.Length; n++)
                        {//从后往前开始找停运状态
                            if ((command[command.Length - n - 1].Contains("停运") &&
                                !command[command.Length - n - 1].Contains("G") &&
                                !command[command.Length - n - 1].Contains("D") &&
                                !command[command.Length - n - 1].Contains("C") &&
                                !command[command.Length - n - 1].Contains("J") &&
                                !command[command.Length - n - 1].Contains("00")) ||
                                 (command.Length > 2 && command[command.Length - n - 1].Contains("停运）")))
                            {//如果有-则继续判断是否全部停运
                             //特殊情况-部分停运，但停运部分使用括号标记
                             //76、2018年02月15日，CRH380AL-2590：DJ5732-G2001-(G662-G669：停运)。
                             //221、2018年02月22日，CRH380AL-2600：【0J5901-DJ5902-G6718(石家庄～北京西):停运】，0G4909-G4910-G801/4-G6611-G1559/8-G807-0G808。
                                if (command[command.Length - n - 1].Contains("停运）"))
                                {
                                    if (command[command.Length - n - 1].Contains("G") ||
                                        command[command.Length - n - 1].Contains("D") ||
                                        command[command.Length - n - 1].Contains("C") ||
                                        command[command.Length - n - 1].Contains("J") ||
                                        command[command.Length - n - 1].Contains("0"))
                                    {//如果停运标记后面还有车的话
                                        List<TrainModel> tempModels = trainModelAddFunc(Regex.Replace(command[command.Length - n - 1], @"[\u4e00-\u9fa5]", "").Replace('）', ' ').Replace('，', ' ').Split('-'), 1, trainType, trainModel, index);
                                        foreach (TrainModel model in tempModels)
                                        {
                                            if (!model.firstTrainNum.Contains("未识别"))
                                            {
                                                AllModels.Add(model);
                                            }
                                            else
                                            {
                                                wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                            }
                                        }
                                    }
                                    isNormal = false;
                                    AllTrainNumberInOneRaw = command[1].Split('-');
                                    //寻找车次中的括号左半部分
                                    //从前往后找，找到标记后的车次为停开
                                    bool stopped = false;
                                    for (int m = 0; m < AllTrainNumberInOneRaw.Length; m++)
                                    {
                                        if (AllTrainNumberInOneRaw[m].Contains("（G") ||
                                            AllTrainNumberInOneRaw[m].Contains("（D") ||
                                            AllTrainNumberInOneRaw[m].Contains("（C") ||
                                            AllTrainNumberInOneRaw[m].Contains("（J") ||
                                            AllTrainNumberInOneRaw[m].Contains("（0"))
                                        {//找到标记
                                            stopped = true;
                                        }
                                        //停开与开行分开进行建模
                                        if (stopped == true)
                                        {//不开
                                            List<TrainModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[m], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 0, trainType, trainModel, index);
                                            foreach (TrainModel model in tempModels)
                                            {
                                                if (!model.firstTrainNum.Contains("未识别"))
                                                {
                                                    AllModels.Add(model);
                                                }
                                                else
                                                {
                                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                                }
                                            }
                                        }
                                        else if (stopped == false)
                                        {//开
                                            List<TrainModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[m], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 1, trainType, trainModel, index);
                                            foreach (TrainModel model in tempModels)
                                            {
                                                if (!model.firstTrainNum.Contains("未识别"))
                                                {
                                                    AllModels.Add(model);
                                                }
                                                else
                                                {
                                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //正常情况-则默认所有车次停开
                                    streamStatus = 0;
                                }
                            }
                            break;
                        }
                        //判断某车底中仅停运一部分，且停运标记在车次中的特殊停运车次
                        //示例：236、2018年02月12日，CRH380AL-2607：0D5699(停运)-D5700(停运)-0G75-G75(郑州东始发)。
                        if (command[1].Contains("停"))
                        {
                            AllTrainNumberInOneRaw = command[1].Split('-');
                            //如果部分停开-则停开与开行分开进行建模
                            for (int h = 0; h < AllTrainNumberInOneRaw.Length; h++)
                            {
                                if (AllTrainNumberInOneRaw[h].Contains("停"))
                                {//去中文添加-由于部分情况下无法辨认小括号-因此必须在此处去除小括号
                                    List<TrainModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 0, trainType, trainModel, index);
                                    foreach (TrainModel model in tempModels)
                                    {
                                        if (!model.firstTrainNum.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                        }
                                    }
                                }
                                else
                                {
                                    List<TrainModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 1, trainType, trainModel, index);
                                    foreach (TrainModel model in tempModels)
                                    {
                                        if (!model.firstTrainNum.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                        }
                                    }
                                }
                            }
                        }
                        else if (command[1].Contains("次日"))
                        {

                            AllTrainNumberInOneRaw = command[1].Split('-');
                            //同理-部分次日-则次日与当日分开进行建模
                            for (int h = 0; h < AllTrainNumberInOneRaw.Length; h++)
                            {
                                if (AllTrainNumberInOneRaw[h].Contains("次日"))
                                {//去中文添加-由于部分情况下无法辨认小括号-因此必须在此处去除小括号
                                    List<TrainModel> tempModels;
                                    if (streamStatus != 0)
                                    {
                                        tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), 2, trainType, trainModel, index);
                                    }
                                    else
                                    {
                                        tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), streamStatus, trainType, trainModel, index);
                                    }
                                    foreach (TrainModel model in tempModels)
                                    {
                                        if (!model.firstTrainNum.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                        }
                                    }
                                }
                                else
                                {
                                    List<TrainModel> tempModels = trainModelAddFunc(Regex.Replace(AllTrainNumberInOneRaw[h], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-'), streamStatus, trainType, trainModel, index);
                                    foreach (TrainModel model in tempModels)
                                    {
                                        if (!model.firstTrainNum.Contains("未识别"))
                                        {
                                            AllModels.Add(model);
                                        }
                                        else
                                        {
                                            wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                        }
                                    }
                                }
                            }
                        }
                        else if (command[1].Contains("站") ||
                            (command[1].Contains("道") ||
                            command[1].Contains("到") ||
                            command[1].Contains("开")))
                        {//221、2018年03月20日，CRH380AL-2619：0J5901-DJ5902-G6718(石家庄～北京西)-G801/4（商丘站变更为26道）-0093(商丘站14:25开，郑州东徐兰场15:20到)-0094(郑州东徐兰场16:05开，郑州东动车所16.25到)。
                         //101、2018年03月20日，CRH380B-3763+3758：G1922/19（商丘站变更为27道）。
                         //把车次单独分离-去中文-去横杠-去括号内数字-在此处去除小括号
                         //去括号内数字方法-把括号前半部分换成空格，会变成G801/4 26，G1922/19 27
                         //识别时取空格前数字即可
                         //对于命令中含有时间的，Regex.Replace(X:XX && XX:XX)即可去除
                            AllTrainNumberInOneRaw = Regex.Replace(command[1], @"[\u4e00-\u9fa5]", "").Replace("（", " ").Replace("）", "").Split('-');
                            //把车次添加模型
                            List<TrainModel> tempModels = trainModelAddFunc(AllTrainNumberInOneRaw, streamStatus, trainType, trainModel, index);
                            foreach (TrainModel model in tempModels)
                            {
                                if (!model.firstTrainNum.Contains("未识别"))
                                {
                                    AllModels.Add(model);
                                }
                                else
                                {
                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                }
                            }
                        }
                        else if (isNormal)
                        {//如果一切正常 则
                         //把车次单独分离-去中文-去横杠-由于部分情况下无法辨认小括号-因此必须在此处去除小括号
                            AllTrainNumberInOneRaw = Regex.Replace(command[1], @"[\u4e00-\u9fa5]", "").Replace("（", "").Replace("）", "").Split('-');
                            //把车次添加模型
                            List<TrainModel> tempModels = trainModelAddFunc(AllTrainNumberInOneRaw, streamStatus, trainType, trainModel, index);
                            foreach (TrainModel model in tempModels)
                            {
                                if (!model.firstTrainNum.Contains("未识别"))
                                {
                                    AllModels.Add(model);
                                }
                                else
                                {
                                    wrongNumber = wrongNumber + "第" + index + "行" + "-" + model.firstTrainNum + "\r\n";
                                }
                            }
                        }
                    }
                }
                //高峰车次精确查找 20200110
                int rushHourTrain = 0;
                int tempTrain = 0;
                int weekendTrain = 0;
                int addedTrain = 0;
                if (AllModels != null && AllModels.Count > 0)
                {
                    //重新遍历命令，先选出之前标注为高峰临客的车逐一寻找并确定
                    for (int cmCount = 0; cmCount < AllModels.Count; cmCount++)
                    {
                        TrainModel _tempCM = AllModels[cmCount];
                        if (_tempCM.trainType == 0)
                        {
                            continue;
                        }
                        //找命令
                        for (int ij = 0; ij < AllCommand.Length; ij++)
                        {
                            bool hasGotIt = false;
                            string[] command;
                            /*
                            243、2018年10月27日，CRH380AL-2595：G74-G9782(高峰线)-G9781(高峰线)-0G9782(高峰线)，0G74(停运)。
                             252、2019年06月01日，CRH380AL - 2595：G74 - 0G74(停运) - G9196(高峰线) - G9195(高峰线) - 0G9196(高峰线)
                            303、2019年06月01日，CRH380B - 5754：高峰线 - G4292 - G4291。
                            311、2019年07月10日，CRH380AL-2606：高峰线-0G4567-G4568-G4567-0G4568。
                            296、2019年07月10日，CRH380A-2664+2705：周末线0G9201(高峰线)-G9202(高峰线)-0G6695(停运)-G6695-G6696-G6697-G6698-G6699-G6700-0G6700。
                             296、2019年07月10日，CRH380A-2664+2705：0G9201(高峰线)-G9202(高峰线)-0G6695(停运)-G6695-G6696-G6697-G6698-G6699-G6700(高峰线)-0G6700。
                            */
                            //先切块
                            command = AllCommand[ij].Split('：');
                            if (command.Length > 1)
                            {//非常规情况找车次
                                if (!command[1].Contains("G") &&
                                !command[1].Contains("D") &&
                                !command[1].Contains("C") &&
                                !command[1].Contains("J"))
                                {                //特殊数据
                                                 //304、2018年02月11日，null-G4326/7：18：50分出库11日当天请令：临客线-G4326/7。
                                                 //305、2018年02月11日，null - G4328 / 5：18：50分出库11日当天请令：临客线-G4328/5。
                                    for (int r = 0; r < command.Length; r++)
                                    {//从后往前开始找车次
                                        if (command[command.Length - r - 1].Contains("G") ||
                                            command[command.Length - r - 1].Contains("D") ||
                                            command[command.Length - r - 1].Contains("C") ||
                                            command[command.Length - r - 1].Contains("J"))
                                        {//找到了就用该项作为车次
                                            command[1] = command[command.Length - r - 1];
                                            break;
                                        }
                                    }
                                }
                                if (command[1].Contains("，"))
                                {//有逗号-逗号换横杠
                                    command[1] = command[1].Replace('，', '-');
                                }
                                command[1] = command[1].Trim();
                                if (command[1].Contains(_tempCM.firstTrainNum) || command[1].Contains(_tempCM.secondTrainNum))
                                {//找到对应行，先看下车次本身是否有标注，有标注的直接标记并跳过，无标注的判断是否一整行都是，是的话保留标注，否则取消标注
                                    string[] spCommand = command[1].Split('-');
                                    for (int spCount = 0; spCount < spCommand.Length; spCount++)
                                    {
                                        if (spCommand[spCount].Split('（')[0].Split('/')[0].Equals(_tempCM.firstTrainNum) || spCommand[spCount].Split('（')[0].Split('/')[0].Equals(_tempCM.secondTrainNum))
                                        {
                                            if (spCommand[spCount].Contains("高峰"))
                                            {//当前车次被标注
                                                AllModels[cmCount].trainType = 1;
                                                rushHourTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                            else if (spCommand[spCount].Contains("临客"))
                                            {
                                                AllModels[cmCount].trainType = 2;
                                                tempTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                            else if (spCommand[spCount].Contains("周末"))
                                            {
                                                AllModels[cmCount].trainType = 3;
                                                weekendTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                            else if (spCommand[spCount].Contains("加开"))
                                            {
                                                AllModels[cmCount].trainType = 4;
                                                addedTrain++;
                                                hasGotIt = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (!hasGotIt)
                                    {
                                        if (command[1].Contains("高峰-") ||
                                   command[1].Contains("高峰G") ||
                                   command[1].Contains("高峰D") ||
                                   command[1].Contains("高峰C") ||
                                   command[1].Contains("高峰0") ||
                                   command[1].Contains("高峰线-") ||
                                   command[1].Contains("高峰线G") ||
                                   command[1].Contains("高峰线D") ||
                                   command[1].Contains("高峰线C") ||
                                   command[1].Contains("高峰线0"))
                                        {//有整行标注
                                            AllModels[cmCount].trainType = 1;
                                            rushHourTrain++;
                                        }
                                        else if (command[1].Contains("周末-") ||
                                   command[1].Contains("周末G") ||
                                   command[1].Contains("周末D") ||
                                   command[1].Contains("周末C") ||
                                   command[1].Contains("周末0") ||
                                   command[1].Contains("周末线-") ||
                                   command[1].Contains("周末线G") ||
                                   command[1].Contains("周末线D") ||
                                   command[1].Contains("周末线C") ||
                                   command[1].Contains("周末线0"))
                                        {
                                            AllModels[cmCount].trainType = 3;
                                            weekendTrain++;
                                        }
                                        else if (command[1].Contains("临客G") ||
                                   command[1].Contains("临客D") ||
                                   command[1].Contains("临客C") ||
                                   command[1].Contains("临客0") ||
                                   command[1].Contains("临客-") ||
                                   command[1].Contains("临客线-") ||
                                   command[1].Contains("临客线G") ||
                                   command[1].Contains("临客线D") ||
                                   command[1].Contains("临客线C") ||
                                   command[1].Contains("临客线0"))
                                        {
                                            AllModels[cmCount].trainType = 2;
                                            tempTrain++;
                                        }
                                        else
                                        {//不是整行标注，则取消标注
                                            AllModels[cmCount].trainType = 0;
                                        }
                                        hasGotIt = true;
                                    }
                                }
                            }
                            if (hasGotIt)
                            {
                                break;
                            }
                        }
                    }
                }
                //右方显示框内容
                String commands = "";
                foreach (TrainModel model in AllModels)
                {
                    String streamStatus = "";
                    String trainType = "";
                    if (model.streamStatus == 1)
                    {
                        streamStatus = "√开";
                    }
                    else
                    {
                        streamStatus = "×停";
                    }
                    switch (model.trainType)
                    {
                        case 0:
                            trainType = "";
                            break;
                        case 1:
                            trainType = "-高峰";
                            break;
                        case 2:
                            trainType = "-临客";
                            break;
                        case 3:
                            trainType = "-周末";
                            break;
                    }
                    if (model.secondTrainNum.Equals("null"))
                    {
                        commands = commands  + model.firstTrainNum + "-" + model.trainModel + "-" + model.trainId + "-" + streamStatus + trainType + "\r\n";
                    }
                    else
                    {
                        commands = commands  + model.firstTrainNum + "/" + model.secondTrainNum + "-" + model.trainModel + "-" + model.trainId + "-" + streamStatus + trainType + "\r\n";
                    }
                }
                outputTB.Text = "共" + AllModels.Count.ToString() + "趟" + "\r\n" + commands;
                allHighSpeedCommandModel = AllModels;
                addedTrainText = outputTB.Text;
            }
            //catch (Exception e)
            {
                //MessageBox.Show("出现错误：" + e.ToString().Split('。')[0], "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        public bool IsTrainNumber(string input)
        {//判断是否是符合规范的车次 若不符合 则给予识别错误提示
            bool _isTrainNumber = false;
            if (input.Contains("CR"))
            {
                return false;
            }
            Regex regexOnlyNumAndAlphabeta = new Regex(@"^[A-Za-z0-9]+$");
            Regex regexOnlyAlphabeta = new Regex(@"^[A-Za-z]+$");
            if (regexOnlyNumAndAlphabeta.IsMatch(input) &&
                !regexOnlyAlphabeta.IsMatch(input) &&
                input.Length < 8 &&
                input.Length > 1)
            {
                _isTrainNumber = true;
            }
            return _isTrainNumber;
        }

        private List<TrainModel> trainModelAddFunc(String[] AllTrainNumberInOneRaw, int streamStatus, int trainType, string trainModel, string index)
        {//建立车次模型-通用方法
            //处理单程双车次车辆
            int trainConnectType = -1;
            string trainId = "";
            List<TrainModel> AllModels = new List<TrainModel>();
            if (!trainModel.Equals("null"))
            {//0短编 1长编 2重联
                if (trainModel.Contains("L") ||
                    trainModel.Contains("2B") ||
                    trainModel.Contains("2E") ||
                    trainModel.Contains("1E") ||
                    trainModel.Contains("AF-A") ||
                    trainModel.Contains("BF-A"))
                {
                    trainConnectType = 1;
                }
                else if (trainModel.Contains("+"))
                {
                    trainConnectType = 2;
                }
                else if (trainModel.Contains("AF-B") ||
                    trainModel.Contains("BF-B"))
                {//新增的 17节
                    trainConnectType = 3;
                }
                else
                {
                    trainConnectType = 0;
                }
            }
            if (trainConnectType == 2)
            {//重联，考虑不同型号重联情况
                Regex _regexOnlyNum = new Regex(@"^[0-9]+$");
                string[] trainIds = trainModel.Split('+');
                for (int i = 0; i < trainIds.Length; i++)
                {
                    for (int j = 0; j < trainIds[i].Split('-').Length; j++)
                    {
                        if (_regexOnlyNum.IsMatch(trainIds[i].Split('-')[j]))
                        {
                            if (!trainId.Contains("/"))
                            {
                                trainId = trainIds[i].Split('-')[j] + "/";
                            }
                            else
                            {
                                trainId = trainId + trainIds[i].Split('-')[j];
                            }
                        }
                    }
                }
            }
            else if (trainConnectType == 1)
            {//长编
                if (trainModel.Split('-').Length == 2)
                {
                    trainId = trainModel.Split('-')[1] + "L";
                }
                else if (trainModel.Split('-').Length == 3)
                {
                    trainId = trainModel.Split('-')[2] + "L";
                }
            }
            else
            {
                if (trainModel.Split('-').Length == 2)
                {
                    trainId = trainModel.Split('-')[1];
                }
                else if (trainModel.Split('-').Length == 3)
                {
                    trainId = trainModel.Split('-')[2];
                }
            }
            if (!trainModel.Contains("+"))
            {
                if (trainModel.Contains("-A"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-A";
                }
                else if (trainModel.Contains("-B") && !trainModel.Contains("-BZ"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-B";
                }
                else if (trainModel.Contains("-BZ"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-BZ";
                }
                else if (trainModel.Contains("-Z"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-Z";
                }
                else
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim();
                }
            }
            else
            {
                if (trainModel.Contains("-A"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-A+";
                }
                else if (trainModel.Contains("-B"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-B+";
                }
                else if (trainModel.Contains("-Z"))
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "-Z+";
                }
                else
                {
                    trainModel = trainModel.Split('-')[0].Replace("CRH", "").Replace("CR", "").Trim() + "+";
                }

            }
            //判断index是否为纯数字
            Regex regexOnlyNum = new Regex(@"^[0-9]+$");
            if (!regexOnlyNum.IsMatch(index))
            {
                char[] _indexChar = index.ToCharArray();
                string _tempIndexString = "";
                for (int i = 0; i < _indexChar.Length; i++)
                {
                    if (regexOnlyNum.IsMatch(_indexChar[i].ToString()))
                    {
                        _tempIndexString = _tempIndexString + _indexChar[i];
                    }
                    else
                    {
                        if (i == 0)
                        {//如果第一个字符就不是数字
                            index = "?";
                        }
                        else
                        {
                            index = _tempIndexString;
                            break;
                        }
                    }
                }
            }
            for (int k = 0; k < AllTrainNumberInOneRaw.Length; k++)
            {
                if (AllTrainNumberInOneRaw[k].Contains("G") ||
                   AllTrainNumberInOneRaw[k].Contains("D") ||
                   AllTrainNumberInOneRaw[k].Contains("C") ||
                   AllTrainNumberInOneRaw[k].Contains("J") ||
                   AllTrainNumberInOneRaw[k].Contains("00"))
                {
                    if (AllTrainNumberInOneRaw[k].Contains("/") && !AllTrainNumberInOneRaw[k].Contains("G/"))
                    {
                        string _trainNumber = "";
                        if (AllTrainNumberInOneRaw[k].Contains(" "))
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k].Split(' ')[0];
                        }
                        else
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k];
                        }
                        String[] trainWithDoubleNumber = _trainNumber.Split('/');
                        //先添加第一个车次
                        TrainModel m1 = new TrainModel();
                        m1.firstTrainNum = trainWithDoubleNumber[0].Trim();
                        m1.streamStatus = streamStatus;
                        m1.trainType = trainType;
                        m1.trainModel = trainModel;
                        m1.trainConnectType = trainConnectType;
                        m1.trainIndex = index;
                        m1.trainId = trainId;
                        //作用时间，指设定时间
                        List<DateTime> _dt = new List<DateTime>();
                        _dt.Add(date);
                        m1.effectiveDates = _dt;
                        if (!IsTrainNumber(m1.firstTrainNum))
                        {
                            m1.firstTrainNum = "未识别-(" + m1.firstTrainNum + ")";
                        }
                        Char[] firstTrainWord = trainWithDoubleNumber[0].ToCharArray();
                        String secondTrainWord = "";
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
                                m1.secondTrainNum = secondTrainWord.Trim();
                                m1.upOrDown = -1;
                                AllModels.Add(m1);
                                break;
                            }
                        }
                    }
                    else if (AllTrainNumberInOneRaw[k].Length != 0)
                    {
                        string _trainNumber = "";
                        if (AllTrainNumberInOneRaw[k].Contains(" "))
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k].Split(' ')[0];
                        }
                        else
                        {
                            _trainNumber = AllTrainNumberInOneRaw[k];
                        }
                        TrainModel model = new TrainModel();
                        model.firstTrainNum = _trainNumber;
                        if (!IsTrainNumber(model.firstTrainNum))
                        {
                            model.firstTrainNum = "未识别-(" + model.firstTrainNum + ")";
                        }
                        else
                        {
                            int outNum = 0;
                            int.TryParse(model.firstTrainNum.ToCharArray()[model.firstTrainNum.ToCharArray().Length - 1].ToString(), out outNum);
                            if (outNum % 2 == 0)
                            {//上行
                                model.upOrDown = 0;
                            }
                            else
                            {//下行
                                model.upOrDown = 1;
                            }
                        }
                        model.secondTrainNum = "null";
                        model.streamStatus = streamStatus;
                        model.trainType = trainType;
                        model.trainModel = trainModel;
                        model.trainConnectType = trainConnectType;
                        model.trainIndex = index;
                        model.trainId = trainId;
                        //作用时间，指设定时间
                        List<DateTime> _dt = new List<DateTime>();
                        _dt.Add(date);
                        model.effectiveDates = _dt;

                        AllModels.Add(model);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return AllModels;
        }

        //去除无用字符
        private List<string> removeUnuseableWord(string detectedCommand = "")
        {//字符转换
            String standardCommand = "";
            standardCommand = command_rTb.Text.ToString();
            List<string> commands = new List<string>();
            standardCommand = removing(standardCommand.Trim());
            commands.Add(standardCommand.Trim());
            return commands;
        }
        private string removing(string standardCommand)
        {
            if (standardCommand.Contains(":"))
            { standardCommand = standardCommand.Replace(":", "："); }
            //删除客调命令中的时间
            //standardCommand = Regex.Replace(standardCommand, @"\d+：\d", "");
            standardCommand = Regex.Replace(standardCommand, @"[0-9]{2}(：)[0-9]{2}", "");
            standardCommand = Regex.Replace(standardCommand, @"[0-9]{1}(：)[0-9]{2}", "");
            if (standardCommand.Contains("1\t2"))
                standardCommand = standardCommand.Replace("1\t2", "1、2");
            if (standardCommand.Contains("2\t2"))
                standardCommand = standardCommand.Replace("2\t2", "2、2");
            if (standardCommand.Contains("3\t2"))
                standardCommand = standardCommand.Replace("3\t2", "3、2");
            if (standardCommand.Contains("4\t2"))
                standardCommand = standardCommand.Replace("4\t2", "4、2");
            if (standardCommand.Contains("5\t2"))
                standardCommand = standardCommand.Replace("5\t2", "5、2");
            if (standardCommand.Contains("6\t2"))
                standardCommand = standardCommand.Replace("6\t2", "6、2");
            if (standardCommand.Contains("7\t2"))
                standardCommand = standardCommand.Replace("7\t2", "7、2");
            if (standardCommand.Contains("8\t2"))
                standardCommand = standardCommand.Replace("8\t2", "8、2");
            if (standardCommand.Contains("9\t2"))
                standardCommand = standardCommand.Replace("9\t2", "9、2");
            if (standardCommand.Contains("0\t2"))
                standardCommand = standardCommand.Replace("0\t2", "0、2");
            if (standardCommand.Contains("1道"))
                standardCommand = standardCommand.Replace("1道", "");
            if (standardCommand.Contains("I道"))
                standardCommand = standardCommand.Replace("I道", "");
            if (standardCommand.Contains("2道"))
                standardCommand = standardCommand.Replace("2道", "");
            if (standardCommand.Contains("3道"))
                standardCommand = standardCommand.Replace("3道", "");
            if (standardCommand.Contains("4道"))
                standardCommand = standardCommand.Replace("4道", "");
            if (standardCommand.Contains("5道"))
                standardCommand = standardCommand.Replace("5道", "");
            if (standardCommand.Contains("6道"))
                standardCommand = standardCommand.Replace("6道", "");
            if (standardCommand.Contains("7道"))
                standardCommand = standardCommand.Replace("7道", "");
            if (standardCommand.Contains("8道"))
                standardCommand = standardCommand.Replace("8道", "");
            if (standardCommand.Contains("9道"))
                standardCommand = standardCommand.Replace("9道", "");
            if (standardCommand.Contains("0道"))
                standardCommand = standardCommand.Replace("0道", "");
            if (standardCommand.Contains("V道"))
                standardCommand = standardCommand.Replace("V道", "");
            if (standardCommand.Contains("X道"))
                standardCommand = standardCommand.Replace("X道", "");
            if (standardCommand.Contains("车："))
                standardCommand = standardCommand.Replace("车：", "");

            string s1 = string.Empty;
            foreach (char c in standardCommand)
            {
                if (c == '\t')
                {
                    continue;
                }
                s1 += c;
            }
            standardCommand = s1;
            if (standardCommand.Contains(","))
                standardCommand = standardCommand.Replace(",", "");
            if (standardCommand.Contains("~"))
                standardCommand = standardCommand.Replace("~", "");
            if (standardCommand.Contains("～"))
                standardCommand = standardCommand.Replace("～", "");
            if (standardCommand.Contains("〜"))
                standardCommand = standardCommand.Replace("〜", "");
            if (standardCommand.Contains("—"))
                standardCommand = standardCommand.Replace("—", "-");
            if (standardCommand.Contains("签发："))
                standardCommand = standardCommand.Replace("签发：", "");
            if (standardCommand.Contains("会签："))
                standardCommand = standardCommand.Replace("会签：", "");
            if (standardCommand.Contains("–"))
                standardCommand = standardCommand.Replace("–", "-");

            if (standardCommand.Contains("("))
                standardCommand = standardCommand.Replace("(", "（");
            if (standardCommand.Contains(")"))
                standardCommand = standardCommand.Replace(")", "）");
            if (standardCommand.Contains("d"))
                standardCommand = standardCommand.Replace("d", "D");
            if (standardCommand.Contains("g"))
                standardCommand = standardCommand.Replace("g", "G");
            if (standardCommand.Contains("c"))
                standardCommand = standardCommand.Replace("c", "C");
            if (standardCommand.Contains("j"))
                standardCommand = standardCommand.Replace("j", "J");
            if (standardCommand.Contains("GG"))
                standardCommand = standardCommand.Replace("GG", "G");
            if (standardCommand.Contains("00G"))
                standardCommand = standardCommand.Replace("00G", "0G");
            if (standardCommand.Contains("DD"))
                standardCommand = standardCommand.Replace("DD", "D");
            if (standardCommand.Contains("CC"))
                standardCommand = standardCommand.Replace("CC", "C");
            if (standardCommand.Contains("JJ"))
                standardCommand = standardCommand.Replace("JJ", "J");

            //if (standardCommand.Contains("CRH"))
            // standardCommand = standardCommand.Replace("CRH", "");
            //if (standardCommand.Contains("CR"))
            // standardCommand = standardCommand.Replace("CR", "");
            if (standardCommand.Contains("；"))
                standardCommand = standardCommand.Replace("；", "");
            //特殊情况添加 221、2018年02月22日，CRH380AL-2600：【0J5901-DJ5902-G6718(石家庄～北京西):停运】，0G4909-G4910-G801/4-G6611-G1559/8-G807-0G808。
            //中括号/大括号转小括号 减少后期识别代码数量
            if (standardCommand.Contains("["))
                standardCommand = standardCommand.Replace("[", "（");
            if (standardCommand.Contains("—"))
                standardCommand = standardCommand.Replace("—", "-");
            if (standardCommand.Contains("]"))
                standardCommand = standardCommand.Replace("]", "）");
            if (standardCommand.Contains("【"))
                standardCommand = standardCommand.Replace("【", "（");
            if (standardCommand.Contains("】"))
                standardCommand = standardCommand.Replace("】", "）");
            if (standardCommand.Contains("{"))
                standardCommand = standardCommand.Replace("{", "）");
            if (standardCommand.Contains("}"))
                standardCommand = standardCommand.Replace("}", "）");
            if (standardCommand.Contains(" "))
                standardCommand = standardCommand.Replace(" ", "");
            if (standardCommand.Contains("人："))
                standardCommand = standardCommand.Replace("人：", "");
            if (standardCommand.Contains("G/"))
                standardCommand = standardCommand.Replace("G/", "");
            return standardCommand;
        }

        //读取时刻表到模型
        private void readTimeTable(IWorkbook workbook)
        {
            int IDColumn = -1;
            int trainNumColumn = -1;
            int startStopStationColumn = -1;
            int stopTimeColumn = -1;
            int startTimeColumn = -1;
            int trackColumn = -1;
            int tipsColumn = -1;
            int titleRow = -1;
            List<TimeTableModel> timeTableModel = new List<TimeTableModel>();
            //try
            {
                //找表头
                ISheet sheet1 = workbook.GetSheetAt(0);
                for (int i = 0; i <= sheet1.LastRowNum; i++)
                {
                    IRow row = sheet1.GetRow(i);
                    //short format = row.GetCell(0).CellStyle.DataFormat;
                    if (row != null)
                    {
                        if (row.GetCell(0) != null)
                        {
                            if (row.GetCell(0).ToString().Contains("序号"))
                            {
                                titleRow = i;
                                for (int j = 0; j <= row.LastCellNum; j++)
                                {

                                    if (row.GetCell(j) != null)
                                    {
                                        string titleText = row.GetCell(j).ToString();
                                        if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("序号"))
                                        {
                                            IDColumn = j;
                                        }
                                        if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("车次"))
                                        {
                                            trainNumColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("运行区段"))
                                        {
                                            startStopStationColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("到达时刻"))
                                        {
                                            stopTimeColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("发车时刻"))
                                        {
                                            startTimeColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("股道"))
                                        {
                                            trackColumn = j;
                                        }
                                        else if (row.GetCell(j).ToString().Trim().Replace("\n", "").Contains("备注"))
                                        {
                                            tipsColumn = j;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //找数据
                for (int i = titleRow + 1; i <= sheet1.LastRowNum; i++)
                {
                    int lastRow = sheet1.LastRowNum;
                    {
                        IRow _readingRow = sheet1.GetRow(i);
                        if (_readingRow != null)
                        {
                            TimeTableModel tempModel = new TimeTableModel();
                            if (_readingRow.GetCell(IDColumn) != null)
                            {//ID
                                int id = -1;
                                int.TryParse(_readingRow.GetCell(IDColumn).ToString(), out id);
                                if (id != -1)
                                {
                                    tempModel.ID = id;
                                }
                            }
                            if (_readingRow.GetCell(trainNumColumn) != null && trainNumColumn != 0)
                            {//车次
                                if (_readingRow.GetCell(trainNumColumn).ToString().Length != 0)
                                {
                                    string trainNumber = _readingRow.GetCell(trainNumColumn).ToString();
                                    tempModel.firstTrainNumber = trainNumber.Split('/')[0];
                                    if (trainNumber.Split('/').Length > 1)
                                    {
                                        string[] trainWithDoubleNumber = trainNumber.Split('/');
                                           Char[] firstTrainWord = trainWithDoubleNumber[0].ToCharArray();
                                        String secondTrainWord = "";
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
                                                tempModel.secondTrainNumber = secondTrainWord;
                                                break;
                                            }
                                        }
                                    }

                                }
                            }
                            if (_readingRow.GetCell(startStopStationColumn) != null && startStopStationColumn != 0)
                            {//始发站
                                if (_readingRow.GetCell(startStopStationColumn).ToString().Length != 0)
                                {
                                    tempModel.startStopStation = _readingRow.GetCell(startStopStationColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(stopTimeColumn) != null && stopTimeColumn != 0)
                            {//到时
                                if (_readingRow.GetCell(stopTimeColumn).ToString().Length != 0)
                                {
                                    tempModel.stopTime = _readingRow.GetCell(stopTimeColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(startTimeColumn) != null && startTimeColumn != 0)
                            {//发时
                                if (_readingRow.GetCell(startTimeColumn).ToString().Length != 0)
                                {
                                    tempModel.startTime = _readingRow.GetCell(startTimeColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(trackColumn) != null && trackColumn != 0)
                            {//股道
                                if (_readingRow.GetCell(trackColumn).ToString().Length != 0)
                                {
                                    tempModel.trackNum = _readingRow.GetCell(trackColumn).ToString();
                                }
                            }
                            if (_readingRow.GetCell(tipsColumn) != null && tipsColumn != 0)
                            {//备注
                                if (_readingRow.GetCell(tipsColumn).ToString().Length != 0)
                                {
                                    tempModel.tips = _readingRow.GetCell(tipsColumn).ToString();
                                }
                            }
                            if (tempModel.ID != 0)
                            {
                                timeTableModel.Add(tempModel);
                            }
                        }
                    }
                }
                timeTable = timeTableModel;
            }
            //catch (Exception e)
            {
                return;
            }
        }

        //将时刻表与模型相匹配
        private void matchTimeTableAndCommand(bool hasHighSpeedCommand = false)
        {
            //有高铁模型的话，加入高铁模型一起对比
            foreach(TimeTableModel _ttm in timeTable)
            {
                //对比高铁命令
                if (hasHighSpeedCommand)
                {
                    bool hasGot = false;
                    foreach (TrainModel _tm in allHighSpeedCommandModel)
                    {

                        if (_ttm.firstTrainNumber.Trim().Equals(_tm.firstTrainNum) ||
                            _ttm.firstTrainNumber.Trim().Equals(_tm.secondTrainNum) ||
                            _ttm.secondTrainNumber.Trim().Equals(_tm.firstTrainNum) ||
                            _ttm.secondTrainNumber.Trim().Equals(_tm.secondTrainNum))
                        {//相同车次
                         //找时间
                            if ((_ttm.secondTrainNumber.Length == 0 &&
                                    _tm.secondTrainNum.Length == 0) ||
                                   ( _ttm.firstTrainNumber.Length ==0 &&
                                    _tm.firstTrainNum.Length == 0))
                            {
                                continue;
                            }
                            foreach (DateTime _dt in _tm.effectiveDates)
                            {
                                if (_dt.Year.Equals(date.Year) &&
                                    _dt.Month.Equals(date.Month) &&
                                    _dt.Day.Equals(date.Day))
                                {//有相同时间
                                    //标记下trainID，停运状态
                                    _ttm.containedCommand = "高铁令";
                                    _ttm.streamStatus = _tm.streamStatus;
                                    hasGot = true;
                                }
                            }
                        }
                    }

                    //如果没匹配到，并且车次含有GDC，0J的情况下，默认为不开
                    if (!hasGot)
                    {
                        if(_ttm.firstTrainNumber.Contains("G")||
                           _ttm.firstTrainNumber.Contains("D") ||
                           _ttm.firstTrainNumber.Contains("C") ||
                           _ttm.firstTrainNumber.Contains("0J"))
                        {
                            _ttm.containedCommand = "高铁令";
                            //高铁令无内容不开，-2
                            _ttm.streamStatus = -2;
                        }
                    }
                }
                //对比长期命令(出现不同时提醒)
                foreach(NormalCommandModel _ncm in allCommandModel)
                {
                    foreach(TrainModel _tm in _ncm.allTrainModel)
                    {
                        if (_ttm.firstTrainNumber.Trim().Equals(_tm.firstTrainNum)||
                            _ttm.firstTrainNumber.Trim().Equals(_tm.secondTrainNum) ||
                            _ttm.secondTrainNumber.Trim().Equals(_tm.firstTrainNum) ||
                            _ttm.secondTrainNumber.Trim().Equals(_tm.secondTrainNum))
                        {//相同车次
                            //找时间
                            if ((_ttm.secondTrainNumber.Length == 0 &&
                                    _tm.secondTrainNum.Length == 0) ||
                                   (_ttm.firstTrainNumber.Length == 0 &&
                                    _tm.firstTrainNum.Length == 0))
                            {
                                continue;
                            }
                            //双车次赋值
                            if(_tm.secondTrainNum.Length !=0 && _ttm.secondTrainNumber.Length == 0)
                            {
                                if (_ttm.firstTrainNumber.Equals(_tm.firstTrainNum))
                                {
                                    _ttm.secondTrainNumber = _tm.secondTrainNum;
                                }
                                else if(_ttm.firstTrainNumber.Equals(_tm.secondTrainNum))
                                {
                                    _ttm.secondTrainNumber = _tm.firstTrainNum;
                                }

                            }
                            foreach (DateTime _dt in _tm.effectiveDates)
                            {
                                if(_dt.Year.Equals(date.Year)&&
                                    _dt.Month.Equals(date.Month) &&
                                    _dt.Day.Equals(date.Day))
                                {//有相同时间
                                    //标记下trainID，停运状态
                                    _ttm.containedCommand = _tm.ID;
                                    //如果有冲突，提醒
                                    if (_ttm.containedCommand.Contains("高铁令"))
                                    {
                                        if (!_ttm.streamStatus.Equals(_tm.streamStatus))
                                        {
                                            string streamStatusTTM = DetectStreamStatus(_ttm.streamStatus);
                                            string streamStatusTM = DetectStreamStatus(_tm.streamStatus);
                                            DialogResult dg = new DialogResult();
                                            if(MessageBox.Show("发现"+_ttm.firstTrainNumber+"/"+_ttm.secondTrainNumber+"次在长期命令"+_tm.commandID+"中与高铁命令中的运行状态不同。\n"+
                                                _tm.commandID+"："+ streamStatusTM + "\n高铁命令："+ streamStatusTTM+"\n选择“确定”替换为高铁命令状态，选择“取消”保持长期命令状态", "提醒",MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                                            {
                                                break ;
                                            }
                                            else
                                            {
                                                _ttm.streamStatus = _tm.streamStatus;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _ttm.streamStatus = _tm.streamStatus;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            showResult();
        }

        private string DetectStreamStatus(int streamStatus)
        {
            string streamStatusString = "";
            switch (streamStatus)
            {
                case 0:
                    streamStatusString = "停运";
                    break;
                default:
                    streamStatusString = "运行";
                    break;
            }
            return streamStatusString;
        }

        //对比完成，展示模型
        private void showResult(int search =0)
        {
            List<TimeTableModel> _allTTM = new List<TimeTableModel>();
            if(search == 0)
            {
                _allTTM = timeTable;
            }
            else
            {
                _allTTM = searchedTableModel;
            }
            {
                //附带的小组件清空
                OriginalText_rtb.Text = "";
                //先刷新总列表，以命令名为group名，group内为命令里的车
                mainList.Items.Clear();
                this.mainList.BeginUpdate();
                bool hasTrains = true;
                for (int q = 0; q < _allTTM.Count; q++)
                {
                    {
                        {
                            TimeTableModel _tm = _allTTM[q];
                            ListViewItem item = new ListViewItem();
                            string trainNum = _tm.firstTrainNumber;
                            if (_tm.secondTrainNumber.Trim().Length != 0)
                            {
                                trainNum = trainNum + "/" + _tm.secondTrainNumber;
                            }
                            item.SubItems[0].Text = _tm.ID.ToString();
                            item.SubItems.Add(trainNum);
                            //0停运，1恢复开行，-1未定义
                            if (_tm.streamStatus == 0)
                            {
                                item.SubItems.Add("停运");
                            }
                            else if (_tm.streamStatus >= 1)
                            {
                                item.SubItems.Add("开行");
                            }
                            else if (_tm.streamStatus == -1)
                            {
                                item.SubItems.Add("开行(默认)");
                            }
                            else if(_tm.streamStatus == -2)
                            {
                                item.SubItems.Add("高铁停运");
                            }
                            if (_tm.containedCommand.Contains("高铁令"))
                            {
                                item.SubItems.Add("高铁令");
                            }
                            else if(_tm.containedCommand.Length != 0)
                            {
                                string command = _tm.containedCommand.Substring(8, 5) + "(" + _tm.containedCommand.Substring(0, 8) + ")";
                                item.SubItems.Add(command);
                            }
                            else
                            {
                                item.SubItems.Add("无");
                            }
                            item.SubItems.Add(_tm.startStopStation);
                            item.SubItems.Add(_tm.startTime);
                            item.SubItems.Add(_tm.stopTime);
                            item.SubItems.Add(_tm.trackNum);
                            mainList.Items.Add(item);
                        }
                    }
                }
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高 
                mainList.SmallImageList = imgList; //这里设置listView的SmallImageList ,用imgList将其撑大 
                this.mainList.EndUpdate();
            }
        }

        private void OpenOriginalCommand(string fileName)
        {
            try
            {
                if (fileName.Length != 0)
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

        //在原文中高亮选中的内容
        private int searchAndHightlight(string find,int Type)
        {
            //type为0是长期命令，为1是高铁令
            RichTextBox targetRTB = new RichTextBox();
            if (Type == 0)
            {
                targetRTB = OriginalText_rtb;
            }
            else if (Type == 1)
            {
                targetRTB = command_rTb;
            }
            int index = 0;
            index = targetRTB.Find(find,0, RichTextBoxFinds.WholeWord);
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
                    nextIndex = targetRTB.Find(find, index + find.Length, RichTextBoxFinds.WholeWord);
                    if (nextIndex == -1)//若查到文件末尾，则重置nextIndex为初始位置的值，使其达到初始位置，顺利结束循环，否则会有异常。
                        nextIndex = startPos;
                    index = nextIndex;
                }
            }
            return -1;
        }

        //从选中的项目中找对应的命令对象(提供ID)
        private NormalCommandModel GetCommandFromSelect(string ID)
        {
            NormalCommandModel _tempCM = new NormalCommandModel();
            try
            {
                string commandID = ID.Substring(8, 5);
                string createDate = ID.Substring(0, 4) + "/" + ID.Substring(4, 2) + "/" + ID.Substring(6, 2);
                //遍历日期和命令号都符合的命令
                foreach (NormalCommandModel _cm in allCommandModel)
                {
                    //找到了命令
                    if (_cm.commandID.Equals(commandID) && _cm.createTime.ToString("yyyy/MM/dd").Equals(createDate))
                    {
                        _tempCM = _cm;
                    }
                }
                return _tempCM;
            }
            catch(Exception e)
            {

            }
            return _tempCM;

        }

        private void commandID_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                string ID = "";
                ReadOriginalCommand(GetCommandFromSelect(ID).ID);
            }
        }

        private void OriginalText_rtb_DoubleClick(object sender, EventArgs e)
        {
            if (OriginalText_rtb.Text.Length != 0)
            {
                //有令，有括号
                if(mainList.SelectedItems.Count == 0)
                {
                    return;
                }
                if (mainList.SelectedItems[0].SubItems[3].Text.Contains("("))
                {
                    //原格式50434(20211025)
                    //转成2021102550434
                    string commandID = mainList.SelectedItems[0].SubItems[3].Text.Replace("(", "").Replace(")", "").Substring(5, 8) + mainList.SelectedItems[0].SubItems[3].Text.Substring(0, 5);
                    OpenOriginalCommand(GetCommandFromSelect(commandID).fileName);
                }
            }
        }

        private void importTimeTable_btn_Click(object sender, EventArgs e)
        {
            matchTimeTableAndCommand(true);
        }

        private void startBtnCheck()
        { 

        }

        private void highSpeedCommand_rtb_TextChanged(object sender, EventArgs e)
        {
            commandText = command_rTb.Text;
            if (command_rTb.Text.Length != 0)
            {
                hasText = true;
                startBtnCheck();
                analyseCommand();
                matchTimeTableAndCommand(true);
            }
            else
            {
                hasText = false;
                startBtnCheck();
            }
        }

        private void searchHighSpeedCommand_tb_TextChanged(object sender, EventArgs e)
        {
            {
                //右方显示框内容
                String commands = "";
                List<TrainModel> _allModels = new List<TrainModel>();
                string searchText = searchHighSpeedCommand_tb.Text.ToString().Trim();
                searchText = searchText.ToUpper();
                if (allHighSpeedCommandModel == null)
                {
                    return;
                }
                if (searchText.Length == 0)
                {
                    outputTB.Text = addedTrainText;
                }
                for (int i = 0; i < allHighSpeedCommandModel.Count; i++)
                {
                    TrainModel model = allHighSpeedCommandModel[i];
                    if (model.firstTrainNum.Contains(searchText) ||
                        model.secondTrainNum.Contains(searchText))
                    {
                        String streamStatus = "";
                        String trainType = "";
                        if (model.streamStatus != 0)
                        {
                            streamStatus = "√开";
                        }
                        else
                        {
                            streamStatus = "×停";
                        }
                        switch (model.trainType)
                        {
                            case 0:
                                trainType = "";
                                break;
                            case 1:
                                trainType = "-高峰";
                                break;
                            case 2:
                                trainType = "-临客";
                                break;
                            case 3:
                                trainType = "-周末";
                                break;
                        }
                        if (model.secondTrainNum.Equals("null"))
                        {
                            commands = commands  + model.firstTrainNum + "-" + model.trainModel + "-" + model.trainId + "-" + streamStatus + trainType + "\r\n";
                        }
                        else
                        {
                            commands = commands  + model.firstTrainNum + "-" + model.secondTrainNum + "-" + model.trainModel + "-" + model.trainId + "-" + streamStatus + trainType + "\r\n";
                        }
                        _allModels.Add(model);
                    }
                }
                outputTB.Text = "共" + _allModels.Count.ToString() + "趟" + "\r\n" + commands;
                this.command_rTb.Select(0, 0);
                command_rTb.ScrollToCaret();
            }
        }

        private void timeTableDate_dtp_ValueChanged(object sender, EventArgs e)
        {
            if (!timeTableDate_dtp.Value.Date.Equals(date))
            {
                date = timeTableDate_dtp.Value;
                //高铁的作用时间要改
                foreach (TrainModel _tm in allHighSpeedCommandModel)
                {
                    _tm.effectiveDates.Add(date);
                }
                //重新匹配
                if(allHighSpeedCommandModel.Count != 0)
                {
                    matchTimeTableAndCommand(true);
                }
                else
                {
                    matchTimeTableAndCommand(false);
                }

            }
        }

        private void mainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //找出相应的命令
            if(mainList.SelectedItems.Count != 0)
            {
                //高亮显示，提取双车次
                string firstTrainNumber = mainList.SelectedItems[0].SubItems[1].Text;
                string secondTrainNumber = "";
                if (firstTrainNumber.Split('/').Length > 1)
                {
                    secondTrainNumber = firstTrainNumber.Split('/')[1];
                    firstTrainNumber = firstTrainNumber.Split('/')[0];
                }
                //有令，有括号
                if (mainList.SelectedItems[0].SubItems[3].Text.Contains("("))
                {
                    //原格式50434(20211025)
                    //转成2021102550434
                    string commandID = mainList.SelectedItems[0].SubItems[3].Text.Replace("(","").Replace(")","").Substring(5,8) + mainList.SelectedItems[0].SubItems[3].Text.Substring(0, 5);
                    ReadOriginalCommand(GetCommandFromSelect(commandID).fileName);

                    searchAndHightlight(firstTrainNumber,0);
                    searchAndHightlight(secondTrainNumber, 0);
                }
                else if (mainList.SelectedItems[0].SubItems[3].Text.Contains("高铁令"))
                {
                    searchAndHightlight(firstTrainNumber, 1);
                    searchAndHightlight(secondTrainNumber, 1);
                }
            }
            else
            {
                OriginalText_rtb.Text = "";
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void OriginalText_rtb_TextChanged(object sender, EventArgs e)
        {

        }

        //查找列车
        private void search_tb_TextChanged(object sender, EventArgs e)
        {
            searchedTableModel = new List<TimeTableModel>();
            if(search_tb.Text.Length != 0)
            {
                string text = search_tb.Text.ToUpper(); 
                foreach(TimeTableModel _ttm in timeTable)
                {
                    if(_ttm.firstTrainNumber.Contains(text) ||
                        _ttm.secondTrainNumber.Contains(text))
                    {
                        searchedTableModel.Add(_ttm);
                    }
                }
                showResult(1);
            }
            else
            {
                showResult(0);
            }
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
