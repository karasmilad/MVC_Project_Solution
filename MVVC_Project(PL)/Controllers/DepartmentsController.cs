using Microsoft.AspNetCore.Mvc;
using MVC_Project_BLL_.DTOS;
using MVC_Project_BLL_.Services;
using MVVC_Project_PL_.ViewModels.DepartmentViewModel;

namespace MVVC_Project_PL_.Controllers
{
    public class DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger, IWebHostEnvironment environment) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly ILogger<DepartmentsController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.AllDepartments();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO createdDepartmentDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _departmentService.CreateDepartment(createdDepartmentDTO);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can Not Creaed");
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(createdDepartmentDTO);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentEditViewModel = new DepartmentEditViewModel
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation
            };
            return View(departmentEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, DepartmentEditViewModel departmentEditViewModel)
        {
            try
            {
                if (!id.HasValue) return BadRequest();
                if (!ModelState.IsValid) return View(departmentEditViewModel);
                var updateDepartmentDTO = new UpdateDepartmentDTO
                {
                    DeptId = id.Value,
                    Name = departmentEditViewModel.Name,
                    Code = departmentEditViewModel.Code,
                    Description = departmentEditViewModel.Description,
                    DateOfCreation = departmentEditViewModel.DateOfCreation
                };
                var result = _departmentService.UpdateDepartment(updateDepartmentDTO);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can Not Updated");
                    return View(departmentEditViewModel);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(departmentEditViewModel);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var department = _departmentService.GetDepartmentById(id);
                if (department is null) return NotFound();
                var result = _departmentService.DeleteDepartment(department.DeptId);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can Not Deleted");
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    var department = _departmentService.GetDepartmentById(id);
                    return View(department);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        }
    }
