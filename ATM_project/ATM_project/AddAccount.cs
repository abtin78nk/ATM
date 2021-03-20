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
    public partial class AddAccount : Form
    {
        SqlConnection con = new SqlConnection("Data source=(Local);initial catalog=ATM; integrated security=true");
        SqlCommand cmd = new SqlCommand();

        public void Display()
        {
            DataSet Ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "Select * from Accinfo";
            adp.Fill(Ds, "Accinfo");
            AccountsDGV.DataSource = Ds;
            AccountsDGV.DataMember = "Accinfo";

            AccountsDGV.Columns[0].HeaderText = "شماره حساب";
            AccountsDGV.Columns[1].HeaderText = "صاحب حساب";
            AccountsDGV.Columns[2].HeaderText = "رمز حساب";
            AccountsDGV.Columns[3].HeaderText = "موجودی";
            AccountsDGV.Columns[4].HeaderText = "تاریخ انقضا";
        }
        public void DelTextBox()
        {
            AccNumTxt.Text = "";
            NameTxt.Text = "";
            pintxt.Text = "";
            BallanceTxt.Text = "";
            ExpireTxt.Text = "";
        }
        public AddAccount()
        {
            InitializeComponent();
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
            cmd.CommandText = "insert into Accinfo(AccNum,CusName,pin,Ballance,ExpDate)values(@a,@b,@c,@d,@e)";
            cmd.Parameters.AddWithValue("@a", AccNumTxt.Text);
            cmd.Parameters.AddWithValue("@b", NameTxt.Text);
            cmd.Parameters.AddWithValue("@c", pintxt.Text);
            cmd.Parameters.AddWithValue("@d", BallanceTxt.Text);
            cmd.Parameters.AddWithValue("@e", ExpireTxt.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            DelTextBox();
            //MessageBox.Show("Account added");
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            int D = Convert.ToInt32(AccountsDGV.SelectedCells[0].Value);
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "Delete from Accinfo where AccNum=@D";
            cmd.Parameters.AddWithValue("@D", D);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            DelTextBox();
            //MessageBox.Show("Deleted");
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = " update Accinfo set CusName='" + NameTxt.Text + "',Pin='" + pintxt.Text + "',Ballance='" + BallanceTxt.Text + "',ExpDate='" + ExpireTxt.Text + "' where AccNum="+AccNumTxt.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Display();
            DelTextBox();
            //MessageBox.Show("Updated");
        }

        private void AccountsDGV_MouseUp(object sender, MouseEventArgs e)
        {
            AccNumTxt.Text = AccountsDGV[0, AccountsDGV.CurrentRow.Index].Value.ToString();
            NameTxt.Text = AccountsDGV[1, AccountsDGV.CurrentRow.Index].Value.ToString();
            pintxt.Text = AccountsDGV[2, AccountsDGV.CurrentRow.Index].Value.ToString();
            BallanceTxt.Text = AccountsDGV[3, AccountsDGV.CurrentRow.Index].Value.ToString();
            ExpireTxt.Text = AccountsDGV[4, AccountsDGV.CurrentRow.Index].Value.ToString();
        }
        private void AddAccount_Load(object sender, EventArgs e)
        {
            Display();
        }
    }
}
