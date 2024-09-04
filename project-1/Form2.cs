using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project_1
{
    public partial class Form2 : Form
    {
        int i,j;
        public Form2()
        {
            i = 0;
            j=0;
            InitializeComponent();
        }
        public string name,mobile;
        SqlCommand comm1, comm2;
       static string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=banking_project;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionstring);
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (name1.Text != "" & j==0)
            {
                

                name = name1.Text;
                string dob = dob1.Text;
                string gender = gender1.SelectedItem?.ToString();
                 mobile = mobile1.Text;
                string city = city1.Text;
                string dateOpen = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                // SQL query to insert values into the personal table
                string personal = "INSERT INTO personal (name, dob, gender, mobile, city, date_open) " +
                               "VALUES (@Name, @DOB, @Gender, @Mobile, @City, @DateOpen)";



                using (comm1 = new SqlCommand(personal, conn))
                {
                    comm1.Parameters.AddWithValue("@Name", name);
                    comm1.Parameters.AddWithValue("@DOB", dob);
                    comm1.Parameters.AddWithValue("@Gender", gender);
                    comm1.Parameters.AddWithValue("@Mobile", mobile);
                    comm1.Parameters.AddWithValue("@City", city);
                    comm1.Parameters.AddWithValue("@DateOpen", dateOpen);

                }



                try
                {
                    conn.Open();
                    comm1.ExecuteNonQuery();

                    MessageBox.Show("ACCOUNT CREATED");
                    ++j;
                    ++i;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
            else
            {
                MessageBox.Show("ENTER ALL THE FIELDS");
            }



            SqlCommand cmd1;
            string account1 = "select acc_no from personal where name=@name and mobile=@mobile";
            using (cmd1 = new SqlCommand(account1, conn))
            {
                cmd1.Parameters.AddWithValue("@name", name);
                cmd1.Parameters.AddWithValue("@mobile", mobile);


            }

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();

                if (reader.Read())
                {
                    int no = reader.GetInt32(0);
                    label10.Text=no.ToString();
                    reader.Close();




                }
            }

            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);

            }
            finally { conn.Close(); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(i==1)
            {

                SqlCommand cmd1, cmd2, cmd4;
                string account1 = "select acc_no from personal where name=@name and mobile=@mobile";
                using (cmd1 = new SqlCommand(account1, conn))
                {
                    cmd1.Parameters.AddWithValue("@name", name);
                    cmd1.Parameters.AddWithValue("@mobile", mobile);


                }

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.Read())
                    {
                        int no = reader.GetInt32(0);
                        int pin = int.Parse(pin1.Text);
                        decimal amount = 0;
                        reader.Close();
                        string account2 = "insert into account (acc_no,pin)" + "values(@no,@pin)";
                        using (cmd2 = new SqlCommand(account2, conn))
                        {
                            cmd2.Parameters.AddWithValue("@no", no);
                            cmd2.Parameters.AddWithValue("@pin", pin);
                        }

                        string loan = "insert into loan(acc_no,loan_amount) values(@acc_no,@amount)";
                        using (cmd4 = new SqlCommand(loan, conn))
                        {
                            cmd4.Parameters.AddWithValue("@acc_no", no);
                            cmd4.Parameters.AddWithValue("@amount", amount);
                        }

                        try
                        {
                            cmd2.ExecuteNonQuery();
                            cmd4.ExecuteNonQuery();

                            MessageBox.Show("PIN SETED");

                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Database error: " + ex.Message);


                        }



                    }



                }

                catch (SqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);

                }
                finally { conn.Close(); }

            }
            if(i==0)
            {
                MessageBox.Show("FIRST CREATE ACCOUNT");
            }
            
        }
    }
}
