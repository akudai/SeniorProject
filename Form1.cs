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

namespace Insert_Update_Delete
{ 
    public partial class InsertUpdateDelete : Form
    {

        SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-3AT0919R\SQLEXPRESS;Integrated Security=True");

        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public InsertUpdateDelete()
        {
            InitializeComponent();
        }

        private void InsertUpdateDelete_Load(object sender, EventArgs e)
        {
            cmd.Connection = cn;
            loadlist();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if(txtInsert.Text != "")
            {
                cn.Open();
                cmd.CommandText = "insert into Level1 (EmpName) values ('" + txtInsert.Text + "')";
                cmd.ExecuteNonQuery();
                cmd.Clone();
                MessageBox.Show("Record Inserted!", "Aamir Kudai's Application");
                cn.Close();
                txtInsert.Text = "";
                loadlist();
            }
        }

        private void loadlist()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            cn.Open();
            cmd.CommandText = "select * from Level1";
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while (dr.Read())
                {
                    listBox1.Items.Add(dr[0].ToString());
                    listBox2.Items.Add(dr[1].ToString());
                }
            }
            cn.Close();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox ls = sender as ListBox;
            if(ls.SelectedIndex != -1)
            {
                listBox1.SelectedIndex = ls.SelectedIndex;
                listBox2.SelectedIndex = ls.SelectedIndex;
                txtId.Text = listBox1.SelectedItem.ToString();
                txtname.Text = listBox2.SelectedItem.ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                cn.Open();
                cmd.CommandText = "delete from Level1 where id='" + txtId.Text + "'";
                cmd.ExecuteNonQuery();
                cmd.Clone();
                MessageBox.Show("Record Deleted!", "Aamir Kudai's Application");
                cn.Close();
                txtId.Text = "";
                txtname.Text = "";
                loadlist();
            }

        }

        private void update_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                cn.Open();
                cmd.CommandText = "update Level1 set EmpName='" + txtname.Text + "' where id='" + txtId.Text+"'";
                cmd.ExecuteNonQuery();
                cmd.Clone();
                MessageBox.Show("Record Updated!", "Aamir Kudai's Application");
                cn.Close();
                txtId.Text = "";
                txtname.Text = "";
                loadlist();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
