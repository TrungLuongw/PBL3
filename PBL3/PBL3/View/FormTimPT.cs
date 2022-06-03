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
    public partial class FormTimPT : Form
    {
        public delegate void MyDela(string x);
        public delegate void MyDelHD();
        public MyDela D { get; set; }
        public MyDelHD D1 { get; set; }
        List<IDNgayIDGio> listlt = new List<IDNgayIDGio>();
        int idhd;
        string sta;
        int idstaff;
        public FormTimPT(int idhd,int id)
        {
            InitializeComponent();
            listlt = new List<IDNgayIDGio>();
            this.idhd = idhd;

            idstaff = id;
            setGui();
        }
        private void setGui()
        {
            textBox1.Enabled = false;
            
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
            if (idhd != 0)
            {
                btChon.Enabled = false;
                textBox2.Enabled = false;
                button4.Enabled = false;
                textBox1.Text = idhd.ToString();
                listlt = BLL_HopDong.Instance.ListCheckDateTime(idhd);

                dataGridView1.DataSource = listlt;


            }
            else
            {
                btswap.Enabled = false;
            }



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

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listlt;
        }

        private void btthem_Click(object sender, EventArgs e)
        {
            if (cbday.SelectedItem != null && cbTime.SelectedItem != null)
            {
                IDNgayIDGio i = new IDNgayIDGio
                {
                    idngay = (int)((CBBItems)cbday.SelectedItem).value,
                    ngay = (string)((CBBItems)cbday.SelectedItem).text,
                    idgio = (int)((CBBItems)cbTime.SelectedItem).value,
                    gio = (string)((CBBItems)cbTime.SelectedItem).text
                };
                if (Kiemtra(i))
                {
                    MessageBox.Show("Thời gian này đã có rồi !");
                    return;
                }
                else
                {
                    listlt.Add(i);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = listlt;
                    cbday.SelectedItem = null;
                    cbTime.SelectedItem = null;
                }

            }
        }
        private bool Kiemtra(IDNgayIDGio x)
        {
            foreach (IDNgayIDGio i in listlt)
            {
                if (Convert.ToInt32(i.idgio) == x.idgio && Convert.ToInt32(i.idngay) == x.idngay)
                    return true;
            }


            return false;
        }
        private void remove(string idngay, string idgio)
        {
            foreach (IDNgayIDGio i in listlt)
            {
                if (i.gio.Equals(idgio) && i.ngay.Equals(idngay))
                {
                    listlt.Remove(i);
                    return;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<int> list = new List<int>();
                
                for(int i = dataGridView1.SelectedRows.Count-1; i>-1;i--)
                {
                    list.Add(dataGridView1.SelectedRows[i].Index);
                }
                list = list.OrderByDescending(p=>p).ToList();
                foreach(int i in list)
                {
                    listlt.RemoveAt(i);
                }    
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listlt;
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = BLL_HopDong.Instance.SearchPT(listlt,"").DataSource;
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            int x = dataGridView2.CurrentCell.RowIndex;
            dataGridView1.DataSource = BLL_HopDong.Instance.GetListLichTapByIDPT((int)dataGridView2.Rows[x].Cells[0].Value).DataSource;
        }

        private void btswap_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count==1)
            {
                int idpt =(int) dataGridView2.SelectedRows[0].Cells[0].Value;
                int idHD = Convert.ToInt32(textBox1.Text);
                BLL_HopDong.Instance.swapPT(idpt, idhd);
                BLL_HoatDong.Instance.commitHD(idstaff,idHD, 3, "Thay đổi PT của hợp đồng : "+idhd);
                D1();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Chọn PT muốn đổi");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = BLL_HopDong.Instance.SearchPT(null, textBox2.Text).DataSource;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if(textBox2.Text!="")
            dataGridView2.DataSource = BLL_HopDong.Instance.SearchPT(null, textBox2.Text).DataSource;
        }

        private void btChon_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count==1)
            {
                if (MessageBox.Show(string.Format("Bạn muốn chọn PT này ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    D(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    this.Dispose();
                }    
            }
        }

        
    }
}
