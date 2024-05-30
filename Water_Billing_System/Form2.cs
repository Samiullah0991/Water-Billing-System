using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Water_Billing_System
{
    public partial class Consumer : Form
    {
        public Consumer()
        {
            InitializeComponent();
            ShowConsumer();
        }
        SqlConnection con = new SqlConnection(@"Data Source=SAMIULLAH\SQLEXPRESS;Initial Catalog=WaterBillingDatabase;Integrated Security=True");
        private void ShowConsumer()
        {
            con.Open();
            String Query = "select * from ConsumerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Consumergdv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Reset()
        {
            Cnamebt.Text = "";
            Caddressbt.Text = "";
            Cphonebt.Text = "";
            Ccatogerybt.SelectedIndex = -1;
            Ratebt.Text = "";

        }
        
        
        private void GetRate()
        {
            if (Ccatogerybt.SelectedIndex == 0)
            {
                Ratebt.Text = "70";
            }
            else if (Ccatogerybt.SelectedIndex == 1)
            {
                Ratebt.Text = "95";

            }
            else
            {
                Ratebt.Text = "120";

            }

        }
        int Key = 0;
        private void Ccatogery_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Savebt_Click_1(object sender, EventArgs e)
        {
            if (Cnamebt.Text == "" || Caddressbt.Text == "" || Cphonebt.Text == "" || Ccatogerybt.SelectedIndex ==-1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ConsumerTbl(Cname,Caddress,Cphone,Ccategery,Cdate,Crate) values(@cn,@ca,@cp,@ccc,@cd,@cr)", con);
                    cmd.Parameters.AddWithValue("@cn", Cnamebt.Text);
                    cmd.Parameters.AddWithValue("@ca", Caddressbt.Text);
                    cmd.Parameters.AddWithValue("@cp", Cphonebt.Text);
                    cmd.Parameters.AddWithValue("@ccc", Ccatogerybt.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@cd", Datebt.Value.Date);
                    cmd.Parameters.AddWithValue("@cr", Ratebt.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Consumer Added it");
                    con.Close();
                    ShowConsumer();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        
    }

        private void Editbt_Click_1(object sender, EventArgs e)
        {
            if (Cnamebt.Text == "" || Caddressbt.Text == "" || Cphonebt.Text == "" || Ccatogerybt.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update ConsumerTbl set Cname=@cn,Caddress=@ca,Cphone=@cp,Ccategery=@ccc,Cdate=@cd,Crate=@cr where Cid=@CKey", con);
                    cmd.Parameters.AddWithValue("@cn", Cnamebt.Text);
                    cmd.Parameters.AddWithValue("@ca", Caddressbt.Text);
                    cmd.Parameters.AddWithValue("@cp", Cphonebt.Text);
                    cmd.Parameters.AddWithValue("@ccc", Ccatogerybt.Text);
                    cmd.Parameters.AddWithValue("@cd", Datebt.Value);
                    cmd.Parameters.AddWithValue("@cr", Ratebt.Text);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Consumer Added it");
                    con.Close();
                    ShowConsumer();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Deletebt_Click_1(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Agent to be Deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ConsumerTbl where Cid=@Ckey", con);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Consumer deleted");
                    con.Close();
                    ShowConsumer();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Consumergdv_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Cnamebt.Text = Consumergdv.SelectedRows[0].Cells[1].Value.ToString();
            Caddressbt.Text = Consumergdv.SelectedRows[0].Cells[2].Value.ToString();
            Cphonebt.Text = Consumergdv.SelectedRows[0].Cells[3].Value.ToString();
            Ccatogerybt.Text = Consumergdv.SelectedRows[0].Cells[4].Value.ToString();
            Datebt.Text = Consumergdv.SelectedRows[0].Cells[5].Value.ToString();
            Ratebt.Text = Consumergdv.SelectedRows[0].Cells[6].Value.ToString();
            if (Cnamebt.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(Consumergdv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Agent obj = new Agent();
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

        private void Ccatogerybt_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRate();
        }
    }
}
