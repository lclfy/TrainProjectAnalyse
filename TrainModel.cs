using System;
using System.Collections.Generic;
using System.Text;

namespace TrainProjectAnalyse
{
    public class TrainModel
    {
        //普通长期命令内容
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
        //0停运，1恢复开行，其他为高铁令的开行，-1未定义
        //高铁令无内容不开，-2
        public int streamStatus { get; set; }
        //命令中的第几条
        public string placeInCommand { get; set; }
        //列车ID，为日期+命令号+三位数的第几个
        //20211020 51034 002
        public string ID { get; set; }



        //高铁令内容
        //0为普通-1为高峰-2为临客-3为周末-4为加开
        public int trainType { get; set; }
        //车型
        public string trainModel { get; set; }
        //车号
        public string trainId { get; set; }
        //短-长-8+8（0,1,2）
        public int trainConnectType { get; set; }
        //上-下行
        public int upOrDown { get; set; }
        //第几行
        public string trainIndex { get; set; }



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

            trainModel = "";
            trainId = "";
            upOrDown = -1;
        }
    }
}
