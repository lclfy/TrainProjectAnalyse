using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace TrainProjectAnalyse
{
    public partial class EditForm : Skin_Mac
    {
        public TrainModel editModel;
        //0是new,1是edit
        int NewOrEdit;
        string editYear = "";
        string editMonth = "";
        string editDay = "";
        //如果是添加的，添加完后不会自动关闭窗口（弹窗），会清空以添加下一个
        //如果是修改的，修改完后就会自动关闭（弹窗）
        //是否修改了
        bool getEdited = false;
        public EditForm(TrainModel _tm,int _newOrEdit)
        {
            InitializeComponent();
            editModel = _tm;
            NewOrEdit = _newOrEdit;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            InitView(NewOrEdit);

        }

        private void InitView(int newOrEdit)
        {
            //编辑模式
            if(newOrEdit == 1)
            {
                //车次
                firstTrainNum_tb.Text = editModel.firstTrainNum;
                secondTrainNum_tb.Text = editModel.secondTrainNum;
                //运行状态
                switch (editModel.streamStatus)
                {
                    case 0:
                        streamState_lb.SelectedIndex = 0;
                        break;
                    case 1:
                        streamState_lb.SelectedIndex = 1;
                        break;
                    default:
                        break;
                }
                //显示时间
                this.date_lv.BeginUpdate();
                date_lv.Items.Clear();
                /*
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(30, 10);// 设置行高 20 //分别是宽和高  
                date_lv.LargeImageList = imgList;
                */
                
                //添加数据
                TrainModel _tm = editModel;
                foreach (DateTime _dt in _tm.effectiveDates)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = _dt.ToLongDateString();
                    date_lv.Items.Add(item);
                }
                this.date_lv.EndUpdate();
            }
            else
            {
                streamState_lb.SelectedIndex = 0;
            }
        }

        private void RefreshDateView()
        {
            //显示时间
            this.date_lv.BeginUpdate();
            date_lv.Items.Clear();
            /*
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(20, 10);// 设置行高 20 //分别是宽和高  
            date_lv.LargeImageList = imgList;
            */
            //添加数据
            TrainModel _tm = editModel;
            foreach (DateTime _dt in _tm.effectiveDates)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = _dt.ToLongDateString();
                date_lv.Items.Add(item);
            }
            this.date_lv.EndUpdate();
        }

        private void ClearData()
        {
            editModel = new TrainModel();
            NewOrEdit = 0;
            editYear = "";
            editMonth = "";
            editDay = "";
            firstTrainNum_tb.Text = "";
            secondTrainNum_tb.Text = "";
            streamState_lb.SelectedIndex = 0;
            date_lv.Items.Clear();
            year_txt.Text = "";
            month_txt.Text = "";
            day_txt.Text = "";
        }

        //修改日期，选中日期后显示出来，并且可以修改，可以删除
        private void EditDate(int dateIndex,DateTime date)
        {
            editModel.effectiveDates.RemoveAt(dateIndex);
            editModel.effectiveDates.Add(date);
        }

        private void DeleteDate(int dateIndex)
        {
            editModel.effectiveDates.RemoveAt(dateIndex);
        }

        private void date_lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(date_lv.SelectedItems.Count != 0)
            {
                //年月日填进去
                string date = date_lv.SelectedItems[0].Text;
                if(date.Contains("年")&&
                    date.Contains("月")&&
                    date.Contains("日"))
                {
                    year_txt.Text = date.Split('年')[0];
                    month_txt.Text = date.Split('月')[0].Split('年')[1];
                    day_txt.Text = date.Split('月')[1].Replace("日", "");
                }
            }
        }

        private void year_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //号头只能为数字
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void month_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //号头只能为数字
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void day_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //号头只能为数字
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void year_txt_TextChanged(object sender, EventArgs e)
        {
            editYear = year_txt.Text;
        }

        private void month_txt_TextChanged(object sender, EventArgs e)
        {
            editMonth = month_txt.Text;
        }

        private void day_txt_TextChanged(object sender, EventArgs e)
        {
            editDay = day_txt.Text;
        }

        private void editDate_btn_Click(object sender, EventArgs e)
        {
            //不能为空
            if(year_txt.Text.Length != 0 && month_txt.Text.Length!=0 && day_txt.Text.Length != 0)
            {
                string NewTime = editYear + "-" + editMonth + "-" + editDay;
                DateTime NewDateTime;
                bool result = DateTime.TryParse(NewTime, out NewDateTime);
                if(result == true)
                {
                    EditDate(date_lv.Items.IndexOf(date_lv.SelectedItems[0]),NewDateTime);
                    RefreshDateView();
                }
                else
                {
                    MessageBox.Show("请输入正确的日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("输入的日期不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void stopDate_btn_Click(object sender, EventArgs e)
        {
            DeleteDate(date_lv.Items.IndexOf(date_lv.SelectedItems[0]));
            RefreshDateView();
        }

        private void addStartTime_dtp_ValueChanged(object sender, EventArgs e)
        {

        }

        private void continuiesDay_addbutton_Click(object sender, EventArgs e)
        {
            //添加连续日期
            List<DateTime> li = new List<DateTime>();
            DateTime time1 = addStartTime_dtp.Value;
            DateTime time2 = addEndTime_dtp.Value;
            string allDates = "";
            if(time1 > time2)
            {
                MessageBox.Show("请选择正确的连续日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (time1.Date == time2.Date)
            {
                allDates = allDates + time1.ToShortDateString() + "\n";
                li.Add(time1);
                time1 = time1.AddDays(1);
            }
            else
            {
                while (time1 <= time2.AddDays(1) && time1.Year > 1990 && time1 != time2)
                {
                    allDates = allDates + time1.ToShortDateString() + "\n";
                    li.Add(time1);
                    time1 = time1.AddDays(1);
                }
            }


            if(MessageBox.Show("要添加的日期为：\n"+allDates+"是否添加", "确认信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //判断是否有重复日期
                string sameDates = "";
                foreach(DateTime _dt in li)
                {
                    bool hasGot = false;
                    foreach(DateTime _trainDT in editModel.effectiveDates)
                    {
                        //有重复的，不能添加
                        if (_trainDT.ToShortDateString().Equals(_dt.ToShortDateString()))
                        {
                            sameDates = sameDates + _dt.ToShortDateString() + "\n";
                            hasGot = true;
                            break;
                        }
                    }
                    if (!hasGot)
                    {
                        editModel.effectiveDates.Add(_dt);
                        RefreshDateView();
                    }
                }
                if (sameDates.Length != 0)
                {
                    MessageBox.Show("以下日期与现有重复，未添加：\n" + sameDates, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
            }

        }

        private void singleDay_addButton_Click(object sender, EventArgs e)
        {
            //添加单一日期
            DateTime _date = addSingleTime_dtp.Value;
            if (MessageBox.Show("要添加的日期为：\n" + _date.ToShortDateString() + "\n是否添加", "确认信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                //判断是否有重复日期
                string sameDates = "";
                {
                    bool hasGot = false;
                    foreach (DateTime _trainDT in editModel.effectiveDates)
                    {
                        //有重复的，不能添加
                        if (_trainDT.ToShortDateString().Equals(_date.ToShortDateString()))
                        {
                            sameDates = sameDates + _date.ToShortDateString() + "\n";
                            hasGot = true;
                            break;
                        }
                    }
                    if (!hasGot)
                    {
                        editModel.effectiveDates.Add(_date);
                        RefreshDateView();
                    }
                }
                if (sameDates.Length != 0)
                {
                    MessageBox.Show("日期与现有日期重复，添加失败。\n" + sameDates, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (firstTrainNum_tb.Text.Length != 0 && date_lv.Items.Count != 0 &&streamState_lb.SelectedItem.ToString().Trim().Length != 0)
            {
                editModel.firstTrainNum = firstTrainNum_tb.Text.Trim();
                editModel.secondTrainNum = secondTrainNum_tb.Text.Trim();
                if (streamState_lb.SelectedItem.ToString().Contains("停运"))
                {
                    editModel.streamStatus = 0;
                }
                else if (streamState_lb.SelectedItem.ToString().Contains("恢复开行"))
                {
                    editModel.streamStatus = 1;
                }
                //添加的，则清空准备添加下一个，编辑的，对话框关闭
                AnalyseForm ownerForm = (AnalyseForm)this.Owner;
                ownerForm.EditComplete(editModel, NewOrEdit);
                getEdited = true;
                //新建
                if (NewOrEdit == 0)
                {
                    MessageBox.Show("保存成功，点击确定继续添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ClearData();
                }
                //编辑
                else if(NewOrEdit == 1)
                {
                    MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }


            }
            else
            {
                if(firstTrainNum_tb.Text.Length == 0)
                {
                    if (date_lv.Items.Count == 0)
                    {
                        MessageBox.Show("车次，日期为空，请添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        MessageBox.Show("车次为空，请添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (date_lv.Items.Count == 0)
                {
                    MessageBox.Show("日期为空，请添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else if(streamState_lb.SelectedItem.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("请选择运行状态", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void closeForm_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
