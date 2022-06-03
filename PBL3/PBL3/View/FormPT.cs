using PBL3.BLL;
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
    public partial class FormPT : Form
    {
        int idstaff;
        public FormPT(int id)
        {
            InitializeComponent();
            idstaff = id;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[x].Selected = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[x].Selected = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                {
                    int x = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    setGui2(x);
                    dataGridView2.DataSource = BLL_NhanVien.Instance.ListHDByIDPT(x).Select(p=>new { p.id,p.TTKH.ten,count = p.lichtaps.Count}).ToList();
                }

            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                if (dataGridView2.SelectedRows[0].Cells[0].Value != null)
                {
                    int x = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value);
                    dataGridView3.DataSource = BLL_NhanVien.Instance.GetLichtapByidHD(x).Select(p => new { p.Ngay.Thu, p.ThoiGian.Time }).ToList();

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_NhanVien.Instance.ListAllPt().Select(p => new { p.id, p.TTNV.ten, p.TTNV.ngaysinh }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = BLL_NhanVien.Instance.ListPtNotCount().Select(p => new { p.id, p.TTNV.ten, p.TTNV.ngaysinh }).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (dataGridView1.SelectedRows[0].Cells[0].Value != null)
                {
                    int x = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    setGui2(x);
                    dataGridView3.DataSource = BLL_NhanVien.Instance.GetalllichtapCuaPT(x).Select(p => new { p.Ngay.Thu, p.ThoiGian.Time }).ToList();
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void setGui2(int x)
        {
            PT d = BLL_kh.Instance.getpt(x);
            if (x != 0)
            {
                tbid.Text = d.id.ToString();
                tbten.Text = d.TTNV.ten;
                tbcmnd.Text = d.TTNV.cmnd;
                tbsdt.Text = d.TTNV.sdt;
                dateTimePicker1.Value = Convert.ToDateTime(d.TTNV.ngaysinh);
                if (d.TTNV.Hinh != null)
                    pictureBox1.Image = ByteArrayToImage((byte[])d.TTNV.Hinh);
                else
                    pictureBox1.Image = null;
                textBox1.Text = d.Hopdongs.Count.ToString();
            }
            
        }
        Image ByteArrayToImage(Byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }
    }
}
