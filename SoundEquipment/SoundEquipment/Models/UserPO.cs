using System.ComponentModel.DataAnnotations;

namespace SoundEquipment.Models
{
    public class UserPO
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int RoleID { get; set; }
        [Required]
        public string Username { get; set; }
        
        //Password can't be required because it can't be updated by the administrator, unless different mapper is used
        public string Password { get; set; }
        //For creating a user:
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirm { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}