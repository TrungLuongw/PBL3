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
        bool check;
        int idstaff;
        public FormLichTap(int id)
        {
            InitializeComponent();
            setGui();
            check = false;
            idstaff = id;
        }
        public void setGui()
        {
            button3.Hide();
            foreach (CBBItems i in BLL_Lichtap.Instance.GetListDayOfWeed())
            {
                comboBox1.Items.Add(i);

            }
            switch (DateTime.Now.DayOfWeek.ToString())
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
            dataGridView1.DataSource = BLL_Lichtap.Instance.Listlichtap(x).DataSource;





        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = 0;
            if (dataGridView2.SelectedRows.Count == 1)
            {

                x = (int)dataGridView2.SelectedRows[0].Cells[0].Value;
                FormaddLich f = new FormaddLich(BLL_Lichtap.Instance.GetIDHopDongByIDLichTap(x), 0,idstaff);
                f.D += new FormaddLich.Mydel(LoadDGV1);
                f.ShowDialog();
                
            }

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.check = true;
            int i = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[i].Selected = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string x = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                dataGridView2.DataSource = BLL_Lichtap.Instance.ListTT(x, comboBox1.Text).DataSource;
            }
        }
        private void LoadDGV1()
        {
            string x = "";
            if(dataGridView1.SelectedRows.Count==1)
            {
                x= dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }    
            dataGridView1.DataSource = BLL_Lichtap.Instance.Listlichtap(comboBox1.Text).DataSource;
            if(x!="")
            dataGridView2.DataSource = BLL_Lichtap.Instance.ListTT(x, comboBox1.Text).DataSource;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            

            LoadDGV1();
            
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


            if (dataGridView2.SelectedRows.Count == 1)
            {

                if (check)
                {
                    int x = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                    lichtap a = BLL_Lichtap.Instance.GetLichTapById(x);
                    tbtenkh.Text = a.Hopdong.TTKH.ten;
                    tbsdtkh.Text = a.Hopdong.TTKH.sdt;
                    if (a.Hopdong.idpt != null)
                    {
                        tbtenpt.Text = a.Hopdong.PT.TTNV.ten;
                        tbsdtpt.Text = a.Hopdong.PT.TTNV.sdt;
                    }
                    else
                    {
                        tbtenpt.Text = "";
                        tbsdtpt.Text = "";
                    }
                }

            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (tbsearch.Text != "")
            {
                dataGridView3.DataSource = BLL_Lichtap.Instance.SearchTT(tbsearch.Text).DataSource;
                button3.Show();
            }
        }




        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[i].Selected = true;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView3.CurrentCell.RowIndex;
            dataGridView3.Rows[x].Selected = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {
                if (BLL_Lichtap.Instance.KiemTraCoHOpDong((int)dataGridView3.SelectedRows[0].Cells[0].Value))
                {
                    FormaddLich f = new FormaddLich(0, (int)dataGridView3.SelectedRows[0].Cells[0].Value,idstaff);
                    f.D += new FormaddLich.Mydel(LoadDGV1);
                    f.ShowDialog();
                    
                }
                else
                {


                    MessageBox.Show("Chưa có hợp đồng với khách hàng này hoặc khách hàng có hợp đồng tự tập!!");


                }
            }
        }
    }
}
