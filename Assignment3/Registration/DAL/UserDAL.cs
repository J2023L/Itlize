using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Registration.Models;
using System.Net.Mail;

namespace Registration.DAL
{
    public class UserDAL
    {
        //connection string (or directly put the connection strings manually here （not recommended)) 

        string conString = ConfigurationManager.ConnectionStrings["adoConString"].ConnectionString;

        //Get all date

        public List<User> GetUsers()
        {
            List<User> userList = new List<User>();

            //DataReader

            using (SqlConnection conn = new SqlConnection(conString))
            {


                string sqlQuery = "SELECT UserId, UserName, Email, PW FROM Users";

                SqlCommand command = new SqlCommand(sqlQuery, conn);

                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserName = reader["UserName"].ToString(),
                            EmailAddress = reader["Email"].ToString(),
                            Password = reader["PW"].ToString()
                        };
                        userList.Add(user);
                    }
                }
            }


            //DataSet DataAdapter


            //using (SqlConnection conn = new SqlConnection(conString))
            //{
            //    string sqlQuery = "SELECT UserId, UserName, Email, PW FROM Users";

            //    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, conn);
            //    DataSet dataSet = new DataSet();
            //    dataAdapter.Fill(dataSet, "Users");


            //    DataTable usersTable = dataSet.Tables["Users"];

            //    foreach (DataRow row in usersTable.Rows)
            //    {
            //        User user = new User
            //        {
            //            UserName = reader["UserName"].ToString(),
            //            EmailAddress = reader["Email"].ToString(),
            //            Password = reader["PW"].ToString()
            //        };

            //        userList.Add(user);
            //    }
            //}

            return userList;
        }

        // add data
        public void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                string sqlQuery = "INSERT INTO Users (UserName, Email, PW) VALUES (@UserName, @Email, @PW)";
                SqlCommand command = new SqlCommand(sqlQuery, conn);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.EmailAddress);
                command.Parameters.AddWithValue("@PW", user.Password);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        // Store Procedure 

        public List<User> GetUserInfo(string userN)
        {
            List<User> UserList = new List<User>();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("GetUserInfo", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserN", SqlDbType.NVarChar, 100) { Value = userN });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            User user = new User
                            {
                                UserName = reader["UserName"].ToString(),
                                EmailAddress = reader["Email"].ToString(),
                                Password = reader["PW"].ToString()
                            };

                            UserList.Add(user);
                        }
                    }
                }
            }

            return UserList;
        }
    }
}
