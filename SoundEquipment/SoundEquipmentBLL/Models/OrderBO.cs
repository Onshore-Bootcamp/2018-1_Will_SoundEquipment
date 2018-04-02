using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEquipmentBLL.Models
{
    public class OrderBO
    {
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
    }
}
