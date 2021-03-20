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
    public partial class BankAcc2 : Form
    {
        SqlConnection con = new SqlConnection("Data source=(Local);initial catalog=ATM; integrated security=true");
        SqlCommand cmd = new SqlCommand();
        public void Display()
        {
            DataSet Ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "Select * from Accinfo2";
            adp.Fill(Ds, "Accinfo2");
            Accinfo2DGV.DataSource = Ds;
            Accinfo2DGV.DataMember = "Accinfo2";
            Accinfo2DGV.Columns[0].HeaderText = "شماره حساب";
            Accinfo2DGV.Columns[1].HeaderText = "صاحب حساب";
            
        }
        public void cleartxt()
        {
            AccNum2Txt.Text = "";
            Name2Txt.Text = "";
        }
        public BankAcc2()
        {
            InitializeComponent();
        }

        private void BankAcc2_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "insert into Accinfo2(AccNum2,AccName2)values(@a,@b)";
            cmd.Parameters.AddWithValue("@a", AccNum2Txt.Text);
            cmd.Parameters.AddWithValue("@b", Name2Txt.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            cleartxt();

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form1().Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            int X = Convert.ToInt32(Accinfo2DGV.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete From Accinfo2 where AccNum2=@X";
            cmd.Parameters.AddWithValue("@X", X);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            cleartxt();
        }

        private void Accinfo2DGV_MouseUp(object sender, MouseEventArgs e)
        {
            AccNum2Txt.Text = Accinfo2DGV[0, Accinfo2DGV.CurrentRow.Index].Value.ToString();
            Name2Txt.Text = Accinfo2DGV[1, Accinfo2DGV.CurrentRow.Index].Value.ToString();
        }
    }
}
