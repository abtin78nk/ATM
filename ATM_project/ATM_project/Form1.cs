using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace ATM_project
{
    public partial class Form1 : Form
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
        protected static extern int mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, IntPtr hwndCallback);

        public bool ProcessCDTray(bool open)
        {
            int ret = 0;
            //do a switch of the value passed
            switch (open)
            {
                case true:  //true = open the cd
                    ret = mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
                    return true;
                    break;
                case false: //false = close the tray
                    ret = mciSendString("set cdaudio door closed", null, 0, IntPtr.Zero);
                    return true;
                    break;
                default:
                    ret = mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
                    return true;
                    break;
            }
        }

        SqlConnection con = new SqlConnection("Data source=(Local);initial catalog=ATM; integrated security=true");
        SqlCommand cmd = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
        }

        string Date;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(893, 648);
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            Date = p.GetYear(DateTime.Now).ToString()+"/" + p.GetMonth(DateTime.Now).ToString("0#")+"/" + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Size = new Size(893, 648);
            superTabControl1.SelectedTab = superTabItem2;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        string AccNum;
        int Ballance;
        private void buttonX3_Click(object sender, EventArgs e)
        {
            SqlDataReader At;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Accinfo where Pin=@p";
            cmd.Parameters.AddWithValue("@P", PinTxt.Text);
            con.Open();
            At = cmd.ExecuteReader();

            if (At.Read())
            {
                AccNum = At["AccNum"].ToString();
                Ballance = Convert.ToInt32(At["Ballance"]);
                PinTxt.Text = "";
                superTabControl1.SelectedTab = superTabItem3;
            }
            else
            {
                MessageBox.Show("رمز اشتباه است");
                PinTxt.Text = "";
                PinTxt.Focus();
                con.Close();
                return;

            }
            con.Close();
            
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show("از اینکه بانک مارا انتخاب کردید از شما سپاس گذاریم");
            ProcessCDTray(true);
            superTabControl1.SelectedTab = superTabItem1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                this.Size = new Size(1137, 648);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(893, 648);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("از اینکه بانک مارا انتخاب کردید از شما سپاس گذاریم");
            ProcessCDTray(true);
            superTabControl1.SelectedTab = superTabItem1;
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddAccount().Show();
        }

        private void superTabControlPanel1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BankAcc2().Show();
        }

       

        private void labelX5_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem4;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("از اینکه بانک مارا انتخاب کردید از شما سپاس گذاریم");
            ProcessCDTray(true);
            superTabControl1.SelectedTab = superTabItem1;
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("از اینکه بانک مارا انتخاب کردید از شما سپاس گذاریم");
            ProcessCDTray(true);
            superTabControl1.SelectedTab = superTabItem1;
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("از اینکه بانک مارا انتخاب کردید از شما سپاس گذاریم");
            ProcessCDTray(true);
            superTabControl1.SelectedTab = superTabItem1;
        }
        private void labelX8_Click(object sender, EventArgs e)
        {
            Ballancelbl.Text = Ballance.ToString();
            
            superTabControl1.SelectedTab = superTabItem6;
        }

        private void CathBtn_Click(object sender, EventArgs e)
        {
            
            con.Open();
            int amount = Convert.ToInt32(Cathtxt.Text);
            if (Ballance>=amount)
            {
                int Cath = Ballance - amount;
                string newAmount = "update Accinfo set Ballance='" + Cath + "' where AccNum='" + AccNum + "'";
                SqlCommand sc = new SqlCommand(newAmount, con);
                sc.ExecuteNonQuery();
                MessageBox.Show("برداشت وجه انجام شد");
                Cathtxt.Text = "";
                con.Close();
            }
            else 
            {
                MessageBox.Show("موجودی کافی نیست");
                Cathtxt.Text = "";
                con.Close();
            }
            con.Close();   
        }

        private void adb_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddCathl().Show();
        }
        private void buttonX11_Click(object sender, EventArgs e)
        {
            SqlDataReader At;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Accinfo where Pin=@p";
            cmd.Parameters.AddWithValue("@P", PinTxt.Text);
            con.Open();
            At = cmd.ExecuteReader();

            if (At.Read())
            {
                AccNum = At["AccNum"].ToString();
                Ballance = Convert.ToInt32(At["Ballance"]);
                
            }
            if(Ballance <= Int32.Parse(amtxt.Text))
            {
                MessageBox.Show("موجودی حساب کافی نمیباشد");
                con.Close();
            }
            con.Close();
            //#################################################
            string AccNum2, AccName2;
            SqlDataReader dr1;
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from Accinfo2 where AccNum2=@N";
            cmd.Parameters.AddWithValue("@N", AccN2T.Text);
            con.Open();
            dr1 = cmd.ExecuteReader();
         
            if (dr1.Read())
            {
                AccNum2 = dr1["AccNum2"].ToString();
                AccName2 = dr1["AccName2"].ToString(); 
                l1.Visible = true;
                l2.Visible = true;
                l3.Visible = true;
                l4.Visible = true;
                l5.Visible = true;
                l6.Visible = true;
                l7.Visible = true;
                l8.Visible = true;
                buttonX12.Visible = true;
                l2.Text = AccNum;
                l4.Text = AccNum2;
                l6.Text = AccName2;
                l8.Text = amtxt.Text;
                con.Close();

            }
            else
            {
                MessageBox.Show("شماره کارت اشتباه است");
                AccN2T.Text = "";
                AccN2T.Focus();
                return;
            }
            con.Close();
        }

        private void labelX7_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem5;
        }

        private void buttonX12_Click(object sender, EventArgs e)
        {
            
            con.Open();
                int Cath = Ballance - Convert.ToInt32(l8.Text);
                string newAmount = "update Accinfo set Ballance='" + Cath + "' where AccNum='" + AccNum + "'";
                SqlCommand sc = new SqlCommand(newAmount, con);
                sc.ExecuteNonQuery();
               
                con.Close();
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Insert into TransferInfo(From1,To2,CathTo)values(@a,@b,@c)";
            cmd.Parameters.AddWithValue("@a",l2.Text);
            cmd.Parameters.AddWithValue("@b",l4.Text); 
            cmd.Parameters.AddWithValue("@c",l8.Text);
            con.Open();
            MessageBox.Show("واریز وجه انجام شد");
            l1.Visible = false;
            l2.Visible = false;
            l3.Visible = false;
            l4.Visible = false;
            l5.Visible = false;
            l6.Visible = false;
            l7.Visible = false;
            l8.Visible = false;
            buttonX12.Visible = false;
            amtxt.Text="";
            AccN2T.Text = "";
            cmd.ExecuteNonQuery();
            con.Close();
            


        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem3;
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem3;
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem3;
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            MessageBox.Show("از اینکه بانک مارا انتخاب کردید از شما سپاس گذاریم");
            ProcessCDTray(true);
            superTabControl1.SelectedTab = superTabItem1;
        }

        private void buttonX17_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem3;
        }

        private void labelX6_Click(object sender, EventArgs e)
        {
            superTabControl1.SelectedTab = superTabItem7;
        }

        private void buttonX18_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Update Accinfo set Pin='" + pin2txt.Text + "'where Pin=" + pin1txt.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("رمز تغییر کرد");
            }
            catch (Exception)

            {
                MessageBox.Show("رمز فعلی اشتباه است، لطفا دوباره سعی کنید");
            }
  
        }  
    }
}
