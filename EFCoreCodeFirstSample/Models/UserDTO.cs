using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSample.Models
{
    public class LoginUserDTO
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
    public class UserDTO : LoginUserDTO
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<String> Roles { get; set; }

    }
}
