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
    public partial class FormHoatDong : Form
    {
        public FormHoatDong()
        {
            InitializeComponent();
            setGui();
            setGuiGT();
        }
        private void setGui()
        {
            comboBox1.Items.Add(new CBBItems { value = 0, text = "Tất cả" });
            comboBox1.Items.Add(new CBBItems { value = 1, text = "Gia hạn hợp đồng" });
            comboBox1.Items.Add(new CBBItems { value = 2, text = "Thêm hợp đồng" });
            comboBox1.Items.Add(new CBBItems { value = 2, text = "Hoạt động" });
            comboBox1.SelectedIndex = 0;
            dataGridView1.DataSource = BLL_HoatDong.Instance.ListAllLS(dateTimePicker1.Value,dateTimePicker2.Value).Select(p => new { p.id, p.TTNV.ten, p.ngay,p.tien }).ToList();
            TinhTien();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btthongke_Click(object sender, EventArgs e)
        {
            loadDGV(comboBox1.Text,dateTimePicker1.Value,dateTimePicker2.Value);
            
        }
        private void loadDGV(string x,DateTime t1 , DateTime t2)
        {
            switch(x)
            {
                case "Tất cả":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListAllLS(t1,t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).ToList();
                    break;
                case "Gia hạn hợp đồng":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListLSGH(t1, t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).ToList();
                    break;
                case "Thêm hợp đồng":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListLSADD(t1, t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).ToList();
                    break;
                case "Hoạt động":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListLSCommit(t1, t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).ToList();
                    break;
            }

            TinhTien();
        }
        private void loadDGV2(string x, DateTime t1, DateTime t2)
        {
            switch (x)
            {
                case "Tất cả":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListAllLS(t1, t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).OrderByDescending(p=>p.id).ToList();
                    break;
                case "Gia hạn hợp đồng":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListLSGH(t1, t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).OrderByDescending(p => p.id).ToList();
                    break;
                case "Thêm hợp đồng":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListLSADD(t1, t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).OrderByDescending(p => p.id).ToList();
                    break;
                case "Hoạt động":
                    dataGridView1.DataSource = BLL_HoatDong.Instance.ListLSCommit(t1, t2).Select(p => new { p.id, p.TTNV.ten, p.ngay, p.tien }).OrderByDescending(p => p.id).ToList();
                    break;
            }

            TinhTien();
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
                    textBox1.Text = BLL_HoatDong.Instance.GetText(x);
                }

            }
        }
        private void TinhTien()
        {
            if(dataGridView1.Rows.Count>0)
            {
                double b = 0;
                foreach(DataGridViewRow i in dataGridView1.Rows)
                {
                    b += Convert.ToDouble(i.Cells[3].Value);
                }
                CultureInfo culture = new CultureInfo("vi-VN");
                textBox5.Text = b.ToString("c",culture);
            }    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadDGV2(comboBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadDGV(comboBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Xác Nhận ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {

                        BLL_HoatDong.Instance.Del((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                        loadDGV2(comboBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);

                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==2)
            {
                setGuiTab3();
            }
            if(tabControl1.SelectedIndex == 1)
            {
                loadDGV2();
            }    
        }
        void setGuiTab3()
        {

            comboBox2.Items.Add(new CBBItems { value = 0, text = "Tất cả" });
            comboBox2.Items.Add(new CBBItems { value = 1, text = "Admin" });
            comboBox2.Items.Add(new CBBItems { value = 2, text = "Staff" });
            comboBox3.Items.Add(new CBBItems { value = 1, text = "Admin" });
            comboBox3.Items.Add(new CBBItems { value = 2, text = "Staff" });
            comboBox4.Items.Add(new CBBItems { value = 1, text = "Admin" });
            comboBox4.Items.Add(new CBBItems { value = 2, text = "Staff" });
            
            comboBox2.SelectedIndex = 0;
            comboBox5.Items.Clear();
            foreach (CBBItems i in BLL_Dangnhap.Instance.listNewUser().ToList())
            {
                comboBox5.Items.Add(i);
            }
            loadDGV3();
        }
        void setcbbNV()
        {
            comboBox5.Items.Clear();
            foreach (CBBItems i in BLL_Dangnhap.Instance.listNewUser().ToList())
            {
                comboBox5.Items.Add(i);
            }
            loadDGV3();
        }
        void setTTTK(string  id)
        {
            taikhoan t = BLL_Dangnhap.Instance.GetTkById(id);
            textBox2.Text = t.id;
            textBox3.Text = t.pass;
            textBox4.Text = t.TTNV.ten;
            comboBox3.SelectedIndex =(int)t.TTNV.idchucvu - 1;
            textBox6.Text = textBox3.Text;
            comboBox4.SelectedIndex = (int)t.TTNV.idchucvu - 1;
            
        }
        void setTTTKEmpty()
        {
            
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox3.SelectedIndex =1;
            textBox6.Text = textBox3.Text;
            comboBox4.SelectedIndex = 1;

        }
        void loadDGV3()
        {
            switch (comboBox2.Text)
            {
                case "Tất cả":
                    dataGridView2.DataSource = BLL_Dangnhap.Instance.ListTk(0).Select(p=> new { tk =p.id,name = p.TTNV.ten,type = p.TTNV.idchucvu }).ToList();
                    break;
                case "Admin":
                    dataGridView2.DataSource = BLL_Dangnhap.Instance.ListTk(1).Select(p => new { tk = p.id, name = p.TTNV.ten, type = p.TTNV.idchucvu }).ToList();
                    break;
                case "Staff":
                    dataGridView2.DataSource = BLL_Dangnhap.Instance.ListTk(2).Select(p => new { tk = p.id, name = p.TTNV.ten, type = p.TTNV.idchucvu }).ToList();
                    break;
                default:
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadDGV3();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[x].Selected = true;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                if (dataGridView2.SelectedRows[0].Cells[0].Value != null)
                {

                    string x = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    setTTTK(x);
                }

            }
            textBox6.UseSystemPasswordChar = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                if (dataGridView2.SelectedRows[0].Cells[0].Value != null)
                {

                    string x = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    if (MessageBox.Show("Bạn có muốn xóa ???", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        BLL_Dangnhap.Instance.DelTK(x);
                        loadDGV3();
                        setcbbNV();
                        setTTTKEmpty();
                    }
                }

            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox6.UseSystemPasswordChar == false)
                textBox6.UseSystemPasswordChar = true;
            else
                textBox6.UseSystemPasswordChar =false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                if (dataGridView2.SelectedRows[0].Cells[0].Value != null)
                {

                    string x = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    if (MessageBox.Show("Bạn có muốn reset passwork = 1 ???", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        BLL_Dangnhap.Instance.resetpw(x);
                        setTTTK(x);
                    }
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                if (dataGridView2.SelectedRows[0].Cells[0].Value != null)
                {
                    if (textBox6.Text != textBox3.Text || comboBox4.Text != comboBox3.Text)
                    {
                        string x = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                        int y = ((CBBItems)comboBox4.SelectedItem).value;
                        string z = textBox6.Text;
                        if (MessageBox.Show("Bạn có muốn lưu thay đổi ???", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                        {
                            BLL_Dangnhap.Instance.updatetk(x, y, z);
                            
                            setTTTK(x);
                            loadDGV3();
                            
                        }
                    }
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(comboBox5.SelectedItem!=null&&textBox7.Text!=""&&textBox8.Text!="")
            {

                BLL_Dangnhap.Instance.createTK(textBox8.Text, ((CBBItems)comboBox5.SelectedItem).value, textBox7.Text);
                loadDGV3();
                comboBox5.SelectedItem = null;
                textBox7.Text = "";
                textBox8.Text = "";
                setcbbNV();
            }
            else
            { 
                MessageBox.Show("Điền đẩy đủ thông tin");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                numericUpDown4.Minimum = 0;
                numericUpDown4.Enabled = false;
                numericUpDown4.Value = 0;
            }    
            else
            {
                numericUpDown4.Minimum = 1;
                numericUpDown4.Enabled = true;
                numericUpDown4.Value = 1;
            }    
        }
        void setGuiGT()
        {
            comboBox7.Items.Add("Tất cả");
            comboBox7.Items.Add("Gói tự tập");
            comboBox7.Items.Add("Gói PT");
            comboBox7.SelectedIndex = 0;

        }
        void setGuiGT2(GoiTap g)
        {
            textBox11.Text = g.id.ToString();
            textBox10.Text = g.ten;
            textBox13.Text = g.mota;
            textBox14.Text = g.Hopdongs.Count().ToString();
            if ((int)g.sobuoi > 0)
            {
                numericUpDown1.ReadOnly = false;
                numericUpDown1.Minimum = (int)g.sobuoi;
                numericUpDown1.Value = (int)g.sobuoi;
                
            }
            else
            {
                numericUpDown1.Minimum = 0;
                numericUpDown1.ReadOnly = true;
                numericUpDown1.Value = (int)g.sobuoi;
            }
            
            numericUpDown2.Value = Convert.ToInt32(g.giatien);
            
        }
        void loadDGV2()
        {
            switch (comboBox7.Text)
            {
                case "Tất cả":
                    dataGridView3.DataSource = BLL_HoatDong.Instance.listGoiTap().Select(p => new { p.id, p.ten, p.sobuoi, p.giatien }).ToList();
                    break;
                case "Gói tự tập":
                    dataGridView3.DataSource = BLL_HoatDong.Instance.listGoiTapTT().Select(p => new { p.id, p.ten, p.sobuoi, p.giatien }).ToList();
                    break;
                case "Gói PT":
                    dataGridView3.DataSource = BLL_HoatDong.Instance.listGoiTapPT().Select(p => new { p.id, p.ten, p.sobuoi, p.giatien }).ToList();
                    break;
                default:
                    break;
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            loadDGV2();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView3.CurrentCell.RowIndex;
            dataGridView3.Rows[x].Selected = true;
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView3.SelectedRows.Count==1)
            {
                int x = (int)dataGridView3.SelectedRows[0].Cells[0].Value;
                setGuiGT2(BLL_HoatDong.Instance.GetGoiTap(x));


            }    
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {
                int x = (int)dataGridView3.SelectedRows[0].Cells[0].Value;
                if (textBox10.Text != "" && textBox13.Text != "")
                {
                    BLL_HoatDong.Instance.updateGT(x, textBox10.Text, textBox13.Text, (int)numericUpDown1.Value, (float)numericUpDown2.Value);
                    loadDGV2();
                    
                }
                else
                {
                    MessageBox.Show("Hãy nhập đầy đủ thông tin !");
                }    
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 1)
            {
                int x = (int)dataGridView3.SelectedRows[0].Cells[0].Value;
                if (Convert.ToInt32(textBox14.Text)<1)
                {
                    BLL_HoatDong.Instance.DelGt(x);
                    loadDGV2();

                }
                else
                {
                    MessageBox.Show("Gói tập đang có người sử dụng không thể xóa !");
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(textBox9.Text!=""&&textBox12.Text!="")
            {
                if(BLL_HoatDong.Instance.kiemtraTenGT(textBox9.Text))
                {
                    if(radioButton1.Checked)
                    {
                        BLL_HoatDong.Instance.AddGt(textBox9.Text, textBox12.Text, 0, (float)numericUpDown3.Value);
                    }   
                    else
                    {
                        BLL_HoatDong.Instance.AddGt(textBox9.Text, textBox12.Text, (int)numericUpDown4.Value, (float)numericUpDown3.Value);
                    }
                    textBox9.Text = "";
                    textBox12.Text = "";
                    radioButton1.Checked = true;
                    loadDGV2();
                }   
                else
                {
                    MessageBox.Show("Tên gói tập này có rồi");
                    return;
                }    

            }
            else
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin !");
            }    
        }
    }
}
