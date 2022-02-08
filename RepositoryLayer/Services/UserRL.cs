using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        IConfiguration _config;
        public UserRL(IConfiguration config)
        {
            _config = config;
        }
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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
                    command.Parameters.AddWithValue("@Password", encryptpass(user.Password));
                    command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                    sqlConnection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result >= 0)
                    {
                        UserResponse newUser = new UserResponse();
                        newUser.FullName = user.FullName;
                        newUser.EmailId = user.EmailId;
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
        /// <summary>
        /// Gets the login.
        /// </summary>
        /// <param name="User1">The user1.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public string GetLogin(UserLogin User1)
        {
            try
            {
                using (sqlConnection)
                {
                    UserModel detail = new UserModel();
    
                    SqlCommand command = new SqlCommand("SP_Login", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    this.sqlConnection.Open();
                    command.Parameters.AddWithValue("@EmailId", User1.EmailId);
                    command.Parameters.AddWithValue("@Password", encryptpass(User1.Password));
                
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            detail.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                            detail.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                            detail.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                           
                        }
                        string token = GenerateJWTToken(detail.EmailId,detail.UserId);
                        return token;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="EmailId">The email identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        private string GenerateJWTToken(string EmailId,long UserId)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, EmailId),
                        new Claim("UserId",UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public string encryptpass(string Password)
        {
            string msg = "";
            byte[] encode = new byte[Password.Length];
            encode = Encoding.UTF8.GetBytes(Password);
            msg = Convert.ToBase64String(encode); //ToBase64String(Byte[]) Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits
            return msg;
        }
        /// <summary>
        /// Decryptpasses the specified encryptpwd.
        /// </summary>
        /// <param name="encryptpwd">The encryptpwd.</param>
        /// <returns></returns>
        private string Decryptpass(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                using (sqlConnection)
                {
                    UserModel detail = new UserModel();
                    SqlCommand command = new SqlCommand("SP_ForgetPassword", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;

                    this.sqlConnection.Open();
                    command.Parameters.AddWithValue("@EmailId", model.EmailId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            detail.EmailId = Convert.ToString(reader["EmailId"] == DBNull.Value ? default : reader["EmailId"]);
                            detail.UserId = Convert.ToInt32(reader["UserId"] == DBNull.Value ? default : reader["UserId"]);
                        }
                        string token = GenerateJWTToken(detail.EmailId, detail.UserId);
                        new MsmqModel().MsmqSender(token);
                        return token;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public bool ResetPassword(ResetPasswordModel model, string email)
        {
            try
            {
                using (sqlConnection)
                {
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                        SqlCommand command = new SqlCommand("SP_ResetPassword", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@EmailId", email);
                        command.Parameters.AddWithValue("@NewPassword", encryptpass(model.NewPassword));
                        this.sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        this.sqlConnection.Close();
                        if (result >= 0)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


}

