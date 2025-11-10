using System.ComponentModel.DataAnnotations;
namespace MVVC_Project_PL_.ViewModels.AccountViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Can Not B")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
