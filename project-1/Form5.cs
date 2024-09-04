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

namespace project_1
{
    public partial class Form5 : Form
    {
        public int acc_no;
        public Form5(int acc_no1)
        {
            acc_no = acc_no1;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal amount=decimal.Parse(textBox1.Text);
            SqlConnection conn;
            string query1 = "select loan_amount from loan where acc_no=@acc_no";
            conn= new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=banking_project;Integrated Security=True");
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query1, conn))
            {
                cmd.Parameters.AddWithValue("acc_no", acc_no);
                using(SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    if (reader.Read()) 
                    {
                        if (reader.GetDecimal(0)==0)
                        {
                            reader.Close();
                            SqlCommand cmd2;
                            string date_loan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string query2 = "update loan set loan_amount=@amount,date_loan=@date_loan where acc_no=@acc_no";
                            using( cmd2 = new SqlCommand( query2, conn))
                            {
                                cmd2.Parameters.AddWithValue("@amount", amount);
                                cmd2.Parameters.AddWithValue("@date_loan", date_loan);
                                cmd2.Parameters.AddWithValue("@acc_no", acc_no);
                            }
                            try
                            {
                                cmd2.ExecuteNonQuery();
                                MessageBox.Show("LOAN AMOUNT GRANTED ");

                            }
                            catch(Exception ex) 
                            {
                                MessageBox.Show(ex.Message);
                            }
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("YOU ALDREADY HAVE ON GOING LOAN");
                        }
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            decimal amount = decimal.Parse(textBox2.Text);
            SqlConnection conn;
            string query1 = "select loan_amount from loan where acc_no=@acc_no";
            conn = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=banking_project;Integrated Security=True");
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query1, conn))
            {
                cmd.Parameters.AddWithValue("acc_no", acc_no);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.GetDecimal(0) !=0)
                        {
                            reader.Close();
                            SqlCommand cmd2;
                            string datepay = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string query2 = "update loan set loan_amount=loan_amount-@amount,pay=@amount,date_pay=@datepay where acc_no=@acc_no";
                            using (cmd2 = new SqlCommand(query2, conn))
                            {
                                cmd2.Parameters.AddWithValue("@amount", amount);
                                cmd2.Parameters.AddWithValue("@acc_no", acc_no);
                                cmd2.Parameters.AddWithValue("@datepay", datepay);
                            }
                            try
                            {
                                cmd2.ExecuteNonQuery();
                                MessageBox.Show("LOAN DUE PAID");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("YOU NOT HAVE NO DUE PENDING FOR LOAN");
                        }
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=banking_project;Integrated Security=True");
            conn.Open();
            SqlCommand comm = new SqlCommand("select* from loan where acc_no="+acc_no.ToString(), conn);
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close( );
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
