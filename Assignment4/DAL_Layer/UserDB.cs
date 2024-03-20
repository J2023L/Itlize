using DAL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Layer
{
    public class UserDB
    {
        //connection string (or directly put the connection strings manually here （not recommended)) 

        string conString = ConfigurationManager.ConnectionStrings["adoConString"].ConnectionString;

        //Get all date

        public List<UserDAL> GetUsers()
        {
            List<UserDAL> userList = new List<UserDAL>();

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
                        UserDAL user = new UserDAL
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
        public void AddUser(UserDAL user)
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

        public List<UserDAL> GetUserInfo(string userN)
        {
            List<UserDAL> UserList = new List<UserDAL>();

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
                            UserDAL user = new UserDAL
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
