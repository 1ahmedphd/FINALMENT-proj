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
using System.Xml.Linq;

namespace FINALMENT_proj
{
    public partial class Add_Menu : Form
    {
        public Add_Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=<MSI/SQLEXPRESS>;Initial Catalog=SedapMakan;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Menu] ([Name], " +
                        "[Description],[Price],[Availability]) " +
                        " VALUES (@Name, @Description, @Price, @Availability)", conn);

            Menu emp = new Menu();
            emp.Name = txtName.Text;
            emp.Description = txtDesc.Text;
            emp.Price = float.Parse(txtPrice.Text);
            emp.Availability = chkAvail.Checked;

            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Dept", emp.Description);
            cmd.Parameters.AddWithValue("@Price", emp.Price);
            cmd.Parameters.AddWithValue("@Availability", emp.Availability);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("New Menu Item Added");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
