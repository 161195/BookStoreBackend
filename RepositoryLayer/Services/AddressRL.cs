using CommonLayer.AddressModel;
using CommonLayer.CartModel;
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
    public class AddressRL : IAddressRL
    {
        public static string connectionString = @"Data Source = (localdb)\ProjectsV13;Initial Catalog = BookStoreDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //creating object of sqlconnection class and creating connection with database
        SqlConnection sqlConnection = new SqlConnection(connectionString);

        public AddressResponse AddressAdding(long TypeId, AddressModel model, long UserId)
        {
            try
            {
                SqlConnection sqlConnection1 = new SqlConnection(connectionString);
                string query = "select UserId from UserTable where  UserId=@UserId";
                SqlCommand Validcommand = new SqlCommand(query, sqlConnection1);
                ValidationOfIdForCart Cart = new ValidationOfIdForCart();

                sqlConnection1.Open();        
                Validcommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = Validcommand.ExecuteReader();

                // HasRows Gets a value that indicates whether the SqlDataReader contains one or more rows.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Reads next record in the data reader.               
                        Cart.UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    using (sqlConnection)
                    {

                        SqlCommand command = new SqlCommand("SP_AddressAdd", this.sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TypeId", TypeId);
                        command.Parameters.AddWithValue("@FullName", model.FullName);
                        command.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                        command.Parameters.AddWithValue("@City", model.City);
                        command.Parameters.AddWithValue("@State", model.State);
                        command.Parameters.AddWithValue("@UserId", UserId);

                        sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (result >= 0)
                        {
                            AddressResponse newAddress = new AddressResponse();
                            newAddress.TypeId = TypeId;
                            newAddress.FullName = model.FullName;
                            newAddress.FullAddress = model.FullAddress;
                            newAddress.City = model.City;
                            newAddress.State = model.State;
                            newAddress.UserId = UserId;
                            return newAddress;
                        }
                    }
                }
                sqlConnection1.Close();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<AddressResponse> GetAddress(long UserId)
        {
            try
            {
                List<AddressResponse> responseModel = new();
                SqlCommand command = new("SP_GetAddress", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter dataAdapter = new(command);
                DataTable dataTable = new();
                dataAdapter.Fill(dataTable);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    responseModel.Add(
                         new AddressResponse
                         {
                             TypeId = Convert.ToInt32(dataRow["TypeId"]),
                             FullName = dataRow["FullName"].ToString(),
                             FullAddress = dataRow["FullAddress"].ToString(),
                             City = dataRow["City"].ToString(),
                             State = dataRow["State"].ToString(),
                             UserId = Convert.ToInt32(dataRow["UserId"]),
                         }
                     );
                }
                return responseModel;
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
        public AddressUpdateResponse GetWithAddressId(long AddressId, long UserId)
        {
            try
            {
                AddressUpdateResponse responseModel = new();
                SqlCommand command = new("spGetAddressWithAddressId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                this.sqlConnection.Open();
                command.Parameters.AddWithValue("@AddressId", AddressId);
                command.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        responseModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                        responseModel.TypeId = Convert.ToInt32(reader["TypeId"]);
                        responseModel.FullName = reader["FullName"].ToString();
                        responseModel.FullAddress = reader["FullAddress"].ToString();
                        responseModel.City = reader["City"].ToString();
                        responseModel.State = reader["State"].ToString();
                        responseModel.UserId = Convert.ToInt32(reader["UserId"]);

                    }
                    return responseModel;
                }
                return null;
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
        public AddressUpdateResponse AddressEdit(long AddressId, UpdateModel model, long UserId)
        {
            try
            {
                SqlConnection sqlConnection1 = new(connectionString);
                string query = "select UserId from UserTable where UserId=@UserId ";
                SqlCommand validateCommand = new(query, sqlConnection1);
                ValidationOfIdForCart validationModel = new();

                sqlConnection1.Open();
                validateCommand.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = validateCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        validationModel.UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    using (sqlConnection)
                    {
                        SqlCommand command = new("SP_UpdateAddress", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AddressId", AddressId);
                        command.Parameters.AddWithValue("@TypeId", model.TypeId);
                        command.Parameters.AddWithValue("@FullName", model.FullName);
                        command.Parameters.AddWithValue("@FullAddress", model.FullAddress);
                        command.Parameters.AddWithValue("@City", model.City);
                        command.Parameters.AddWithValue("@State", model.State);
                        command.Parameters.AddWithValue("@UserId", UserId);
                  
                        this.sqlConnection.Open();
                        int result = command.ExecuteNonQuery();
                        this.sqlConnection.Close();
                        if (result >= 0)
                        {                          
                            return GetWithAddressId(AddressId, UserId);
                        }
                    }
                }
                sqlConnection1.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public bool DeletetWithWAddressId(long AddressId, long UserId)
        {
            try
            {
                SqlCommand command = new("SP_DeleteAddressWithAddressId", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AddressId", AddressId);
                command.Parameters.AddWithValue("@UserId", UserId);

                this.sqlConnection.Open();
                int result = command.ExecuteNonQuery();
                if (result >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
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

    }
}
