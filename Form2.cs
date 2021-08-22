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
using System.Configuration;

namespace WFMyTaskV5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from SalesReps";
            try
            {
                command.Connection = con;
                con.Open();
                SqlDataReader dataReader;
                dataReader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                dataGridView2.DataSource = dt;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandText = $" insert into SalesReps (SalesRepName) values ('{textBox1.Text}')";
            command.Connection = con;
            if (textBox1.Text != "")
            {

                try
                {
                    con.Open();
                    label2.Text = $"{command.ExecuteNonQuery()} تمت العملية بنجاح !! ";
                    Form2_Load(sender, e);
                    textBox1.Text = "";

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
