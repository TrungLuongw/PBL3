using PBL3.BLL;
using PBL3.DAL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3.View
{
    public partial class ADDTV : Form
    {
        bool kh;
        int id;
        int idstaff;
        public delegate void Mydel(int id,bool check);

        public Mydel D { get; set; }

        public ADDTV(int id,bool kh,int idStaff)
        {
            InitializeComponent();
            this.kh = kh;
            this.id = id;
            this.idstaff = idStaff;
            setGui();
        }
        
        private void setGui()
        {
            
            if(kh)
            {
                groupBox2.Hide();
                if(id==0)
                {
                    button1.Dispose();
                    button4.Dispose();
                    pictureBox1.Dispose();
                    button7.Dispose();
                }
                else
                {
                    button6.Dispose();
                    TTKH t = BLL_kh.Instance.GetKhByID(id);
                    tbid.Text = t.id.ToString();
                    tbten.Text = t.ten;
                    tbcmnd.Text = t.cmnd;
                    tbsdt.Text = t.sdt;
                    tbdiachi.Text = t.diachi;
                    dateTimePicker1.Value = Convert.ToDateTime(t.ngaysinh);
                    if(t.Hinh!=null)
                    pictureBox1.Image = ByteArrayToImage(t.Hinh);
                }    
            }
            else
            {
                foreach (CBBItems i in BLL_NhanVien.Instance.listCV())
                {
                    comboBox1.Items.Add(i);
                }
                if(id==0)
                {
                    button1.Dispose();
                    button4.Dispose();
                    pictureBox1.Dispose();
                    button7.Dispose();
                }
                else
                {
                    button6.Dispose();
                    TTNV t = BLL_NhanVien.Instance.GetTTNV(id);
                    tbid.Text = t.id.ToString();
                    tbten.Text = t.ten;
                    tbcmnd.Text = t.cmnd;
                    tbsdt.Text = t.sdt;
                    tbdiachi.Text = t.diachi;
                    dateTimePicker1.Value = Convert.ToDateTime(t.ngaysinh);
                    if (t.Hinh != null)
                        pictureBox1.Image = ByteArrayToImage(t.Hinh);
                    dateTimePicker2.Value =Convert.ToDateTime( t.ngayvao);
                    
                    comboBox1.SelectedIndex =(int)t.idchucvu - 1;
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

        private void button7_Click(object sender, EventArgs e)
        {
            label11.Text = "";
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            open.InitialDirectory = @"C:\Users\ThinkPad\Desktop";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(tbid.Text!=""&&tbten.Text!=""&&tbcmnd.Text!=""&&tbsdt.Text!=""&&tbdiachi.Text!="")
            {
                if (dateTimePicker1.Value < DateTime.Now.AddYears(-9))
                {
                    if (kh)
                    {
                        BLL_kh.Instance.updateTTKH(Convert.ToInt32(tbid.Text), tbten.Text,
                          tbcmnd.Text, tbsdt.Text, tbdiachi.Text, dateTimePicker1.Value);
                        D(id,true);
                        this.Dispose();
                    }
                    else
                    {
                        if(comboBox1.SelectedItem!=null)
                        {
                            
                                BLL_NhanVien.Instance.updateTTNV(Convert.ToInt32(tbid.Text), tbten.Text, tbcmnd.Text, tbsdt.Text, tbdiachi.Text,
                                    dateTimePicker1.Value, dateTimePicker2.Value, (int)((CBBItems)comboBox1.SelectedItem).value);
                                D(id,true);
                                this.Dispose();
                            
                        }
                        else
                        {
                            MessageBox.Show("Thông tin thêm chưa hợp lệ");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ngày sinh không hợp lệ : tuổi quá nhỏ!!");

                }
            }
            else
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin!!");
            }
        }

        private void tbcmnd_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tbcmnd.Text, "  ^ [0-9]"))
            {
                tbcmnd.Text = "";
            }
        }

        private void tbcmnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbsdt_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tbsdt.Text, "  ^ [0-9]"))
            {
                tbsdt.Text = "";
            }
        }

        private void tbsdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                byte[] b = ImageToByteArray(pictureBox1.Image);
                if (kh)
                {
                   
                    BLL_kh.Instance.saveHinh(b, id);
                }
                else
                {
                    BLL_NhanVien.Instance.saveHinh(b, id);
                }
                label11.Text = "Đã save ảnh !";

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ( tbten.Text != "" && tbcmnd.Text != "" && tbsdt.Text != "" && tbdiachi.Text != "")
            {
                if (dateTimePicker1.Value < DateTime.Now.AddYears(-9))
                {
                    if (kh)
                    {
                        BLL_kh.Instance.addTTKH(tbten.Text,
                          tbcmnd.Text, tbsdt.Text, tbdiachi.Text, dateTimePicker1.Value);
                        string x = "Thêm thành viên mới";
                        BLL_HoatDong.Instance.commit(idstaff, 0, 3, x);
                        D(id,true);
                        this.Dispose();
                    }
                    else
                    {
                        if (comboBox1.SelectedItem != null )
                        {
                            BLL_NhanVien.Instance.addTTNT( tbten.Text,tbcmnd.Text, tbsdt.Text, tbdiachi.Text,
                                dateTimePicker1.Value, dateTimePicker2.Value, (int)((CBBItems)comboBox1.SelectedItem).value);
                            
                            D(id,true);
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Thông tin thêm chưa hợp lệ");
                        }
                    }    
                }
                else
                {
                    MessageBox.Show("Ngày sinh không hợp lệ : tuổi quá nhỏ!!");
                }
            }
            else
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin!!");
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
