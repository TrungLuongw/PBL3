using PBL3.BLL;
using PBL3.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3.View
{
    public partial class FormNhanVien : Form
    {
        int idstaff;
        public FormNhanVien(int id)
        {
            InitializeComponent();
            setGui();
            idstaff = id;
            
        }
        public void setGui()
        {
            foreach(CBBItems i in BLL_NhanVien.Instance.listCV())
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.Enabled = false;
            button1.Hide();
        }
        public void setGui2(int x,bool check)
        {
            if (x != 0)
            {
                TTNV d = BLL_NhanVien.Instance.GetTTNV(x);
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
                dateTimePicker2.Value = Convert.ToDateTime(d.ngayvao);
                
                comboBox1.SelectedIndex = (int)d.idchucvu - 1;
            }
            if(check)
            {
                LoadDGV(label7.Text);
            }
        }
        private void LoadDGV(string x)
        {
            switch (x)
            {
                case "Nhân viên ":

                    dataGridView1.DataSource = BLL_NhanVien.Instance.listNV().Select(p => new { p.id, p.ten, p.ngaysinh }).ToList();
                    textBox1.Text =  dataGridView1.Rows.Count.ToString();
                    break;
                case "Nhân viên PT":

                    dataGridView1.DataSource = BLL_NhanVien.Instance.listNVPT().Select(p => new { p.id, p.ten, p.ngaysinh }).ToList();
                    textBox1.Text = dataGridView1.Rows.Count.ToString();
                    break;
                
                default:
                    break;
            }
            label8.Text = "";
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

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDGV(button2.Text);
            label7.Text = button2.Text;
            button1.Show();
            button8.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDGV(button3.Text);
            label7.Text = button3.Text;
            button1.Hide();
            button8.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[x].Selected = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            label8.Text = "";
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                {
                    int x = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    setGui2(x,false);
                }

            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (pictureBox1.Image != null)
                {
                    byte[] b = ImageToByteArray(pictureBox1.Image);
                    BLL_NhanVien.Instance.saveHinh(b, (int)dataGridView1.SelectedRows[0].Cells[0].Value);
                    setGui2((int)dataGridView1.SelectedRows[0].Cells[0].Value,false);
                    label8.Text = "Đã lưu...";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("bạn có chắc muốn thêm vào bảng PT ? ", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BLL_NhanVien.Instance.addPT((int)dataGridView1.SelectedRows[0].Cells[0].Value);
                    LoadDGV(label7.Text);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("bạn có chắc muốn cắt chức PT ???? ", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                   if( MessageBox.Show("","Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    BLL_NhanVien.Instance.delPT((int)dataGridView1.SelectedRows[0].Cells[0].Value);
                    LoadDGV(label7.Text);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ADDTV f = new ADDTV(0, false,idstaff);
            f.D += new ADDTV.Mydel(setGui2);
            f.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {

                    if (MessageBox.Show(string.Format("Bạn có thật sự muốn xóa Nhân viên : {0} ?", dataGridView1.SelectedRows[i].Cells[1].Value), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        if (MessageBox.Show("", "Xác nhận lại ", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                            BLL_NhanVien.Instance.DelTTNV((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                        
                    }

                }
                LoadDGV(label7.Text);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                ADDTV f = new ADDTV((int)dataGridView1.SelectedRows[0].Cells[0].Value, false,idstaff);
                f.D += new ADDTV.Mydel(setGui2);
                f.ShowDialog();
                this.Show();
            }
        }

        
    }
}
