using System;
using System.Collections.Generic;
using System.Text;

namespace TrainProjectAnalyse
{
    public class NormalCommandModel
    {//每个命令下含有多个车次
        public DateTime createTime { get; set; }
        public string commandID { get; set; }
        public List<TrainModel> allTrainModel { get; set; }
        public string fileName { get; set; }

        //列车ID，为日期+命令号
        //2021102051034
        public string ID { get; set; }
        //里面Train的ID总数，添加新的时直接+1即可
        public int TrainIDCount { get; set; }

        public NormalCommandModel()
        {
            createTime = DateTime.Now;
            commandID = "";
            allTrainModel = new List<TrainModel>();
            fileName = "";
            ID = createTime.ToString("yyyyMMdd");
            TrainIDCount = 1;
        }
    }
}
