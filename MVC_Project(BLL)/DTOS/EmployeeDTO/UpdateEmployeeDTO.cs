using MVC_Project_DAL_.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.DTOS.EmployeeDTO
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Max Lenght Sholud be 50 Char")]
        [MinLength(5, ErrorMessage = "Min Lenght Sholud be 5 Char")]
        public string Name { get; set; } = null!;
        [Required]
        [Range(24, 60, ErrorMessage = "Age Should be between 24 to 60")]
        public int Age { get; set; }
        [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address Be Like 123-Street-City-Country")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "IS Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeTypes? EmployeeType { get; set; }
    }
}
