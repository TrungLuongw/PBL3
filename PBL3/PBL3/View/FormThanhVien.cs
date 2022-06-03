using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PBL3.BLL;
using PBL3.DTO;

namespace PBL3.View
{
    public partial class FormThanhVien : Form
    { int idstaff;
        public FormThanhVien(int id,bool level)
        {
            InitializeComponent();
            idstaff = id;
            setGui();
            if(level)
            {

            }   
            else
            {
                button6.Dispose();
            }
            comboBox1.Items.Add("Tất cả");
            comboBox1.Items.Add("Hiện tại");
            comboBox1.Items.Add("Tiềm năng");
            comboBox1.Items.Add("Hết hạn HD");
            comboBox1.SelectedIndex = 0;
            LoadDGV();
        }
        private void setGui()
        {
            foreach (CBBItems i in BLL_kh.Instance.GetListGoitap())
            {
                tbgoitap.Items.Add(i);
            }
            tbgoitap.SelectedIndex = 0;
            
        }
        private void LoadDGV()
        {
            switch (comboBox1.Text)
            {
                case "Tất cả":
                    
                    dataGridView1.DataSource = BLL_kh.Instance.listKH().Select(p=>new {p.id,p.ten,p.ngaysinh }).ToList();
                    break;
                case "Hiện tại":
                    
                    dataGridView1.DataSource = BLL_kh.Instance.listKHold().Select(p => new { p.id, p.ten, p.ngaysinh }).ToList();
                    break;
                case "Tiềm năng":
                    
                    dataGridView1.DataSource = BLL_kh.Instance.listKHnew().Select(p => new { p.id, p.ten, p.ngaysinh }).ToList();
                    break;
                case "Hết hạn HD":
                    
                    dataGridView1.DataSource = BLL_kh.Instance.listKHHHHD().Select(p => new { p.id, p.ten, p.ngaysinh }).ToList();
                    break;
                default:
                    break;
            }
            label8.Text = "";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
                open.InitialDirectory = @"C:\Users\ThinkPad\Desktop";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(open.FileName);


                }
            }
        }
        byte[] ImageToByteArray(Image img)
        {
            MemoryStream m = new MemoryStream();
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            return m.ToArray();
        }
        Image ByteArrayToImage(Byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadDGV();
            
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
                    setGui2(x,false);
                }

            }
            label8.Text = "";
        }
        public void setGui2(int x,bool check)
        {
            if (x != 0)
            {
                TTKH d = BLL_kh.Instance.GetKhByID(x);
                tbid.Text = d.id.ToString();
                tbten.Text = d.ten;
                tbcmnd.Text = d.cmnd;
                tbsdt.Text = d.sdt;
                tbdiachi.Text = d.diachi;
                dateTimePicker1.Value = Convert.ToDateTime(d.ngaysinh);
                if (d.Hinh != null)
                    pictureBox1.Image = ByteArrayToImage((byte[])d.Hinh);
                else
                    pictureBox1.Image = null;
                
            }
            if(check)
            {
                LoadDGV();
            }    
        }
        

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    
                        if (MessageBox.Show(string.Format("Bạn có thật sự muốn xóa khách hàng : {0} ?", dataGridView1.SelectedRows[i].Cells[1].Value), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            BLL_kh.Instance.delKh((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                            string x = "Xóa thông tin khách hàng";
                            BLL_HoatDong.Instance.commit(idstaff, (int)dataGridView1.SelectedRows[i].Cells[0].Value, 3, x);
                             LoadDGV();
                        }
                    
                }
            }
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (pictureBox1.Image != null)
                {
                    byte[] b = ImageToByteArray(pictureBox1.Image);
                    BLL_kh.Instance.saveHinh(b, (int)dataGridView1.SelectedRows[0].Cells[0].Value);
                    setGui2((int)dataGridView1.SelectedRows[0].Cells[0].Value,false);
                    label8.Text = "Đã lưu...";
                    string x = "Update Thông tin khách hàng : Ảnh";
                    BLL_HoatDong.Instance.commit(idstaff, (int)dataGridView1.SelectedRows[0].Cells[0].Value, 3, x);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count==1)
            {
                if(BLL_kh.Instance.kiemtraHDExit((int)dataGridView1.SelectedRows[0].Cells[0].Value))
                {
                    if (BLL_kh.Instance.KiemtraGOitap(((CBBItems)tbgoitap.SelectedItem).value))
                    {
                        if (tbIDPT.Text != "")
                        {
                            if ((int)numericUpDown1.Value > 0)
                            {
                                GoiTap i = BLL_kh.Instance.GetGTByID(((CBBItems)tbgoitap.SelectedItem).value);
                                if (MessageBox.Show(string.Format("Đăng ký gói tập :{0}\n Hợp đồng : {1} tháng\nTên PT :{2}\n Giá tiền : {3}\t Tổng : {4}", i.ten
                                    , (int)numericUpDown1.Value, BLL_kh.Instance.getpt(Convert.ToInt32(tbIDPT.Text)).TTNV.ten, i.giatien, i.giatien * 1.0 * (int)numericUpDown1.Value),
                                    "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                                {
                                    BLL_kh.Instance.createHD((int)dataGridView1.SelectedRows[0].Cells[0].Value
                                    , Convert.ToInt32(tbIDPT.Text), ((CBBItems)tbgoitap.SelectedItem).value, (int)numericUpDown1.Value);
                                    BLL_HoatDong.Instance.AddHD(idstaff, (int)dataGridView1.SelectedRows[0].Cells[0].Value, 2, (int)numericUpDown1.Value, Convert.ToDouble(i.giatien * 1.0 * (int)numericUpDown1.Value));

                                    LoadDGV();
                                    setGUiHD();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Chưa nhập số tháng !");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hãy nhập thông tin PT !");
                        }
                    }
                    else
                    {
                        
                        if ((int)numericUpDown1.Value > 0)
                        {
                            GoiTap i = BLL_kh.Instance.GetGTByID(((CBBItems)tbgoitap.SelectedItem).value);
                            if (MessageBox.Show(string.Format("Đăng ký gói tập :{0}\n Hợp đồng : {1} tháng\nTên PT :Không có\n Giá tiền : {2}\t Tổng : {3}", i.ten
                                , (int)numericUpDown1.Value, i.giatien, i.giatien * 1.0 * (int)numericUpDown1.Value),
                                "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                            {
                                BLL_kh.Instance.createHD((int)dataGridView1.SelectedRows[0].Cells[0].Value
                                , 0, ((CBBItems)tbgoitap.SelectedItem).value, (int)numericUpDown1.Value);
                                BLL_HoatDong.Instance.AddHD(idstaff, (int)dataGridView1.SelectedRows[0].Cells[0].Value, 2, (int)numericUpDown1.Value, Convert.ToDouble(i.giatien * 1.0 * (int)numericUpDown1.Value));

                                LoadDGV();
                                setGUiHD();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Chưa nhập số tháng !");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Khách hàng này đã có hợp đồng !");
                }
            }
        }
        private void setGUiHD()
        {
            tbIDPT.Text = "";
            numericUpDown1.Value = 1;
        }
        public void TimIDPT(string x)
        {
            tbIDPT.Text = x;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                

                FormTimPT f = new FormTimPT(0,idstaff);
                f.D += new FormTimPT.MyDela(TimIDPT);
                f.Show();

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ADDTV f = new ADDTV(0, true,idstaff);
            f.D += new ADDTV.Mydel(setGui2);
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count==1)
            {
                string x = "Update Thông tin khách hàng";
                BLL_HoatDong.Instance.commit(idstaff, (int)dataGridView1.SelectedRows[0].Cells[0].Value, 3, x);
                ADDTV f = new ADDTV((int)dataGridView1.SelectedRows[0].Cells[0].Value, true,idstaff);
                f.D += new ADDTV.Mydel(setGui2);
                f.ShowDialog();
            }    
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox2.Text = BLL_kh.Instance.GetGTByID((int)((CBBItems)tbgoitap.SelectedItem).value).mota.ToString();
        }

        private void tbgoitap_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = BLL_kh.Instance.GetGTByID((int)((CBBItems)tbgoitap.SelectedItem).value).mota.ToString();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string x = textBox1.Text;
                dataGridView1.DataSource = BLL_kh.Instance.searchTTKH(x).Select(p => new { p.id, p.ten, p.ngaysinh }).ToList();
            }
        }

       
    }
}
