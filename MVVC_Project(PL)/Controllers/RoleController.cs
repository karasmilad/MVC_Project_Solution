using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project_DAL_.Models.IdentityModels;
using MVVC_Project_PL_.ViewModels.RoleViewModel;
using MVVC_Project_PL_.ViewModels.UserViewModel;
using System.Buffers;
using System.Threading.Tasks;

namespace MVVC_Project_PL_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController(RoleManager<IdentityRole> _roleManager,IWebHostEnvironment _webHostEnvironment,UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index(string? saerchValue)
        {
            var RolesQuesry = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(saerchValue))
            {
                RolesQuesry = RolesQuesry.Where(u => u.Name.ToLower().Contains(saerchValue));
            }
            var roles = RolesQuesry.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name,
                //Roles
            }).ToList();
            return View(roles);
        }

        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var role = _roleManager.Roles.Where(r => r.Id == id).Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name,
            }).FirstOrDefault();
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        #endregion
        #region Edit
        #region Edit[Get]
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null)
            {
                return NotFound();
            }
            var usersInRole = await _userManager.Users.ToListAsync();
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Users = usersInRole.Select(u => new UserRoleViewModel()
                {
                    UserId = u.Id,
                    UserName = u.UserName!,
                    IsSelected = _userManager.IsInRoleAsync(u, role.Name).Result
                }).ToList()
            });
        }
        #endregion
        #region Edit[Post]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel roleViewModel , string id)
        {
            if(string.IsNullOrEmpty(id)) return BadRequest();
            if (id != roleViewModel.Id)  return BadRequest();
            if(!ModelState.IsValid) return View(roleViewModel);
            string message = string.Empty;
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;
                if (role == null) return NotFound();
                role.Name = roleViewModel.Name;
                var result = _roleManager.UpdateAsync(role).Result;
                foreach (var userRole in roleViewModel.Users)
                {
                    var user  = await _userManager.FindByIdAsync(userRole.UserId);
                    if(user != null )
                    {
                        if(userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                        {
                            await _userManager.AddToRoleAsync(user, role.Name);
                        }
                        else if(!userRole.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                    }
                }
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                    message = "Failed to update role.";
            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An error occurred while updating the role.";
            }
            return View(roleViewModel);
        }
        #endregion
        #endregion
        #region Create
        #region Create[Get]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion
        #region Create[Post]
        [HttpPost]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid) return View(roleViewModel);
            string message = string.Empty;
            try
            {
                var role = new IdentityRole()
                {
                    Name = roleViewModel.Name,
                };
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                    message = "Failed to create role.";
            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An error occurred while creating the role.";
            }
            return View(roleViewModel);
        }
        #endregion
        #endregion
        #region Delete
        [HttpPost]
        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            string message = string.Empty;
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;
                if (role == null)
                {
                    return NotFound();
                }
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                    message = "Failed to delete role.";
            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An error occurred while deleting the role.";
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
