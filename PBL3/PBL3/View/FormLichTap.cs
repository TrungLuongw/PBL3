using PBL3.BLL;
using PBL3.DAL;
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
    public partial class FormLichTap : System.Windows.Forms.Form
    {
        public FormLichTap()
        {
            InitializeComponent();
            setGui();
        }
        public void setGui()
        {
            
            foreach(CBBItems i in DAL_LichTap.Instance.GetListDayOfWeed())
            {
                comboBox1.Items.Add(i);
                
            }
            switch(DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    comboBox1.SelectedIndex = 0;
                    break;
                case "Tuesday":
                    comboBox1.SelectedIndex = 1;
                    break;
                case "Wednesday":
                    comboBox1.SelectedIndex = 2;
                    break;
                case "Thursday":
                    comboBox1.SelectedIndex = 3;
                    break;
                case "Friday":
                    comboBox1.SelectedIndex = 4;
                    break;
                case "Saturday":
                    comboBox1.SelectedIndex = 5;
                    break;
                case "Sunday":
                    comboBox1.SelectedIndex = 6;
                    break;
                default:
                    break;

            }
            //var l1 = from f in db.lichtaps
            //         group f by f.ThoiGian.Time into g

            //         select new
            //         {

            //             Time = g.Key,
            //             count = g.Count()
            //         }
            //             ;
            string x = comboBox1.Text;
            //var l1 = db.lichtaps.Where(p => p.Ngay.Thu == x).GroupBy(p => p.ThoiGian.Time).Select(g => new { Time = g.Key, Count = g.Count() });
            //l1 = l1.OrderBy(p => p.Time.ToString().Length);
            dataGridView1.DataSource = BLL_QL.Instance.Listlichtap(x).DataSource;





        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = 0;
            if(dataGridView2.SelectedRows.Count==1)
            {

                x = (int)dataGridView2.SelectedRows[0].Cells[0].Value;
                FormaddLich f = new FormaddLich(BLL_Lichtap.Instance.GetIDHopDongByIDLichTap(x));
                f.Show();
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count ==1)
            {
                string x = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                //PBL db = new PBL();
                //var l1 = from f in db.lichtaps
                //         where f.ThoiGian.Time == x
                //         select new { tenkh = f.Hopdong.TTKH.ten, tennv = f.Hopdong.PT.TTNV.ten };
                //var l1 = from f in db.SVs select f;
                //var l2 = db.SVs.Select(p=> new { p.NameSV, p.LopSH.NameLop });
                //var l1 = from p in db.SVs
                //         where p.MSSV == 1
                //         select new { p.NameSV, p.LopSH.NameLop };

                //var l2 = db.SVs.Where(p => p.MSSV == 1).Select(p => new { p.NameSV, p.LopSH.NameLop });
                //var l1 = from p in db.SVs
                //         where p.MSSV == ((CBBItems)comboBox1.SelectedItem).value
                //         select new { p.NameSV, p.LopSH.NameLop };
                //var l2 = db.SVs.Where(p => p.MSSV == ((CBBItems)comboBox1.SelectedItem).value).Select(p => new { p.NameSV, p.LopSH.NameLop });         



                dataGridView2.DataSource = BLL_Lichtap.Instance.ListTT(x).DataSource;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string x = comboBox1.Text;
            
            dataGridView1.DataSource = BLL_QL.Instance.Listlichtap(x).DataSource;
            dataGridView2.DataSource = null;
            tbsdtkh.Text = "";
            tbsdtpt.Text = "";
            tbtenkh.Text = "";
            tbtenpt.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count==1)
            {
                int  x = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                lichtap a = BLL_Lichtap.Instance.GetLichTapById(x);
                tbtenkh.Text =   a.Hopdong.TTKH.ten;
                tbsdtkh.Text = a.Hopdong.TTKH.sdt;
                tbtenpt.Text = a.Hopdong.PT.TTNV.ten;
                tbsdtpt.Text = a.Hopdong.PT.TTNV.sdt;


            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
