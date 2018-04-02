using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEquipmentBLL.Models
{
    public class ProductBO
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TypeOfEquipment { get; set; }

        public string Manufacturer { get; set; }

        public int ModelYear { get; set; }
    }
}
