using System;
using System.Collections.Generic;
using System.Text;

namespace TrainProjectAnalyse
{
    public class TrainModel
    {
        //添加时间
        public DateTime createTime { get; set; }
        //命令号
        public string commandID { get; set; }
        //起始日期 结束日期
        public DateTime startDate { get; set; }
        public DateTime stopDate { get; set; }
        //车次
        public string firstTrainNum { get; set; }
        public string secondTrainNum { get; set; }
        //0停运，1恢复开行，-1未定义
        public int streamStatus { get; set; }

        public TrainModel()
        {
            createTime = DateTime.Now;
            commandID = "";
            firstTrainNum = "";
            secondTrainNum = "";
            streamStatus = -1;
        }

        public TrainModel(DateTime _createTime, string _commandID,
                                            DateTime _startDate,DateTime _stopDate,
                                            string _firstTrainNum,string _secondTrainNum,
                                            int _streamStatus)
        {
            this.createTime = _createTime;
            this.commandID = _commandID;
            this.startDate = _startDate;
            this.stopDate = _stopDate;
            this.firstTrainNum = _firstTrainNum;
            this.secondTrainNum = _secondTrainNum;
            this.streamStatus = _streamStatus;

        }
    }
}
