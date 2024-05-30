using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Water_Billing_System
{
    public partial class Agent : Form
    {
        public Agent()
        {
            InitializeComponent();
            ShowAgent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=SAMIULLAH\SQLEXPRESS;Initial Catalog=WaterBillingDatabase;Integrated Security=True");
        private void ShowAgent()
        {
            con.Open();
            String Query = "select * from Agenttbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Agentogv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (Agentname.Text == "" || Agentpassword.Text == "" || Agentphone.Text == "" || Agentaddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Agenttbl(Agname,Agphone,AgAddress,Agpassword) values(@An,@Ap,@Aa,@App)", con);
                    cmd.Parameters.AddWithValue("@An", Agentname.Text);
                    cmd.Parameters.AddWithValue("@Ap", Agentphone.Text);
                    cmd.Parameters.AddWithValue("@Aa", Agentaddress.Text);
                    cmd.Parameters.AddWithValue("@App", Agentpassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Added it");
                    con.Close();
                    ShowAgent();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void Agentogv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Agentname.Text = Agentogv.SelectedRows[0].Cells[1].Value.ToString();
            Agentphone.Text = Agentogv.SelectedRows[0].Cells[2].Value.ToString();
            Agentaddress.Text = Agentogv.SelectedRows[0].Cells[3].Value.ToString();
            Agentpassword.Text = Agentogv.SelectedRows[0].Cells[4].Value.ToString();
            if(Agentname.Text=="")
            {
                Key=0;
            }
            else
            {
                Key = Convert.ToInt32(Agentogv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (Agentname.Text == "" || Agentpassword.Text == "" || Agentphone.Text == "" || Agentaddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update Agenttbl set Agname=@An,Agphone=@Ap,AgAddress=@Aa,Agpassword=@App where Agnumber=@AKey", con);
                    cmd.Parameters.AddWithValue("@An", Agentname.Text);
                    cmd.Parameters.AddWithValue("@Ap", Agentphone.Text);
                    cmd.Parameters.AddWithValue("@Aa", Agentaddress.Text);
                    cmd.Parameters.AddWithValue("@App", Agentpassword.Text);
                    cmd.Parameters.AddWithValue("@AKey",Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Added it");
                    con.Close();
                    ShowAgent();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Reset()
        {
            Agentname.Text = "";
            Agentaddress.Text = "";
            Agentphone.Text = "";
            Agentpassword.Text = "";
            Key = 0;


        }
        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key==0)
            {
                MessageBox.Show("Select The Agent to be Deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Agenttbl where Agnumber=@Akey", con);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent deleted");
                    con.Close();
                    ShowAgent();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Consumer obj = new Consumer();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billing obj = new Billing();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }
    }
}
