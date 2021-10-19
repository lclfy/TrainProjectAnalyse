﻿using System;
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

        public NormalCommandModel()
        {
            createTime = DateTime.Now;
            commandID = "";
            allTrainModel = new List<TrainModel>();
            fileName = "";
        }
    }
}