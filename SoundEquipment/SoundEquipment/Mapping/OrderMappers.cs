using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoundEquipment.Models;
using SoundEquipmentBLL.Models;
using SoundEquipmentDAL.Models;

namespace SoundEquipment.Mapping
{
    public class OrderMappers
    {
        public static OrderPO OrderDOtoPO(OrderDO input)
        {
            OrderPO output = new OrderPO();

            output.OrderID = input.OrderID;
            output.Quantity = input.Quantity;
            output.DateOfOrder = input.DateOfOrder;
            output.ExpectedArrivalDate = input.ExpectedArrivalDate;
            output.DistributionCenter = input.DistributionCenter;
            //output.ProductID = input.ProductID;
            output.Name = input.Name;

            return output;
        }

        public static OrderDO OrderPOtoDO(OrderPO input)
        {
            OrderDO output = new OrderDO();

            output.OrderID = input.OrderID;
            output.Quantity = input.Quantity;
            output.DateOfOrder = input.DateOfOrder;
            output.ExpectedArrivalDate = input.ExpectedArrivalDate;
            output.DistributionCenter = input.DistributionCenter;
            output.ProductID = input.ProductID;

            return output;
        }

        public static OrderPO OrderBOtoPO(OrderBO input)
        {
            OrderPO output = new OrderPO();

            output.OrderID = input.OrderID;
            output.Quantity = input.Quantity;
            output.DateOfOrder = input.DateOfOrder;
            output.ExpectedArrivalDate = input.ExpectedArrivalDate;
            output.DistributionCenter = input.DistributionCenter;
            output.ProductID = input.ProductID;

            return output;
        }

        public static OrderBO OrderPOtoBO(OrderPO input)
        {
            OrderBO output = new OrderBO();

            output.OrderID = input.OrderID;
            output.Quantity = input.Quantity;
            output.DateOfOrder = input.DateOfOrder;
            output.ExpectedArrivalDate = input.ExpectedArrivalDate;
            output.DistributionCenter = input.DistributionCenter;
            output.ProductID = input.ProductID;

            return output;
        }

        public static OrderDO OrderBOtoDO(OrderBO input)
        {
            OrderDO output = new OrderDO();

            output.OrderID = input.OrderID;
            output.Quantity = input.Quantity;
            output.DateOfOrder = input.DateOfOrder;
            output.ExpectedArrivalDate = input.ExpectedArrivalDate;
            output.DistributionCenter = input.DistributionCenter;
            output.ProductID = input.ProductID;

            return output;
        }

        public static OrderBO OrderDOtoBO(OrderDO input)
        {
            OrderBO output = new OrderBO();

            output.OrderID = input.OrderID;
            output.Quantity = input.Quantity;
            output.DateOfOrder = input.DateOfOrder;
            output.ExpectedArrivalDate = input.ExpectedArrivalDate;
            output.DistributionCenter = input.DistributionCenter;
            output.ProductID = input.ProductID;

            return output;
        }
    }
}