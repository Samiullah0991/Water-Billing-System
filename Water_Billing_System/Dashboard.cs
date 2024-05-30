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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountConsumer();
            CountAgents();
            SumBill();
            countBill();

        }
        SqlConnection con = new SqlConnection(@"Data Source=SAMIULLAH\SQLEXPRESS;Initial Catalog=WaterBillingDatabase;Integrated Security=True");

        private void CountConsumer()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(@"Select count(*)from ConsumerTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Consumerlbl.Text = dt.Rows[0][0].ToString() + " Consumers";
            con.Close();
        }

        private void CountAgents()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(@"Select count(*)from Agenttbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Agentlbl.Text = dt.Rows[0][0].ToString() + " Agents";
            con.Close();
        }
        private void countBill()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(@"select count(*) from BillTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Billbl.Text = dt.Rows[0][0].ToString() + " Bills"; 
            con.Close();
        }

        private void Datebt_ValueChanged(object sender, EventArgs e)
        {
           
        }




        private void label5_Click(object sender, EventArgs e)
        {
            Billing obj = new Billing();
            obj.Show();
            this.Hide();
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

        private void label7_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void Agentlbl_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Datebt_onValueChanged_1(object sender, EventArgs e)
        {
           
        }

        private void Datebt_ValueChanged_1(object sender, EventArgs e)
        {
   
                String Period = Datebt.Value.Month + "/" + Datebt.Value.Year;
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(@"select Sum(Total) from BillTbl where Bperiod ='" + Period + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Billmonth.Text = "Rs" + dt.Rows[0][0].ToString();
                con.Close();

          
        }
        private void SumBill()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(@"Select sum(Total)from BillTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Billmonth.Text = dt.Rows[0][0].ToString() + " Rupees";
            con.Close();
        }
    }
}
