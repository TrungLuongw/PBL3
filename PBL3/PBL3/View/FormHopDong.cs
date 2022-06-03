using PBL3.BLL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3.View
{
    public partial class FormHopDong : Form
    {
        int idStaff;
        CultureInfo culture = new CultureInfo("vi-VN");
        public FormHopDong(int id,bool level)
        {
            InitializeComponent();
            setGUI();
            BLL_HopDong.Instance.HetHan();
            idStaff = id;
            if(level)
            {

            }   
            else
            {
                button7.Dispose();
            }
            comboBox2.Items.Add("Tất cả");
            comboBox2.Items.Add("Thiếu lịch");
            comboBox2.Items.Add("Chưa có PT");
            comboBox2.Items.Add("Tự tập");
            comboBox2.Items.Add("Hết hạn");
            comboBox2.SelectedIndex = 0;
            LoadDGV();
        }
        private void setGUI()
        {
       

            foreach (CBBItems i in BLL_HopDong.Instance.GetStatus())
            {
                comboBox1.Items.Add(i);
            }
            numericUpDown1.Value = 1;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDGV();
            label10.Text = comboBox2.Text;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[x].Selected = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                {
                    int x = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    setGui2(x);
                }

            }
        }
        private void setGui2(int x)
        {
            
            Hopdong d = BLL_Lichtap.Instance.GetHopdongByID(x);
            tbtenkh.Text = d.TTKH.ten;
            if (d.idpt != null)
                tbtenpt.Text = d.PT.TTNV.ten;
            else
                tbtenpt.Text = "";
            tbgoitap.Text = d.GoiTap.ten;
            dateTimePicker1.Value = Convert.ToDateTime(d.ngayki);
            dateTimePicker2.Value = Convert.ToDateTime(d.ngayhethan);
            double tien = Convert.ToDouble(d.GoiTap.giatien);
            tbgiatien.Text = tien.ToString();
            comboBox1.SelectedIndex = (int)d.idstatus - 1;
        }

        private void tbgiatien_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {

                CultureInfo culture = new CultureInfo("vi-VN");
                int num = (int)numericUpDown1.Value;
                double tien = Convert.ToDouble(tbgiatien.Text)*1.0*num;
                DateTime d = DateTime.Now;
                if (Convert.ToDateTime(dateTimePicker2.Value) < d)
                {

                    d = d.AddMonths(num);
                }
                else
                {
                    d = Convert.ToDateTime(dateTimePicker2.Value).AddMonths(num);

                }
                if (MessageBox.Show(string.Format("Bạn có thật sự muốn gia hạn thêm {0} tháng ?\n Tổng tiền cần thanh toán : {1}", num, tien.ToString("c",culture)  ), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                { 
                    BLL_HopDong.Instance.GiahanHD(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value), d);
                    BLL_HoatDong.Instance.AddGiaHan(idStaff, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value), 1,num,tien * 1.0 * num);
                    setGui2(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    LoadDGV();
                }

            }
        }

        private void btxemlich_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {

                int idhd = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                if (BLL_HopDong.Instance.CheckIDPt(idhd))
                {
                    MessageBox.Show("Hợp đồng này ko có lịch tập");
                    return;
                }
                FormaddLich f = new FormaddLich(idhd, 0,idStaff);
                f.D += new FormaddLich.Mydel(LoadDGV);
                f.ShowDialog();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                
                int idhd = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                if (BLL_HopDong.Instance.CheckIDPt(idhd))
                {
                    MessageBox.Show("Hợp đồng tự do, ko có PT");
                    return;
                }
                FormTimPT f = new FormTimPT(idhd,idStaff);
                f.D1 += new FormTimPT.MyDelHD(LoadDGV);
                f.ShowDialog();

            }
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                for(int i=0; i<dataGridView1.SelectedRows.Count;i++)
                {
                    if (BLL_HopDong.Instance.kiemtraThoiHan((int)dataGridView1.SelectedRows[i].Cells[0].Value))
                    {
                        if (MessageBox.Show(string.Format("Hợp đồng ID = {0} này còn hạn bạn muốn xóa không ?", dataGridView1.SelectedRows[i].Cells[0].Value), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            BLL_HopDong.Instance.DelHD((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                            
                        }
                    }
                    else

                    {
                        if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa ?\nHợp đồng iD = {0} ?", dataGridView1.SelectedRows[i].Cells[0].Value), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            BLL_HopDong.Instance.DelHD((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                            
                        }
                    }
                       

                }
                LoadDGV();
            }
        }
        private void LoadDGV()
        {
            switch(comboBox2.Text)
            {
                case "Tất cả":
                    
                    dataGridView1.DataSource = BLL_HopDong.Instance.DanhsachHD().DataSource;
                    break;
                case "Thiếu lịch":
                    
                    dataGridView1.DataSource = BLL_HopDong.Instance.DanhsachHDTL().DataSource;
                    break;
                case "Hết hạn":
                    
                    dataGridView1.DataSource = BLL_HopDong.Instance.DanhsachHDHH().DataSource;
                    break;
                case "Chưa có PT":

                    dataGridView1.DataSource = BLL_HopDong.Instance.DanhsachHDTPT().DataSource;
                    break;
                case "Tự tập":

                    dataGridView1.DataSource = BLL_HopDong.Instance.DanhsachHDTT().DataSource;
                    break;
                default:
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string x = textBox1.Text;
            dataGridView1.DataSource= BLL_HopDong.Instance.SearchHD(x).DataSource;
        }

        

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int idhd = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                if (BLL_HopDong.Instance.CheckIDPt(idhd))
                {
                    MessageBox.Show("Hợp đồng này ko có PT");
                    return;
                }
                if (MessageBox.Show("bạn có chắc muốn xóa PT của HD này ???? ", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BLL_HopDong.Instance.XoaPTofHD((int)dataGridView1.SelectedRows[0].Cells[0].Value);
                    string x = "Đã Xóa PT của hợp đồng : " + (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    BLL_HoatDong.Instance.commitHD(idStaff, (int)dataGridView1.SelectedRows[0].Cells[0].Value, 3, x);
                    LoadDGV();
                }
            }
        }
    }
}
