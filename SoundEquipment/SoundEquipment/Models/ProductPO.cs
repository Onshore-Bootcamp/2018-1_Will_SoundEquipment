using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoundEquipment.Models
{
    public class ProductPO
    {
        [Required]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "    Please enter a product name.")]
        public string Name { get; set; }
        [Required]
        public string TypeOfEquipment { get; set; }

        public string Manufacturer { get; set; }

        public int ModelYear { get; set; }
        
    }
}