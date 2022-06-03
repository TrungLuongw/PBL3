using PBL3.BLL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3.View
{
    public partial class chart : Form
    {
        public chart()
        {
            InitializeComponent();
            setGui();
            label1.Text = "Biểu đồ thống kê doanh thu trong năm " + DateTime.Now.Year.ToString();
            checkBox1.Checked = true;
            checkBox2.Checked = true;
        }
        
        private void setGui()
        {
            List<datachart> list2 = new List<datachart>();
            for(int i =1;i<=12;i++)
            {
                list2.Add(new datachart { month = i, value = 0 });
            }
            List<datachart> list1 = new List<datachart>();
            for (int i = 1; i <= 12; i++)
            {
                list1.Add(new datachart { month = i, value = 0 });
            }
            foreach (LichSu i in BLL_HoatDong.Instance.listAdd().ToList())
            {
                if((int)i.maso==2)
                {
                    int x = Convert.ToInt32(Convert.ToDateTime(i.ngay).Month);
                    list2[x - 1].value += (float)i.tien;
                }    
            }
            foreach (LichSu i in BLL_HoatDong.Instance.listGiaHan().ToList())
            {
                if ((int)i.maso == 1)
                {
                    int x = Convert.ToInt32(Convert.ToDateTime(i.ngay).Month);
                    list1[x - 1].value += (float)i.tien;
                }
            }
            List<datachart> list3 = new List<datachart>();
            for (int i = 1; i <= 12; i++)
            {

                list3.Add(new datachart { month = i, value = list1[i-1].value+list2[i-1].value });

            }
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.Minimum = 0;
            chart.AxisX.Maximum = 12;
            chart.AxisY.Minimum = 0;
            chart.AxisX.Interval = 1;
            chart.AxisY.Interval = 3000000;
            chart.AxisX.Title="THÁNG";
            chart.AxisY.Title = "VNĐ";
            chart.AxisX.TitleAlignment = StringAlignment.Far;
            chart.AxisY.TitleAlignment = StringAlignment.Far;



            chart1.Series["Đăng Ký Hợp Đồng Mới"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Đăng Ký Hợp Đồng Mới"].Color = Color.Orange;
              foreach (datachart i in list2)
            {
                chart1.Series["Đăng Ký Hợp Đồng Mới"].Points.AddXY(i.month, i.value);
            }



            chart1.Series["Gia Hạn Hợp Đồng"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Gia Hạn Hợp Đồng"].Color = Color.Red;
            foreach (datachart i in list1)
            {
                chart1.Series["Gia Hạn Hợp Đồng"].Points.AddXY(i.month, i.value);
            }



            chart1.Series["Tổng doanh thu"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Tổng doanh thu"].Color = Color.Blue;
            foreach (datachart i in list3)
            {
                chart1.Series["Tổng doanh thu"].Points.AddXY(i.month, i.value);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            check();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            check();
        }
        private void check()
        {
            if(checkBox1.Checked&&checkBox2.Checked)
            {
                chart1.Series["Đăng Ký Hợp Đồng Mới"].Color = Color.Orange;
                chart1.Series["Gia Hạn Hợp Đồng"].Color = Color.Red;
                chart1.Series["Tổng doanh thu"].Color = Color.Blue;
            }
            else
            {
                if(checkBox1.Checked&&checkBox2.Checked==false)
                {
                    chart1.Series["Gia Hạn Hợp Đồng"].Color=Color.White;
                    chart1.Series["Đăng Ký Hợp Đồng Mới"].Color = Color.Orange;
                    chart1.Series["Tổng doanh thu"].Color = Color.White;
                }
                else
                {
                    if(checkBox1.Checked==false&&checkBox2.Checked)
                    {
                        chart1.Series["Gia Hạn Hợp Đồng"].Color = Color.Red;
                        chart1.Series["Đăng Ký Hợp Đồng Mới"].Color = Color.White;
                        chart1.Series["Tổng doanh thu"].Color = Color.White;
                    }
                    else
                    {
                        chart1.Series["Gia Hạn Hợp Đồng"].Color = Color.White;
                        chart1.Series["Đăng Ký Hợp Đồng Mới"].Color = Color.White;
                        chart1.Series["Tổng doanh thu"].Color = Color.White;
                    }
                }    
                    
            }    
        }
    }
}
