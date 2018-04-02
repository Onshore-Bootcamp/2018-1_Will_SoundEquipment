using SoundEquipmentDAL.Mapping;
using SoundEquipmentDAL.Models;
using SoundEquipmentDAL.ErrorLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SoundEquipmentDAL
{
    public class UserDAO
    {
        //Connection string constructor
        public UserDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Connection string
        private readonly string _connectionString;

        //READ ALL
        public List<UserDO> ReadAllUsers()
        {
            List<UserDO> allUsers = new List<UserDO>();
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;
            DataTable table = new DataTable();
            SqlCommand readAllUsersCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                readAllUsersCommand = new SqlCommand("READ_ALL_USERS", connection);
                readAllUsersCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                adapter = new SqlDataAdapter(readAllUsersCommand);
                adapter.Fill(table);

                allUsers = MappingUserDO.MakeListOfObjects(table);
            }
           
            catch(Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return allUsers;
        }

        //READ USER BY ID
        public UserDO ReadIndividualUserByID(int UserID)
        {
            UserDO userDataObject = new UserDO();
            SqlConnection connection = null;
            SqlCommand readIndividualUserCommand = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);

                readIndividualUserCommand = new SqlCommand("READ_INDIVIDUAL_USER_BY_ID", connection);
                readIndividualUserCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                readIndividualUserCommand.Parameters.AddWithValue("UserID", UserID);

                reader = readIndividualUserCommand.ExecuteReader();
                reader.Read();

                userDataObject.UserID = (int)reader["UserID"];
                userDataObject.RoleID = (int)reader["RoleID"];
                userDataObject.Username = reader["Username"].ToString();
                userDataObject.Password = reader["Password"].ToString();
                userDataObject.FirstName = reader["FirstName"].ToString();
                userDataObject.LastName = reader["LastName"].ToString();
                userDataObject.Email = reader["Email"].ToString();
                userDataObject.PhoneNumber = reader["PhoneNumber"].ToString();

                reader.Close();
            }

            catch(Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return userDataObject;
        }

        //READ USER BY USERNAME
        public UserDO UserLogIn(string Username)
        {
            UserDO userDataObject = new UserDO();
            SqlConnection connection = null;
            SqlCommand readByUsernameCommand = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);

                readByUsernameCommand = new SqlCommand("READ_INDIVIDUAL_USER_BY_USERNAME", connection);
                readByUsernameCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                readByUsernameCommand.Parameters.AddWithValue("Username", Username);

                //Pull the other user attributes from the database
                reader = readByUsernameCommand.ExecuteReader();

                reader.Read();

                userDataObject.UserID = (int)reader["UserID"];
                userDataObject.RoleID = (int)reader["RoleID"];
                userDataObject.Username = reader["Username"].ToString();
                userDataObject.Password = reader["Password"].ToString();
                userDataObject.FirstName = reader["FirstName"].ToString();
                userDataObject.LastName = reader["LastName"].ToString();
                userDataObject.Email = reader["Email"].ToString();
                userDataObject.PhoneNumber = reader["PhoneNumber"].ToString();
                
                reader.Close();
            }

            catch (Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return userDataObject;
        }

        //CREATE
        public void CreateUser(UserDO userToCreate)
        {
            SqlConnection connection = null;
            SqlCommand createUserRowCommand = null;
            
            try
            {
                connection = new SqlConnection(_connectionString);
                createUserRowCommand = new SqlCommand("CREATE_USER", connection);
                createUserRowCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter("CREATE_USER", connection);

                connection.Open();

                createUserRowCommand.Parameters.AddWithValue("@RoleID", userToCreate.RoleID);
                createUserRowCommand.Parameters.AddWithValue("@Username", userToCreate.Username);
                createUserRowCommand.Parameters.AddWithValue("@Password", userToCreate.Password);

                //TEST:
                createUserRowCommand.Parameters.AddWithValue("@PasswordConfirm", userToCreate.Password);

                createUserRowCommand.Parameters.AddWithValue("@FirstName", userToCreate.FirstName);
                createUserRowCommand.Parameters.AddWithValue("@LastName", userToCreate.LastName);
                createUserRowCommand.Parameters.AddWithValue("@Email", userToCreate.Email);
                createUserRowCommand.Parameters.AddWithValue("@PhoneNumber", userToCreate.PhoneNumber);

                createUserRowCommand.ExecuteNonQuery();
            }

            catch(Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        //UPDATE
        public void UpdateUser(UserDO userToUpdate)
        {
            SqlConnection connection = null;
            SqlCommand updateUserRowCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                updateUserRowCommand = new SqlCommand("UPDATE_USER", connection);
                updateUserRowCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                updateUserRowCommand.Parameters.AddWithValue("@UserID", userToUpdate.UserID);
                updateUserRowCommand.Parameters.AddWithValue("@RoleID", userToUpdate.RoleID);
                updateUserRowCommand.Parameters.AddWithValue("@Username", userToUpdate.Username);
                //Password is not updated
                updateUserRowCommand.Parameters.AddWithValue("@FirstName", userToUpdate.FirstName);
                updateUserRowCommand.Parameters.AddWithValue("@LastName", userToUpdate.LastName);
                updateUserRowCommand.Parameters.AddWithValue("@Email", userToUpdate.Email);
                updateUserRowCommand.Parameters.AddWithValue("@PhoneNumber", userToUpdate.PhoneNumber);

                updateUserRowCommand.ExecuteNonQuery();
            }

            catch(Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        //DELETE
        public void DeleteUser(int UserID)
        {
            SqlConnection connection = null;
            SqlCommand deleteUserRowCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                deleteUserRowCommand = new SqlCommand("DELETE_USER", connection);
                deleteUserRowCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                deleteUserRowCommand.Parameters.AddWithValue("UserID", UserID);
                deleteUserRowCommand.ExecuteNonQuery();
            }

            catch(Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
            }

            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
