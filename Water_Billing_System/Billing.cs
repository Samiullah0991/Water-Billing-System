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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Water_Billing_System
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            ShowBillings();
            GetCons();
            Userlbl.Text = login.User;
        }
        SqlConnection con = new SqlConnection(@"Data Source=SAMIULLAH\SQLEXPRESS;Initial Catalog=WaterBillingDatabase;Integrated Security=True");
      
        private void ShowBillings()
        {
            con.Open();
            string Query = "select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Billinggdv.DataSource = ds.Tables[0];
            con.Close();
        }
        private void GetCons()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select Cid From ConsumerTbl",con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Cid", typeof(int));
            dt.Load(Rdr);
            Cidbt.ValueMember = "Cid";
            Cidbt.DataSource = dt;
            con.Close();
            
        }
        private void GetConsRate()
        {
            con.Open();
            String Query = "select * from ConsumerTbl where Cid="+ Cidbt.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda=new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Ratebt.Text = dr["Crate"].ToString();
            }

            con.Close();   
        }
        private void Reset()
        {
            Cidbt.SelectedIndex = -1;
            Ratebt.Text = "";
            Taxxbt.Text = "";
            Consuptionbt.Text = "";
        }

       

        private void Savebt_Click(object sender, EventArgs e)
        {
            if (Ratebt.Text == ""|| Consuptionbt.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    int R;
                    double Total, Consuption, Tax;

                    if (!int.TryParse(Ratebt.Text, out R))
                    {
                        MessageBox.Show("Invalid Rate input");
                        return;
                    }

                    if (!double.TryParse(Consuptionbt.Text, out Consuption))
                    {
                        MessageBox.Show("Invalid Consumption input");
                        return;
                    }

                    if (!double.TryParse(Taxxbt.Text, out Tax))
                    {
                        MessageBox.Show("Invalid Tax input");
                        return;
                    }

                    double taxAmount;

                    if (Consuption > 200)
                    {
                        taxAmount = Consuption * 0.15;
                    }
                    else if (Consuption > 100)
                    {
                        taxAmount = Consuption * 0.10;
                    }
                    else
                    {
                        taxAmount = Consuption * 0.05;
                    }

                    Taxxbt.Text = taxAmount.ToString("");

                    Tax = (int)(R * Consuption * (Tax / 100.0));
                    Total = (R * Consuption) - Tax;
                    string Period = Bperiodbt.Value.Month + " / " + Bperiodbt.Value.Year;

                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(Cid,Agent,Bperiod,Consuption,Rate,Tax,Total) values(@ci,@ag,@bp,@c,@r,@t,@tt)", con);
                    cmd.Parameters.AddWithValue("@ci", Cidbt.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@ag", Userlbl.Text);
                    cmd.Parameters.AddWithValue("@bp", Period);
                    cmd.Parameters.AddWithValue("@c", Consuptionbt.Text);
                    cmd.Parameters.AddWithValue("@r", Ratebt.Text);
                    cmd.Parameters.AddWithValue("@t", Taxxbt.Text);
                    cmd.Parameters.AddWithValue("@tt", Total);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Added");
                    con.Close();
                    ShowBillings();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }






        private void Cidbt_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Agent obj = new Agent();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Consumer obj = new Consumer();
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
        int Key = 0;
        private void Billinggdv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
                Cidbt.Text = Billinggdv.SelectedRows[0].Cells[1].Value.ToString();

                Bperiodbt.Text = Billinggdv.SelectedRows[0].Cells[3].Value.ToString();
                Consuptionbt.Text = Billinggdv.SelectedRows[0].Cells[4].Value.ToString();
                Ratebt.Text = Billinggdv.SelectedRows[0].Cells[5].Value.ToString();
                Taxxbt.Text = Billinggdv.SelectedRows[0].Cells[6].Value.ToString();
               
                if (Cidbt.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(Billinggdv.SelectedRows[0].Cells[0].Value.ToString());
                }
            
        }

        private void Agentbt_Click(object sender, EventArgs e)
        {

        }

        private void Editbt_Click(object sender, EventArgs e)
        {
            if (Ratebt.Text == "" || Bperiodbt.Text == "" || Consuptionbt.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    int R;
                    double Total, Consuption, Tax;

                    if (!int.TryParse(Ratebt.Text, out R))
                    {
                        MessageBox.Show("Invalid Rate input");
                        return;
                    }

                    if (!double.TryParse(Consuptionbt.Text, out Consuption))
                    {
                        MessageBox.Show("Invalid Consumption input");
                        return;
                    }

                    if (!double.TryParse(Taxxbt.Text, out Tax))
                    {
                        MessageBox.Show("Invalid Tax input");
                        return;
                    }

                    double taxAmount;

                    if (Consuption > 200)
                    {
                        taxAmount = Consuption * 0.15;
                    }
                    else if (Consuption > 100)
                    {
                        taxAmount = Consuption * 0.10;
                    }
                    else
                    {
                        taxAmount = Consuption * 0.05;
                    }

                    Taxxbt.Text = taxAmount.ToString("");

                    Tax = (int)(R * Consuption * (Tax / 100.0));
                    Total = (R * Consuption) - Tax;
                    string Period = Bperiodbt.Value.Month + " / " + Bperiodbt.Value.Year;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE BillTbl SET Cid=@ci, Bperiod=@bp, Consuption=@c, Rate=@r, Tax=@t, Total=@tt", con);
                    cmd.Parameters.AddWithValue("@ci", Cidbt.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@bp", Period);
                    cmd.Parameters.AddWithValue("@c", Consuptionbt.Text);
                    cmd.Parameters.AddWithValue("@r", Ratebt.Text);
                    cmd.Parameters.AddWithValue("@t", Taxxbt.Text);
                  
                    cmd.Parameters.AddWithValue("@tt", Total);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Updated");
                    con.Close();
                    ShowBillings();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void Cidbt_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetConsRate();
        }
       
    private void textBox1_TextChanged(object sender, EventArgs e)
       {
       
    }

        private void Consuptionbt_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Deletebt_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("delete from BillTbl where Bnum=@Bkey", con);
                    cmd.Parameters.AddWithValue("@BKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Consumer deleted");
                    con.Close();
                    ShowBillings();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
