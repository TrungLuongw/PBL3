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
        public delegate void Mydel();
        public Mydel D { get; set; }
        int idstaff;
        public FormaddLich(int idhd, int idkh,int iduser)
        {

            InitializeComponent();
            this.IDHopDong = idhd;
            this.IDKH = idkh;
            setGUI();
            idstaff = iduser;
            cbTime.Items.Clear();
            cbday.Items.Clear();
            foreach (CBBItems i in BLL_Lichtap.Instance.GetListDayOfWeed())
            {
                cbday.Items.Add(i);

            }
            foreach (CBBItems i in BLL_Lichtap.Instance.GetTimeOfDay())
            {
                cbTime.Items.Add(i);

            }
        }
        int IDHopDong;
        int IDKH;
        


        private void setGUI()
        {
            cbday.SelectedItem = null;
            cbTime.SelectedItem = null;


            if (IDHopDong != 0)
            {
                Hopdong d = BLL_Lichtap.Instance.GetHopdongByID(IDHopDong);
                tbidPT.Text = d.idpt.ToString();
                tbidKH.Text = d.idKH.ToString();
                if (d.idpt != null)
                    cbTenPT.Text = d.PT.TTNV.ten;
                else
                    cbTenPT.Text = "";
                cbTenKh.Text = d.TTKH.ten;
                tbsb.Text = d.GoiTap.sobuoi.ToString();
                dataGridView1.DataSource = BLL_Lichtap.Instance.GetLichtapByIdHD(IDHopDong).DataSource;
                tbsbhc.Text = dataGridView1.Rows.Count.ToString();
                tbIDHD.Text = IDHopDong.ToString();

            }
            else
            {
                int x = BLL_Lichtap.Instance.GetIDHopDongByIDKKandPT(IDKH, 0).Count;
                if (x > 0)
                {
                    if (x > 1)
                    {
                        foreach (CBBItems i in BLL_Lichtap.Instance.ListCBBPTByIdKH(IDKH))
                        {
                            cbTenPT.Items.Add(i);
                        }
                        cbTenPT.SelectedIndex = 0;
                        tbidPT.Text = ((CBBItems)cbTenPT.SelectedItem).value.ToString();
                        tbidKH.Text = IDKH.ToString();
                        cbTenKh.Text = BLL_Lichtap.Instance.GetTTKHById(IDKH).ten;
                        tbIDHD.Text = BLL_Lichtap.Instance.GetIDHopDongByIDKKandPT(Convert.ToInt32(tbidKH.Text), Convert.ToInt32(tbidPT.Text))[0].id.ToString();
                        dataGridView1.DataSource = BLL_Lichtap.Instance.GetLichtapByIdHD(Convert.ToInt32(tbIDHD.Text)).DataSource;
                        Hopdong d = BLL_Lichtap.Instance.GetHopdongByID(Convert.ToInt32(tbIDHD.Text));
                    }
                    else
                    {
                        Hopdong d = BLL_Lichtap.Instance.GetIDHopDongByIDKKandPT(IDKH, 0)[0];
                        tbidPT.Text = d.idpt.ToString();
                        tbidKH.Text = d.idKH.ToString();
                        if (d.idpt != null)
                            cbTenPT.Text = d.PT.TTNV.ten;
                        else
                            cbTenPT.Text = "";
                        cbTenKh.Text = d.TTKH.ten;
                        tbsb.Text = d.GoiTap.sobuoi.ToString();
                        dataGridView1.DataSource = BLL_Lichtap.Instance.GetLichtapByIdHD(d.id).DataSource;
                        tbsbhc.Text = dataGridView1.Rows.Count.ToString();
                        tbIDHD.Text = d.id.ToString();


                    }
                }



            }





        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                cbday.SelectedIndex = (int)dataGridView1.SelectedRows[0].Cells[3].Value - 1;
                cbTime.SelectedIndex = (int)dataGridView1.SelectedRows[0].Cells[4].Value - 1;
            }
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(string.Format("Bạn có thật sự muốn thanh toán hóa đơn \n Tổng tiền - (Tổng tiền/100)x Giảm giá \n =>{0}-({1}/100)x{2}={3}", tongtien, tongtien, giam, tienthanhtoan.ToString("c", culture)), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            //{
            //    billDAO.Instance.thanhtoan(idBill, tongtien, giam);
            //}
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(string.Format("Bạn có thật sự muốn xóa {0} lịch tập này ?", (int)dataGridView1.SelectedRows.Count), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        BLL_Lichtap.Instance.DelLichTapById((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                        BLL_HoatDong.Instance.commit(idstaff, Convert.ToInt32(tbidKH.Text), 3, "Thay đổi lịch tập !");
                    }

                    setGUI();
                }



            }

        }

        private void btthem_Click(object sender, EventArgs e)
        {
            if (tbsb.Text == tbsbhc.Text)
            {
                MessageBox.Show("Đã đủ số buổi tập !!!");
                return;
            }
            if (cbTenPT.Text == "" || cbTenKh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập PT và khách hàng trước !!!");
                return;
            }
            if (cbday.SelectedItem ==null || cbTime.SelectedItem== null)
            {
                MessageBox.Show("Vui lòng nhập Thời gian và ngày đầy đủ !!!");
                return;
            }
            if (tbIDHD.Text != "")
            {
                if (BLL_Lichtap.Instance.kiemtraLichTap(Convert.ToInt32(tbidPT.Text), cbTime.SelectedIndex + 1, cbday.SelectedIndex + 1))
                {
                    BLL_Lichtap.Instance.AddLichTap(Convert.ToInt32(tbIDHD.Text), cbTime.SelectedIndex + 1, cbday.SelectedIndex + 1);
                    BLL_HoatDong.Instance.commit(idstaff, Convert.ToInt32(tbidKH.Text), 3, "Thay đổi lịch tập !");
                    setGUI();

                    
                }
                else
                {
                    string x = "";
                    List<LichTapCuaPT> list = BLL_Lichtap.Instance.MesKiemTraLichtap(Convert.ToInt32(tbidPT.Text), cbTime.SelectedIndex + 1, cbday.SelectedIndex + 1);
                    foreach (LichTapCuaPT i in list)
                    {
                        x += i.Day + " : " + i.Time + "\n";
                    }
                    MessageBox.Show("PT đã có lịch tập trong khoảng thời gian này ! \n" + x);


                }
            }

        }

        private void btsua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {

                if (BLL_Lichtap.Instance.kiemtraLichTap(Convert.ToInt32(tbidPT.Text), cbTime.SelectedIndex + 1, cbday.SelectedIndex + 1))
                {
                    BLL_Lichtap.Instance.UpdateLichTapById((int)dataGridView1.SelectedRows[0].Cells[0].Value, cbday.SelectedIndex + 1, cbTime.SelectedIndex + 1);
                    BLL_HoatDong.Instance.commit(idstaff, Convert.ToInt32(tbidKH.Text), 3, "Thay đổi lịch tập !");
                    setGUI();
                }
                else
                {
                    string x = "";
                    List<LichTapCuaPT> list = BLL_Lichtap.Instance.MesKiemTraLichtap(Convert.ToInt32(tbidPT.Text), cbTime.SelectedIndex + 1, cbday.SelectedIndex + 1);
                    foreach (LichTapCuaPT i in list)
                    {
                        x += i.Day + " : " + i.Time + "\n";
                    }
                    MessageBox.Show("PT đã có lịch tập trong khoảng thời gian này ! \n" + x);


                }


            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[x].Selected = true;
        }

       

       

        private void FormaddLich_FormClosed(object sender, FormClosedEventArgs e)
        {
            D();

            this.Close();
        }
    }
}
