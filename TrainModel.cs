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
        //日期格式yyyy/MM/dd，存储时以英文逗号区分
        public List<DateTime> effectiveDates { get; set; }
        //车次
        public string firstTrainNum { get; set; }
        public string secondTrainNum { get; set; }
        //0停运，1恢复开行，-1未定义
        public int streamStatus { get; set; }
        //命令中的第几条
        public string placeInCommand { get; set; }

        //列车ID，为日期+命令号+三位数的第几个
        //20211020 51034 002
        public string ID { get; set; }

        public TrainModel()
        {
            createTime = DateTime.Now;
            effectiveDates = new List<DateTime>();
            commandID = "";
            firstTrainNum = "";
            secondTrainNum = "";
            streamStatus = -1;
            placeInCommand = "";
            ID = createTime.ToString("yyyyMMdd");
        }

        public TrainModel(DateTime _createTime, string _commandID,
                                            List<DateTime> _effectiveDates,
                                            string _firstTrainNum,string _secondTrainNum,
                                            int _streamStatus)
        {
            this.createTime = _createTime;
            this.commandID = _commandID;
            this.effectiveDates = _effectiveDates;
            this.firstTrainNum = _firstTrainNum;
            this.secondTrainNum = _secondTrainNum;
            this.streamStatus = _streamStatus;

        }
    }
}
