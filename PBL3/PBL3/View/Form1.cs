using PBL3.BLL;
using PBL3.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        Point x;
        public Form1()
        {
            InitializeComponent();
            
            x = new Point(this.Location.X,this.Location.Y);
        }
        Form2 f;
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbuser_TextChanged(object sender, EventArgs e)
        {
            if (tbuser.Text!=""&& tbuser.Text!= "Username")
            {
                pictureBox2.BackgroundImage = Properties.Resources.user2;
            }
            else
            {
                pictureBox2.BackgroundImage = Properties.Resources.user;
            }
            tbuser.ForeColor = Color.FromArgb(78, 184, 206);
            panel2.BackColor = Color.FromArgb(78, 184, 206);


            tbpass.ForeColor = Color.White;
            panel3.BackColor = Color.White;
            label2.Text = "";
        }
        private void clos()
        {
            this.Dispose();
        }

        private void tbuser_Click(object sender, EventArgs e)
        {
            if(tbuser.Text== "Username")
            tbuser.Clear();
            tbuser.ForeColor = Color.FromArgb(78, 184, 206);
            panel2.BackColor = Color.FromArgb(78, 184, 206);


            tbpass.ForeColor = Color.White;
            panel3.BackColor = Color.White;
            if(tbpass.Text=="")
            {
                tbpass.Text = "Passwork";
                tbpass.UseSystemPasswordChar = false;
            }



        }

        private void tbpass_Click(object sender, EventArgs e)
        {
            if(tbpass.Text== "Passwork")
            tbpass.Clear();
            tbpass.ForeColor = Color.FromArgb(78, 184, 206);
            panel3.BackColor = Color.FromArgb(78, 184, 206);


            tbuser.ForeColor = Color.White;
            panel2.BackColor = Color.White;
            if(tbuser.Text=="")
            {
                tbuser.Text = "Username";
            }
        }

        private void tbpass_TextChanged(object sender, EventArgs e)
        {
            if (tbpass.Text != "" && tbpass.Text != "Passwork")
            {
                tbpass.UseSystemPasswordChar = true;
                pictureBox3.BackgroundImage = Properties.Resources.pass22;
            }
            else
            {
                pictureBox3.BackgroundImage = Properties.Resources.pass1111;
            }
            tbpass.ForeColor = Color.FromArgb(78, 184, 206);
            panel3.BackColor = Color.FromArgb(78, 184, 206);


            tbuser.ForeColor = Color.White;
            panel2.BackColor = Color.White;
            label2.Text = "";
        }

        private void btSignIn_Click(object sender, EventArgs e)
        {
            this.TopMost = true;

            if (BLL_Dangnhap.Instance.KiemTraDangNhap(tbuser.Text, tbpass.Text))
            {
                f = new Form2(BLL_Dangnhap.Instance.iduser(tbuser.Text, tbpass.Text));
                f.d += new Form2.Mydel(clos);
                
                f.Show();
                timer1.Start();
                
                tbuser.Text = "";
                tbpass.Text = "";
                label2.Text = "";
            }
            else
            {
                tbuser.Text = "";
                tbpass.Text = "";
                label2.Text = "Sai tên tài khoản hoặc  mật khẩu";
            }

           // this.Hide();


            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Left += 30;
            if(this.Left>f.Width+this.Width)
            {
                timer1.Stop();
                this.TopMost = false;
                f.TopMost = true;
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Left -= 20;
            if (this.Left <= 600)
            {


                this.Hide();
                timer2.Stop();
                f.TopMost = false;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
