using SoundEquipment.Models;
using SoundEquipmentBLL.Models;
using SoundEquipmentDAL.Models;

namespace SoundEquipment.Mapping
{
    public class ProductMappers
    {
        public static ProductPO ProductDOtoPO(ProductDO input)
        {
            ProductPO output = new ProductPO();

            output.ProductID = input.ProductID;
            output.Name = input.Name;
            output.TypeOfEquipment = input.TypeOfEquipment;
            output.Manufacturer = input.Manufacturer;
            output.ModelYear = input.ModelYear;

            return output;
        }

        public static ProductDO ProductPOtoDO(ProductPO input)
        {
            ProductDO output = new ProductDO();

            output.ProductID = input.ProductID;
            output.Name = input.Name;
            output.TypeOfEquipment = input.TypeOfEquipment;
            output.Manufacturer = input.Manufacturer;
            output.ModelYear = input.ModelYear;

            return output;
        }

        public static ProductPO ProductBOtoPO(ProductBO input)
        {
            ProductPO output = new ProductPO();

            output.ProductID = input.ProductID;
            output.Name = input.Name;
            output.TypeOfEquipment = input.TypeOfEquipment;
            output.Manufacturer = input.Manufacturer;
            output.ModelYear = input.ModelYear;

            return output;
        }

        public static ProductBO ProductPOtoBO(ProductPO input)
        {
            ProductBO output = new ProductBO();

            output.ProductID = input.ProductID;
            output.Name = input.Name;
            output.TypeOfEquipment = input.TypeOfEquipment;
            output.Manufacturer = input.Manufacturer;
            output.ModelYear = input.ModelYear;

            return output;
        }

        public static ProductDO ProductBOtoDO(ProductBO input)
        {
            ProductDO output = new ProductDO();

            output.ProductID = input.ProductID;
            output.Name = input.Name;
            output.TypeOfEquipment = input.TypeOfEquipment;
            output.Manufacturer = input.Manufacturer;
            output.ModelYear = input.ModelYear;

            return output;
        }

        public static ProductBO ProductDOtoBO(ProductDO input)
        {
            ProductBO output = new ProductBO();

            output.ProductID = input.ProductID;
            output.Name = input.Name;
            output.TypeOfEquipment = input.TypeOfEquipment;
            output.Manufacturer = input.Manufacturer;
            output.ModelYear = input.ModelYear;

            return output;
        }
    }
}