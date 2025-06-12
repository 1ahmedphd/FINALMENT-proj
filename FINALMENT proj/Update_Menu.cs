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
    public partial class Update_Menu : Form
    {
        public Menu menu;
        public Update_Menu(Menu Menu)
        {
            InitializeComponent();

            menu = Menu;

            lblFoodID.Text = menu.FoodID.ToString();
            txtName.Text = menu.Name;
            txtDesc.Text = menu.Description;
            txtPrice.Text = menu.Price.ToString();
            chkAvail.Checked = menu.Availability;
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Menu Item Name Cannot Be Empty. Please provide Menu Item name");
                return;
            }

            try
            {
                // Update the Menu object with new values
                menu.Price = float.Parse(txtPrice.Text); // Convert text to float
                menu.Name = txtName.Text;
                menu.Description = txtDesc.Text;
                menu.Availability = chkAvail.Checked;

                using (SqlConnection conn = new SqlConnection("Server=DESKTOP-BFNOIDM\\SQLEXPRESS01;Database=oop ga;Trusted_Connection=True;"))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Menu] SET [Name] = @Name, [Description] = @Description, " +
                        "[Price] = @Price, [Availability] = @Availability WHERE [FoodID] = @FoodID", conn);

                    cmd.Parameters.AddWithValue("@FoodID", menu.FoodID);
                    cmd.Parameters.AddWithValue("@Name", menu.Name);
                    cmd.Parameters.AddWithValue("@Description", menu.Description);
                    cmd.Parameters.AddWithValue("@Price", menu.Price);
                    cmd.Parameters.AddWithValue("@Availability", menu.Availability);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Menu Data Updated");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for the price.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
