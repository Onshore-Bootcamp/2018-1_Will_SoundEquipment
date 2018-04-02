using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoundEquipment.Models
{
    public class LoginPO
    {
        [Required(ErrorMessage = "   Please enter a username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "   Please enter a password.")]
        public string Password { get; set; }
    }
}