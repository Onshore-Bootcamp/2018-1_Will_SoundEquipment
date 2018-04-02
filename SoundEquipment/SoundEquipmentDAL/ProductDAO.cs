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
    public class ProductDAO
    {
        //Connection string constructor
        public ProductDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Connection string
        private readonly string _connectionString;

        //READ ALL
        public List<ProductDO> ReadAllProducts()
        {
            List<ProductDO> allProducts = new List<ProductDO>();
            SqlConnection connection = null;
            SqlDataAdapter adapter = null;
            DataTable table = new DataTable();
            SqlCommand readAllProductsCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                readAllProductsCommand = new SqlCommand("READ_ALL_PRODUCTS", connection);
                readAllProductsCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                adapter = new SqlDataAdapter(readAllProductsCommand);
                adapter.Fill(table);
                allProducts = MappingProductDO.MakeListOfProductDataObjects(table);

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

            return allProducts;
        }

        //READ INDIVIDUAL PRODUCT BY ID
        public ProductDO ReadIndividualProduct(int ProductID)
        {
            ProductDO productDataObject = new ProductDO();
            SqlConnection connection = null;
            SqlCommand readIndividualProductCommand = null;
            SqlDataReader reader = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                readIndividualProductCommand = new SqlCommand("READ_INDIVIDUAL_PRODUCT_BY_ID", connection);
                readIndividualProductCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                readIndividualProductCommand.Parameters.AddWithValue("ProductID", ProductID);

                reader = readIndividualProductCommand.ExecuteReader();
                reader.Read();

                productDataObject.ProductID = (int)reader["ProductID"];
                productDataObject.Name = reader["Name"].ToString();
                productDataObject.TypeOfEquipment = reader["TypeOfEquipment"].ToString();
                productDataObject.Manufacturer = reader["Manufacturer"].ToString();
                productDataObject.ModelYear = (int)reader["ModelYear"];

                reader.Close();
            }

            catch (Exception exception)
            {
                ErrorLogger.LogExceptions(exception);
                throw exception;
            }

            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return productDataObject;
        }

        //CREATE
        public void CreateProduct(ProductDO productToCreate)
        {
            SqlConnection connection = null;
            SqlCommand createProductRowCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                createProductRowCommand = new SqlCommand("CREATE_PRODUCT", connection);
                createProductRowCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter("CREATE_PRODUCT", connection);

                connection.Open();

                createProductRowCommand.Parameters.AddWithValue("@Name", productToCreate.Name);
                createProductRowCommand.Parameters.AddWithValue("@TypeOfEquipment", productToCreate.TypeOfEquipment);
                createProductRowCommand.Parameters.AddWithValue("@Manufacturer", productToCreate.Manufacturer);
                createProductRowCommand.Parameters.AddWithValue("@ModelYear", productToCreate.ModelYear);

                createProductRowCommand.ExecuteNonQuery();
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
        public void UpdateProduct(ProductDO productToUpdate)
        {
            SqlConnection connection = null;
            SqlCommand updateProductRowCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                updateProductRowCommand = new SqlCommand("UPDATE_PRODUCT", connection);
                updateProductRowCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                updateProductRowCommand.Parameters.AddWithValue("@ProductID", productToUpdate.ProductID);
                updateProductRowCommand.Parameters.AddWithValue("@Name", productToUpdate.Name);
                updateProductRowCommand.Parameters.AddWithValue("@TypeOfEquipment", productToUpdate.TypeOfEquipment);
                updateProductRowCommand.Parameters.AddWithValue("@Manufacturer", productToUpdate.Manufacturer);
                updateProductRowCommand.Parameters.AddWithValue("@ModelYear", productToUpdate.ModelYear);

                updateProductRowCommand.ExecuteNonQuery();
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
        public void DeleteProduct(int ProductID)
        {
            SqlConnection connection = null;
            SqlCommand deleteProductCommand = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                deleteProductCommand = new SqlCommand("DELETE_PRODUCT", connection);
                deleteProductCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();

                deleteProductCommand.Parameters.AddWithValue("ProductID", ProductID);
                deleteProductCommand.ExecuteNonQuery();
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
