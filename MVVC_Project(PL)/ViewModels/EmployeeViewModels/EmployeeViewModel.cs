using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Project_DAL_.Models.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVVC_Project_PL_.ViewModels.EmployeeViewModels
{
    public class EmployeeViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Max Lenght Sholud be 50 Char")]
        [MinLength(5, ErrorMessage = "Min Lenght Sholud be 5 Char")]
        public string Name { get; set; } = null!;
        [Required]
        [Range(24, 60, ErrorMessage = "Age Should be between 24 to 60")]
        public int Age { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]-[a-zA-Z]", ErrorMessage = "Address Be Like Street-City")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public EmployeeTypes EmployeeTypes { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public virtual IEnumerable<SelectListItem>? Departments { get; set; }
    }
}
