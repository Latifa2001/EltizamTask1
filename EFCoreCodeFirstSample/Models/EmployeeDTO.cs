using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSample.Models
{
    public class LoginEmployeeDTO
    {

        [Required]
        [DataType(DataType.Text)]
        public string EmployeeName { get; set; }
        public string Department { get; set; } = null!;
        public string? PhotoFileName { get; set; }
        public string? DateOfJoining { get; set; }

    }
    public class UpdateEmployeeDTO : LoginEmployeeDTO
    {
        public int EmployeeId { get; set; }
    }
    public class EmployeeDTO : UpdateEmployeeDTO
    {
        public ICollection<String> Roles { get; set; }
    }
}
