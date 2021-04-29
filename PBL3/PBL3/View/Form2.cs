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
    public partial class Form2 : System.Windows.Forms.Form
    {
        private Button currentButton;
        private System.Windows.Forms.Form activeForm=null ;

        public Form2()
        {
            InitializeComponent();
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    
                    currentButton = (Button)btnSender;
                    
                    currentButton.ForeColor = Color.FromArgb(40, 40, 40);
                    currentButton.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelMain.BackColor = Color.FromArgb(65, 65, 65);
                    switch (currentButton.Text)
                    {
                        case "Lịch tập    ":
                            currentButton.Image = PBL3.Properties.Resources.scheduleIcon3__1_on;
                            currentButton.BackColor = Color.FromArgb(255, 89, 0); 
                            break;
                        case "Hợp đồng ":
                            currentButton.Image = PBL3.Properties.Resources.contract__1_on;
                            currentButton.BackColor = Color.FromArgb(123, 169, 208);
                            break;
                        case "Thành viên":
                            currentButton.Image = PBL3.Properties.Resources.add_user__1_on;
                            currentButton.BackColor = Color.FromArgb(0, 255, 206);
                            break;
                        case "Nhân viên":
                            currentButton.Image = PBL3.Properties.Resources.team__1_on;
                            currentButton.BackColor = Color.FromArgb(217, 83, 79);
                            break;
                        default:
                            break;
                    }

                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panel2.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(40, 40, 40);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    switch (previousBtn.Text)
                    {
                        case "Lịch tập    ":
                            ((Button)previousBtn).Image = PBL3.Properties.Resources.scheduleIcon3__1_;
                            break;
                        case "Hợp đồng ":
                            ((Button)previousBtn).Image = PBL3.Properties.Resources.contract__1_;
                            break;
                        case "Thành viên":
                            ((Button)previousBtn).Image = PBL3.Properties.Resources.add_user__1_;
                            break;
                        case "Nhân viên":
                            ((Button)previousBtn).Image = PBL3.Properties.Resources.team__1_;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void OpenChildForm(System.Windows.Forms.Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            
            this.panelMain.Controls.Add(childForm);
            this.panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbText.Text = childForm.Text;
            switch (lbText.Text)
            {
                case "Lịch tập":
                    
                    childForm.BackColor = Color.FromArgb(255, 89, 0);
                    break;
                case "Hợp đồng":
                    
                    childForm.BackColor = Color.FromArgb(123, 169, 208);
                    break;
                case "Thành viên":
                    
                    childForm.BackColor = Color.FromArgb(0,255,206);
                    break;
                case "Nhân viên":
                    
                    childForm.BackColor = Color.FromArgb(217,83,79);
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormLichTap(), sender);
        }      
        

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormHopDong(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThanhVien(), sender);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhanVien(), sender);
        }
    }
}
