using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALMENT_proj
{
    public class User
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
        public string username;
        public string name;
        public string permissions;

        public User(string Useranme, string Name, string Permissions)
        {
            username = Useranme;
            name = Name;
            permissions = Permissions;
            
        }

        public void Refresh(string Username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlQuery = "Select * from Users where username = @username";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", Username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username = reader["username"].ToString();
                            name = reader["name"].ToString();
                            permissions = reader["permissions"].ToString();
                        }
                    }
                }
            }
        }

    }

}
