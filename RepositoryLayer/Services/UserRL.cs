using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public UserResponse Registration(UserModel user)
        {
            try
            {
                using (this.sqlConnection)
                {
                    
                    SqlCommand command = new SqlCommand("SP_AddNewUser", this.sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@EmailId", user.EmailId);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                    sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result >= 0)
                    {
                        UserResponse newUser = new UserResponse();
                        newUser.FullName = user.FullName;
                        newUser.EmailId = user.EmailId;
                        newUser.Password = user.Password;
                        newUser.MobileNumber = user.MobileNumber;
         
                        return newUser;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
