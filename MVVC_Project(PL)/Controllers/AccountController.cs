using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_BLL_.Services.EmailSender;
using MVC_Project_DAL_.Models.IdentityModels;
using MVC_Project_DAL_.Models.Shared;
using MVVC_Project_PL_.ViewModels.AccountViewModel;

namespace MVVC_Project_PL_.Controllers
{

    public class AccountController(UserManager<ApplicationUser> _userManager
                                  ,SignInManager<ApplicationUser> _signIn
                                  ,IEmailSender _emailSender)
                                  : Controller
    {
        //Register
        #region Register
        #region Register[Get]
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
        #endregion
        #region Register[Post]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var user = new ApplicationUser()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                
            };
            var result =  _userManager.CreateAsync(user, registerViewModel.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerViewModel);
            }
        }
        #endregion
        #endregion
        //Login
        #region Login
        #region Login[Get]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        #endregion
        #region Login[Post]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            //1] Find User by Email
            //2] Check User == null
            //3] Check Password
            //4] SignIn User
            //5] Check if Account Is Locked
            var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (user != null)
            {
                var flag = _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                if (flag)
                {
                    //User With Email Correct Password
                    var result = _signIn.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Not Allowed to SignIn");
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is Locked");
                    }
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Email or Password");
            return View(loginViewModel);
        }
        #endregion
        #endregion
        //SignOut
        #region SignOut
        [HttpGet]
        public new IActionResult SignOut()
        {
            _signIn.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }
        #endregion
        //ForgrtPassword
        #region ForgetPassword
        #region ForgetPassword
        public IActionResult Forgetpassword()
        {
            return View();
        }
        #endregion
        #region SendResetPasswordUrl
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(forgetPasswordViewModel.Email).Result;
                if(user != null)
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var url = Url.Action("ResetPassword","Account",new {Email = forgetPasswordViewModel.Email , token = token},Request.Scheme);
                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset Your Password",
                        Body = url
                    };
                    _emailSender.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email Address");
                }
            }
            return View(forgetPasswordViewModel);
        }
        #endregion
        #region CheckInbox
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #endregion
        #region ResetPassword[Get]
        [HttpGet]
        public IActionResult ResetPassword(string Email , string token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = token;
            return View();
            //Pass Email and Token to View
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var token = TempData["Token"] as string;
            var email = TempData["Email"] as string;
            var user = _userManager.FindByEmailAsync(email).Result;
            if (ModelState.IsValid)
            {
                var result = _userManager.ResetPasswordAsync(user,token,resetPasswordViewModel.NewPassword).Result;
                if(result.Succeeded)
                    return RedirectToAction("Login");
            }
            ModelState.AddModelError(string.Empty, "Error Occured While Reseting Your Password");
            return View(resetPasswordViewModel);
        }
        #endregion
        #endregion
    }
}
