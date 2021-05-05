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
    public partial class FormaddLich : Form
    {
        public FormaddLich(int x)
        {
            InitializeComponent();
            IDHopDong = x;
            setGUI();
        }
        int IDHopDong;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void setGUI()
        {
            foreach (CBBItems i in BLL_Lichtap.Instance.GetListDayOfWeed())
            {
                cbday.Items.Add(i);

            }
            foreach (CBBItems i in BLL_Lichtap.Instance.GetTimeOfDay())
            {
                cbTime.Items.Add(i);

            }


            if (IDHopDong !=0)
            {
                Hopdong d = BLL_Lichtap.Instance.GetHopdongByID(IDHopDong);
                tbidPT.Text = d.idpt.ToString();
                tbidKH.Text = d.idKH.ToString();
                cbTenPT.Text = d.PT.TTNV.ten;
                cbTenKh.Text = d.TTKH.ten;
                tbsb.Text = d.GoiTap.sobuoi.ToString();
                dataGridView1.DataSource = BLL_Lichtap.Instance.GetLichtapByIdHD(IDHopDong).DataSource;
                tbsbhc.Text = dataGridView1.Rows.Count.ToString();
                
            }




            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count==1)
            {
                cbday.SelectedIndex = (int)dataGridView1.SelectedRows[0].Cells[3].Value-1;
                cbTime.SelectedIndex = (int)dataGridView1.SelectedRows[0].Cells[4].Value-1;
            }
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(string.Format("Bạn có thật sự muốn thanh toán hóa đơn \n Tổng tiền - (Tổng tiền/100)x Giảm giá \n =>{0}-({1}/100)x{2}={3}", tongtien, tongtien, giam, tienthanhtoan.ToString("c", culture)), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            //{
            //    billDAO.Instance.thanhtoan(idBill, tongtien, giam);
            //}
            if(dataGridView1.SelectedRows.Count>0)
            {
                if (MessageBox.Show(string.Format("Bạn có thật sự muốn xóa {0} lịch tập này ?",(int)dataGridView1.SelectedRows.Count), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                { 
                    for(int i =0; i< dataGridView1.SelectedRows.Count;i++)
                    {
                        BLL_Lichtap.Instance.DelLichTapById((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                    }

                    setGUI();
                }    



            }

        }

        private void btthem_Click(object sender, EventArgs e)
        {
            if(tbsb.Text == tbsbhc.Text)
            {
                MessageBox.Show("Đã đủ số buổi tập !!!");
                return;
            }
            if(cbTenPT.Text==""||cbTenKh.Text=="")
            {
                MessageBox.Show("Vui lòng nhập PT và khách hàng trước !!!");
            }
        }
    }
}
