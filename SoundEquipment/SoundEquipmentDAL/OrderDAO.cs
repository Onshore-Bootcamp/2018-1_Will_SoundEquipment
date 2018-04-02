using SoundEquipmentDAL.ErrorLog;
using SoundEquipmentDAL.Mapping;
using SoundEquipmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEquipmentDAL
{
    public class OrderDAO
    {
        //Connection string constructor
        public OrderDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Connection string
        private readonly string _connectionString;

        //READ ALL
        public List<OrderDO> ReadAllOrders()
        {
            List<OrderDO> allOrders = new List<OrderDO>();
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;
            DataTable table = new DataTable();
            SqlCommand readAllOrdersCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                readAllOrdersCommand = new SqlCommand("READ_ALL_ORDERS", connection);
                readAllOrdersCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                adapter = new SqlDataAdapter(readAllOrdersCommand);
                adapter.Fill(table);
                allOrders = MappingOrderDO.MakeListOfOrderDataObjects(table);

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
            return allOrders;
        }

        //READ ONE ORDER
        public OrderDO ReadIndividualOrder(int OrderID)
        {
            OrderDO orderDataObject = new OrderDO();
            SqlConnection connection = null;
            SqlCommand readIndividualOrderCommand = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);

                readIndividualOrderCommand = new SqlCommand("READ_INDIVIDUAL_ORDER_BY_ID", connection);
                readIndividualOrderCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                readIndividualOrderCommand.Parameters.AddWithValue("OrderID", OrderID);

                reader = readIndividualOrderCommand.ExecuteReader();
                reader.Read();

                //Pass name?
                orderDataObject.OrderID = (int)reader["OrderID"];
                orderDataObject.Quantity = (int)reader["Quantity"];
                orderDataObject.DateOfOrder = reader["DateOfOrder"].ToString();
                orderDataObject.ExpectedArrivalDate = reader["ExpectedArrivalDate"].ToString();
                orderDataObject.DistributionCenter = reader["DistributionCenter"].ToString();
                orderDataObject.ProductID = (int)reader["ProductID"];
                //There's no product name in the order table:
                //orderDataObject.Name = reader["Name"].ToString();

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

            return orderDataObject;
        }

        //CREATE
        public void CreateOrder(OrderDO orderToCreate)
        {
            SqlConnection connection = null;
            SqlCommand createOrderRowCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                createOrderRowCommand = new SqlCommand("CREATE_ORDER", connection);
                createOrderRowCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter("CREATE_ORDER", connection);

                connection.Open();

                createOrderRowCommand.Parameters.AddWithValue("@Quantity", orderToCreate.Quantity);
                createOrderRowCommand.Parameters.AddWithValue("@DateOfOrder", orderToCreate.DateOfOrder);
                createOrderRowCommand.Parameters.AddWithValue("@ExpectedArrivalDate", orderToCreate.ExpectedArrivalDate);
                createOrderRowCommand.Parameters.AddWithValue("@DistributionCenter", orderToCreate.DistributionCenter);
                createOrderRowCommand.Parameters.AddWithValue("@ProductID", orderToCreate.ProductID);

                createOrderRowCommand.ExecuteNonQuery();
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
        }

        //UPDATE
        public void UpdateOrder(OrderDO orderToUpdate)
        {
            SqlConnection connection = null;
            SqlCommand updateOrderRowCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                updateOrderRowCommand = new SqlCommand("UPDATE_ORDER", connection);
                updateOrderRowCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                updateOrderRowCommand.Parameters.AddWithValue("@OrderID", orderToUpdate.OrderID);
                updateOrderRowCommand.Parameters.AddWithValue("@Quantity", orderToUpdate.Quantity);
                updateOrderRowCommand.Parameters.AddWithValue("@DateOfOrder", orderToUpdate.DateOfOrder);
                updateOrderRowCommand.Parameters.AddWithValue("@ExpectedArrivalDate", orderToUpdate.ExpectedArrivalDate);
                updateOrderRowCommand.Parameters.AddWithValue("@DistributionCenter", orderToUpdate.DistributionCenter);
                updateOrderRowCommand.Parameters.AddWithValue("@ProductID", orderToUpdate.ProductID);

                updateOrderRowCommand.ExecuteNonQuery();
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
        }

        //DELETE
        public void DeleteOrder(int OrderID)
        {
            SqlConnection connection = null;
            SqlCommand deleteOrderCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);

                deleteOrderCommand = new SqlCommand("DELETE_ORDER_ROW", connection);
                deleteOrderCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                deleteOrderCommand.Parameters.AddWithValue("OrderID", OrderID);
                deleteOrderCommand.ExecuteNonQuery();
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
        }
    }
}
