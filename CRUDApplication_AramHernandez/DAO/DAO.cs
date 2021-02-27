using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUDApplication_AramHernandez
{
    public class DAO
    {
        public void addorUpdate(int userId, string username, string email, string password, string gender, DateTime selectedDate, int role)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataConfig.ConnectionString))
                {

                    connection.Open();
                    MySqlCommand query = new MySqlCommand("CreateOrEditUser", connection);
                    query.CommandType = CommandType.StoredProcedure;
                    query.Parameters.AddWithValue("_userId", userId.ToString());
                    query.Parameters.AddWithValue("_user_name", username);
                    query.Parameters.AddWithValue("_user_email", email);
                    query.Parameters.AddWithValue("_user_password", password);
                    query.Parameters.AddWithValue("_user_gender", gender);                   
                    query.Parameters.AddWithValue("_user_role", role);        
                    query.Parameters.AddWithValue("_user_dob", selectedDate);
                    query.ExecuteNonQuery();
                    connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                
                Console.Error.Write(ex.Message);
            }
        }
        public DataTable readAll()
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(DataConfig.ConnectionString))
            {
                using (MySqlCommand query = new MySqlCommand())
                {
                    connection.Open();
                    MySqlDataAdapter sqlData = new MySqlDataAdapter("ReadAllUsers", connection);
                    sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlData.Fill(dataTable);                    
                    connection.Close();
                    return dataTable;
                }

            }
            
        }

        public void deleteById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DataConfig.ConnectionString))
            {

                connection.Open();
                MySqlCommand query = new MySqlCommand("DeleteUserById", connection);
                query.CommandType = CommandType.StoredProcedure;
                query.Parameters.AddWithValue("_userId", id);
                query.ExecuteNonQuery();
                connection.Close();

            }
        }
        public DataTable readById(int id)
        {
            DataTable tb = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DataConfig.ConnectionString))
                {
                    connection.Open();
                    MySqlDataAdapter query = new MySqlDataAdapter("ReadUserById", connection);
                    query.SelectCommand.Parameters.AddWithValue("_userId", id);
                    query.SelectCommand.CommandType = CommandType.StoredProcedure;
                    query.Fill(tb);
                    return tb;

                }

               
            }catch(Exception e)
            {
                Console.Out.WriteLine(e);
                return null;
            }

        }
        
    }
}