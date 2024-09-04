using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Region = new Region(path);
            pictureBox2.Region = new Region(path);
            pictureBox3.Region = new Region(path);
            pictureBox3.TabStop = false;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2= new Form2();
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn;
            SqlCommand cmd;

            string connectionstring = "Data Source=(localdb)\\ProjectModels;Initial Catalog=banking_project;Integrated Security=True";
            int accno = int.Parse(log_acc.Text);
            int pin = int.Parse(log_pin.Text);

            string query = "select pin from account where acc_no= @accno";

            conn=new SqlConnection(connectionstring);
            using(cmd=new SqlCommand(query, conn)) 
            {
                cmd.Parameters.AddWithValue("@accno", accno);
            }

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read()) 
                {
                    int key = reader.GetInt32(0);
                    if (pin == key)
                    {
                        Form3 form3 = new Form3(key, accno);
                        form3.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("PIN  NOT  MATCH");
                    }
                }
               
                reader.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                conn.Close();
                
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
