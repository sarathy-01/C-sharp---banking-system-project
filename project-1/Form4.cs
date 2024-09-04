using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace project_1
{
    public partial class Form4 : Form
    {
        public int acc_no;
        SqlConnection conn;
        public Form4(int acc_no1)
        {
            acc_no = acc_no1;
            InitializeComponent();
            string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=banking_project;Integrated Security=True";
            conn=new SqlConnection(connectionstring);
            string query1 = "select name,dob,gender,mobile,city from personal where acc_no=@acc_no";
            string query2 = "select pin from account where acc_no=@acc";
            SqlCommand cmd,cmd1;
            using ( cmd = new SqlCommand(query1, conn))
            {
                cmd.Parameters.AddWithValue("@acc_no", acc_no);
            }
            using( cmd1 = new SqlCommand(query2, conn))
            {
                cmd1.Parameters.AddWithValue("@acc", acc_no1);
            }

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader.GetString(0);
                    string dob = reader.IsDBNull(1) ? null : reader.GetString(1);
                    string gender = reader.IsDBNull(2) ? null : reader.GetString(2);
                    string mobile = reader.IsDBNull(3) ? null : reader.GetString(3);
                    string city = reader.IsDBNull(4) ? null : reader.GetString(4);
                    textBox1.Text= name;
                    textBox2.Text= dob;
                    textBox3.Text= gender;  
                    textBox4.Text= mobile;
                    textBox5.Text= city;
                    reader.Close();


                    SqlDataReader reader1=cmd1.ExecuteReader();
                    if(reader1.Read()) 
                    {
                        int pin=reader1.GetInt32(0);
                        textBox6.Text=pin.ToString();
                    }
                    reader1.Close();

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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            
                string query3 = "UPDATE personal SET name=@name, dob=@dob, gender=@gender, mobile=@mobile, city=@city WHERE acc_no=@acc_no";
                
                using (SqlCommand cmd = new SqlCommand(query3, conn))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@dob", textBox2.Text);
                    cmd.Parameters.AddWithValue("@gender", textBox3.Text);
                    cmd.Parameters.AddWithValue("@mobile", textBox4.Text);
                    cmd.Parameters.AddWithValue("@city", textBox5.Text);
                    cmd.Parameters.AddWithValue("@acc_no", acc_no);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Update successful");
                        }
                        else
                        {
                            MessageBox.Show("No rows were updated. Account not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                finally { conn.Close(); }

                }



            string query4 = "UPDATE account SET pin=@pin WHERE acc_no=@acc_no";

            using (SqlCommand cmd = new SqlCommand(query4, conn))
            {
                cmd.Parameters.AddWithValue("@pin", textBox6.Text);
                cmd.Parameters.AddWithValue("@acc_no", acc_no);
                

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Update successful");
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated. Account not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
                finally { conn.Close(); }
            }

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }



}
