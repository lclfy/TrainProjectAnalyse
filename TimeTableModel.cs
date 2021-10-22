using System;
using System.Collections.Generic;
using System.Text;

namespace TrainProjectAnalyse
{
    public class TimeTableModel
    {
        public int ID { get; set; }
        public string firstTrainNumber { get; set; }
        public string secondTrainNumber { get; set; }
        public string startStopStation { get; set; }
        public string stopTime { get; set; }
        public string startTime { get; set; }
        public string trackNum { get; set; }

        public string  tips{get;set;}

        //0停运，1恢复开行，其他为高铁令的开行，-1未定义
        //高铁令无内容不开，-2
        public int streamStatus { get; set; }
        //包含命令
        public string containedCommand { get; set; }

        public TimeTableModel()
        {
            ID = -1;
            streamStatus = -1;
            firstTrainNumber = "";
            secondTrainNumber = "";
            startStopStation = "";
            stopTime = "";
            startTime = "";
            containedCommand = "";
            trackNum = "";
            tips = "";

        }
    }
}
