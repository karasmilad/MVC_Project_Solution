using System.ComponentModel.DataAnnotations;
namespace MVVC_Project_PL_.ViewModels.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length Should be 50 Char")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length Should be 50 Char")]
        public string FirstName { get; set; }
           
        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length Should be 50 Char")]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
