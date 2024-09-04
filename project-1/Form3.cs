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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace project_1
{
    public partial class Form3 : Form
    {
        public int pin, acc_no;
        SqlConnection conn;

        public Form3(int pin1,int acc_no1 )
        {
            pin = pin1;
            acc_no = acc_no1;
           
            InitializeComponent();
            label2.Text =("[ " +acc_no.ToString() +" ]");
            
             string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=banking_project;Integrated Security=True";
             conn=new SqlConnection(connectionstring);
            string print = "select name,dob,gender,mobile,city from personal where acc_no=@acc_no";
            SqlCommand cmdd;
            using (cmdd = new SqlCommand(print, conn))
            {
                cmdd.Parameters.AddWithValue("@acc_no", acc_no);
            }
            try
            {
                conn.Open();
                SqlDataReader reader = cmdd.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader.GetString(0);
                    string dob = reader.IsDBNull(1) ? null : reader.GetString(1);
                    string gender = reader.IsDBNull(2) ? null : reader.GetString(2);
                    string mobile = reader.IsDBNull(3) ? null : reader.GetString(3);
                    string city = reader.IsDBNull(4) ? null : reader.GetString(4);
                    label8.Text = name;
                    label1.Text = ("Hi  "+name);
                    label9.Text = dob;
                    label10.Text = gender;
                    label11.Text = mobile;
                    label12.Text = city;
                    reader.Close();


                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "select balance from account where acc_no=@acc_no";
            SqlCommand cmd;
            using ( cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@acc_no", acc_no);
            }
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    decimal balance = reader.GetDecimal(0);
                    MessageBox.Show($"Account Balance: {balance}");
                }
                else
                {
                    MessageBox.Show("No matching account found.");
                }


                
                reader.Close();
                conn.Close();


            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal add = decimal.Parse(deposit.Text);
            string query = "update account set balance= balance+@add where acc_no= @acc_no";
            SqlCommand cmd;
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@add", add);
                cmd.Parameters.AddWithValue("@acc_no", acc_no);
            }
            try
            {
                conn.Open();
               

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("DEPOSIT SUCCESSFUL");
                }
                else
                {
                    MessageBox.Show("No rows were updated. Account not found.");
                }

                conn.Close();


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void withdraw_Click(object sender, EventArgs e)
        {
            decimal add = decimal.Parse(withdraw1.Text);
            string query = "update account set balance= balance-@add where acc_no= @acc_no";
            SqlCommand cmd;
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@add", add);
                cmd.Parameters.AddWithValue("@acc_no", acc_no);
            }
            try
            {
                conn.Open();


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("WITHDRAW SUCCESSFUL AND BALANCE UPDATED");
                }
                else
                {
                    MessageBox.Show("No rows were updated. Account not found.");
                }

                conn.Close();


            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int c = this.acc_no;
            Form4 form4 = new Form4(c);
            form4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5=new Form5(acc_no);
            form5.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
