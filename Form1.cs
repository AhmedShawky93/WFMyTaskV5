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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillCompoBox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select SalesReps.SalesRepName, Pos.PosName, Pos.PosCode, pos.VisitStart, pos.VisitEnd, pos.posID from pos inner join SalesReps on pos.SalesRepId = SalesReps.SalesRepID";
            try
            {
                command.Connection = con;
                con.Open();
                SqlDataReader dataReader;
                dataReader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                dataGridView1.DataSource = dt;

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
        private void fillCompoBox()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandText = "select * from SalesReps";
            command.Connection = con;
            try
            {
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "SalesRepName";
                comboBox1.ValueMember = "SalesRepID";
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
                SqlCommand command = new SqlCommand();
                command.CommandText = $"insert into pos (SalesRepId, posName, posCode, VisitStart, VisitEnd) values ('{comboBox1.SelectedValue}','{textBox1.Text}','{textBox2.Text}','{Picker1.Value}','{Picker2.Value}')";
                command.Connection = con;


                try
                {
                    con.Open();
                    label6.Text = $"{command.ExecuteNonQuery()} تمت العملية بنجاح !! ";
                    Form1_Load(sender, e);
                    textBox1.Text = "";
                    textBox2.Text = "";

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandText = $"select * from pos where posID = '{selectedRow.Index}'";
            command.Connection = con;
            try
            {
                con.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                comboBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                Picker1.Text = selectedRow.Cells[3].Value.ToString();
                Picker2.Text = selectedRow.Cells[4].Value.ToString();
                textBox3.Text = selectedRow.Cells[5].Value.ToString();

                btnAdd.Enabled = false;
                //Form1_Load(sender, e);

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
                SqlCommand command = new SqlCommand();
                command.CommandText = $"update pos set SalesRepId='{comboBox1.SelectedValue}', posName='{textBox1.Text}', posCode='{textBox2.Text}', VisitStart='{Picker1.Value}', VisitEnd='{Picker2.Value}' where posID='{textBox3.Text}'";
                command.Connection = con;
                try
                {
                    con.Open();
                    label6.Text = $"{ command.ExecuteNonQuery()} تمت العملية بنجاح !!";
                    Form1_Load(sender, e);
                    btnAdd.Enabled = true;
                    textBox1.Text = "";
                    textBox2.Text = "";
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnction"].ConnectionString);
            SqlCommand command = new SqlCommand();
            command.CommandText = $"delete from pos where posID='{textBox3.Text}'";
            command.Connection = con;
            try
            {
                con.Open();
                label6.Text = $"{ command.ExecuteNonQuery()} تمت العملية بنجاح !!";
                Form1_Load(sender, e);
                btnAdd.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";
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

        private void btnSalse_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Show();
        }
    }
}
