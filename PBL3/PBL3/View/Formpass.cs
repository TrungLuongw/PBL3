using PBL3.BLL;
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
    public partial class Formpass : Form
    {
        int iduser;
        public Formpass(int x)
        {
            InitializeComponent();
            iduser = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Equals(textBox3.Text))
            {
                if(BLL_Dangnhap.Instance.KiemTraPass(iduser,textBox1.Text,textBox2.Text))
                {
                    this.Dispose();
                }    
                else
                {
                    MessageBox.Show("Mật Khẩu cũ không đúng");
                }    
            }
            else
            {
                MessageBox.Show("Mật Khẩu nhập lại không hợp lệ");
            }
        }
    }
}
