using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSample.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string? PhotoFileName { get; set; }
        public string? DateOfJoining { get; set; }

    }
}