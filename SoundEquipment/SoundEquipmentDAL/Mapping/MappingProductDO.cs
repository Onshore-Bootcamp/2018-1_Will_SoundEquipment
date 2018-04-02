using SoundEquipmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEquipmentDAL.Mapping
{
    class MappingProductDO
    {
        private static ProductDO ProductTranslateDatabaseRowToItemInObject(DataRow databaseInput)
        {
            ProductDO output = new ProductDO();

            output.ProductID = (int)databaseInput["ProductID"];
            output.Name = databaseInput["Name"].ToString();
            output.TypeOfEquipment = databaseInput["TypeOfEquipment"].ToString();
            output.Manufacturer = databaseInput["Manufacturer"].ToString();
            output.ModelYear = (int)databaseInput["ModelYear"];

            return output;
        }

        public static List<ProductDO> MakeListOfProductDataObjects(DataTable input)
        {
            List<ProductDO> allProducts = new List<ProductDO>();

            //if (input != null & input.Rows.Count > 0)
            //{
            foreach (DataRow row in input.Rows)
            {
                allProducts.Add(ProductTranslateDatabaseRowToItemInObject(row));
            }
            //}

            return allProducts;
        }

        //For getting only names:
        //public static ProductDO DatabaseNameToObjectNameOnly(DataRow databaseInput)
        //{
        //    ProductDO output = new ProductDO();

        //    output.Name = databaseInput["Name"].ToString();

        //    return output;
        //}

        //public static List<ProductDO> MakeListOfProductNames(DataTable input)
        //{
        //    List<ProductDO> allNames = new List<ProductDO>();

        //    foreach (DataRow row in input.Rows)
        //    {
        //        allNames.Add(DatabaseNameToObjectNameOnly(row));
        //    }

        //    return allNames;
        //}    
    }
}
