using SoundEquipmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEquipmentDAL.Mapping
{
    class MappingOrderDO
    {
        private static OrderDO OrderTranslateDatabaseRowToItemInObject(DataRow databaseInput)
        {
            OrderDO output = new OrderDO();

            output.OrderID = (int)databaseInput["OrderID"];
            output.Quantity = (int)databaseInput["Quantity"];
            output.DateOfOrder = databaseInput["DateOfOrder"].ToString();
            output.ExpectedArrivalDate = databaseInput["ExpectedArrivalDate"].ToString();
            output.DistributionCenter = databaseInput["DistributionCenter"].ToString();
            //Here I think productid has to be replaced with the product name:
            //output.ProductID = (int)databaseInput["ProductID"];
            output.Name = databaseInput["Name"].ToString();

            return output;
        }

        public static List<OrderDO> MakeListOfOrderDataObjects(DataTable input)
        {
            List<OrderDO> allOrders = new List<OrderDO>();

            if (input != null & input.Rows.Count > 0)
            {
                foreach (DataRow row in input.Rows)
                {
                    allOrders.Add(OrderTranslateDatabaseRowToItemInObject(row));
                }
            }
            return allOrders;
        }
    }
}
