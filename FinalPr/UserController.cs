using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalPr
{
    internal class UserController
    {
        
        static string query = "SELECT UserID, Username, Password FROM Users";
        List<User> users = ReadUsersFromDatabase(query);
        public Boolean register(string username, string password)
        {
            Boolean isValidUser = true;
            foreach (User x in users)
            {
                if (x.Username.Equals(username)) { isValidUser = false; break; }
            }
            if (isValidUser)
            {
                User u = new User(users.Count + 1, username, password);
                userSaving(u);
            }
            return isValidUser;
        }
        
        public void userSaving(User u)
        {
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                    string query = @"insert into users(username, password) values (@username,@password)";
                    

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", u.Id);
                        command.Parameters.AddWithValue("@Username", u.Username);
                        command.Parameters.AddWithValue("@Password", u.Password);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine("Rows affected: " + rowsAffected);
                    }
                
            }
        }
        public User userLogin(string loginUsername, string loginPassword)
        {
            User u = null;
            if (!string.IsNullOrEmpty(loginUsername) && !string.IsNullOrEmpty(loginPassword))
            {
                foreach (User user in users)
                {
                    if (user.Username.Equals(loginUsername) && user.Password.Equals(loginPassword))
                    {
                        u = user;
                        break;
                    }
                }
            }
            return u;
        }
        public User getUserbyUsername(string loginUsername)
        {
            User a = null;
            foreach (User u in users)
            {
                if (u.Username.Equals(loginUsername))
                {
                    a = u;
                    break;
                }
            }
            return a;
        }
        
        private static List<User> ReadUsersFromDatabase(string query)
        {
            List<User> users = new List<User>();
            string connectionString = "Data Source=WIN-CRHM7DRSQOH\\SQLEXPRESS;Initial Catalog=HumanBenchmark;Integrated Security=True;";
            // Read user data from database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.Id = int.Parse(reader["UserId"].ToString());
                    user.Username = reader["Username"].ToString();
                    user.Password = reader["Password"].ToString();
                    users.Add(user);
                };
                reader.Close();
            }
            return users;
        }
    }
}
