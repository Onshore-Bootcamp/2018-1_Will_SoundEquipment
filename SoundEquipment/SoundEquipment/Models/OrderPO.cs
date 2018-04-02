using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SoundEquipment.Models
{
    public class OrderPO
    {
        public OrderPO()
        {
            ProductsDropDown = new List<SelectListItem>();
        }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string DateOfOrder { get; set; }
        public string ExpectedArrivalDate { get; set; }
        [Required]
        public string DistributionCenter { get; set; }
        [Required]
        public int ProductID { get; set; }
        public string Name { get; set; }

        public List<SelectListItem> ProductsDropDown { get; set; }
    }
}