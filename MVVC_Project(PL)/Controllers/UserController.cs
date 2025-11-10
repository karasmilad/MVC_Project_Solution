using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_DAL_.Models.IdentityModels;
using MVVC_Project_PL_.ViewModels.UserViewModel;

namespace MVVC_Project_PL_.Controllers

{
    [Authorize(Roles = "Admin")]

    public class UserController(UserManager<ApplicationUser> _userManager,IWebHostEnvironment _webHostEnvironment) : Controller
    {
        #region index
        [HttpGet]
        public IActionResult Index(string? saerchValue)
        {
            var UserQuesry = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(saerchValue))
            {
                UserQuesry = UserQuesry.Where(u => u.FirstName.ToLower().Contains(saerchValue) || u.LastName.ToLower().Contains(saerchValue) || u.Email.ToLower().Contains(saerchValue));
            }
            var users = UserQuesry.Select(u => new UserViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                //Roles
            }).ToList();
            foreach (var user in users)
            {
                user.Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;
            }
            return View(users);
        }
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var UserVM = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(UserVM);
        }
        #endregion
        #region Edit
        #region EditGet
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            return View(new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            });
        }
        #endregion
        #region EditPost
        [HttpPost]
        public IActionResult Edit(UserViewModel userVM , string id)
        {
            if(!ModelState.IsValid) return View(userVM);
            if(id != userVM.Id) return BadRequest(); 
            string message = string.Empty;
            try
            {
                var user = _userManager.FindByIdAsync(userVM.Id).Result;
                if (user is null) return NotFound();
                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                user.Email = userVM.Email;
                var result = _userManager.UpdateAsync(user).Result;
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    message = "User Can Not Be Updated";
                }
            }
            catch(Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Somthing Went Wrong Please Try Again Later";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(userVM);
        }
        #endregion
        #endregion
        #region Delete
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user  = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            string message = string.Empty;
            try
            {
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    message = "User Can Not Be Deleted";
                }
            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Somthing Went Wrong Please Try Again Later";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
