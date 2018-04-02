using System.ComponentModel.DataAnnotations;

namespace SoundEquipmentDAL.Models
{
    public class UserDO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int RoleID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        //TEST:
        public string PasswordConfirm { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
