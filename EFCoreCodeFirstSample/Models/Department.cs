using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSample.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
