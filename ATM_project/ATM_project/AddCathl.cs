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

namespace ATM_project
{
    public partial class AddCathl : Form
    {
        SqlConnection con = new SqlConnection("Data source=(Local);initial catalog=ATM; integrated security=true");
        SqlCommand cmd = new SqlCommand();


       

        public void cleartxt()
        {
            AccNum.Text = "";
            amountxt.Text = "";
            Date.Text = "";
        }

        public AddCathl()
        {
            InitializeComponent();
        }

        private void AddCathl_Load(object sender, EventArgs e)
        {     
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Show();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "insert into settle (AccN,amount,settleDate)values(@a,@b,@c)";
            cmd.Parameters.AddWithValue("@a", AccNum.Text);
            cmd.Parameters.AddWithValue("@b", amountxt.Text);
            cmd.Parameters.AddWithValue("@c", Date.Text);
            con.Open();
            cmd.ExecuteNonQuery();

            //_______________________________________

            string amount1;
            int amount2;
            SqlCommand sqc = new SqlCommand("select Ballance from Accinfo where AccNum='" + AccNum.Text + "'", con);
            amount1 = Convert.ToString((int)sqc.ExecuteScalar());//برای خواندن یک ستون 
            amount2 = Convert.ToInt32(amountxt.Text);
            int Sum = Int32.Parse(amount1) + amount2;
            string newAmount = "update Accinfo set Ballance='" + Sum + "' where AccNum='" + AccNum.Text + "'";
            SqlCommand sc = new SqlCommand(newAmount, con);
            sc.ExecuteNonQuery();
            con.Close();
            cleartxt();
            MessageBox.Show("واریز انجام شد");
        }
    }
}
