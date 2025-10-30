using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Project_BLL_.DTOS.EmployeeDTO;
using MVC_Project_BLL_.Services.Interfaces;
using MVC_Project_DAL_.Models.Shared.Enums;
using MVVC_Project_PL_.ViewModels.EmployeeViewModels;

namespace MVVC_Project_PL_.Controllers
{
    public class EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment, ILogger<EmployeeController> logger, IDepartmentService department) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly ILogger _logger = logger;
        private readonly IDepartmentService _department = department;
        #region Index
        [HttpGet]

        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(false);
            if(!string.IsNullOrEmpty(EmployeeSearchName))
            {
                Employees = Employees.Where(e => e.Name.Contains(EmployeeSearchName,StringComparison.OrdinalIgnoreCase));
            }
            return View(Employees);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var Model = new EmployeeViewModel()
            {
                Departments = _department.AllDepartments().Select
                (
                    d => new SelectListItem
                    {
                        Value = d.DeptId.ToString(),
                        Text = d.Name
                    })
            };
            return View(Model);
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createEmployee = new CreateEmployeeDTO
                    {
                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Salary = employeeViewModel.Salary,
                        IsActive = employeeViewModel.IsActive,
                        Email = employeeViewModel.Email,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        HiringDate = employeeViewModel.HiringDate,
                        Gender = employeeViewModel.Gender,
                        EmployeeTypes = employeeViewModel.EmployeeTypes,
                        DepartmentId = employeeViewModel.DepartmentId
                    };
                    int Result = _employeeService.CreateEmployee(createEmployee);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Can not Create Employee");
                        return View(createEmployee);
                    }
                }
                catch (Exception ex)
                {
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(employeeViewModel);
        }
        #endregion
        #region Details
        public IActionResult Details(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var Employee = _employeeService.GetEmployeeById(id.Value);
            if (Employee == null) return NotFound();
            var EmployeeVM = new EmployeeViewModel
            {
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                Salary = Employee.Salary,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                IsActive = Employee.IsActive,
                HiringDate = (DateTime)Employee.HiringDate,
                Gender = Employee.Gender,
                EmployeeTypes = Employee.EmployeeTypes,
                DepartmentId = Employee.DepartmentId
            };
            EmployeeVM.Departments = _department.AllDepartments().Select
            (
                d => new SelectListItem
                {
                    Value = d.DeptId.ToString(),
                    Text = d.Name,
                    Selected = d.DeptId == Employee.DepartmentId
                });
            return View(EmployeeVM);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel EmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                EmployeeVM.Departments = _department.AllDepartments().Select
                (
                    d => new SelectListItem
                    {
                        Value = d.DeptId.ToString(),
                        Text = d.Name,
                        Selected = d.DeptId == EmployeeVM.DepartmentId
                    });
                return View(EmployeeVM);
            }
            ;
            if (!id.HasValue) return BadRequest();
            try
            {
                var updateEmployee = new UpdateEmployeeDTO
                {
                    Id = id.Value,
                    Name = EmployeeVM.Name,
                    Age = EmployeeVM.Age,
                    Address = EmployeeVM.Address,
                    Salary = EmployeeVM.Salary,
                    IsActive = EmployeeVM.IsActive,
                    Email = EmployeeVM.Email,
                    PhoneNumber = EmployeeVM.PhoneNumber,
                    HiringDate = EmployeeVM.HiringDate,
                    DepartmentId = EmployeeVM.DepartmentId,
                    EmployeeTypes = EmployeeVM.EmployeeTypes,
                    Gender = EmployeeVM.Gender
                };
                var Result = _employeeService.UpdateEmployee(updateEmployee);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Can not Update Employee");
                    return View(EmployeeVM);
                }
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
                return View(EmployeeVM);
            }
        }
            #endregion
        #region Delete
            [HttpPost]

            public IActionResult Delete(int id)
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                try
                {
                    bool Result = _employeeService.DeleteEmployee(id);
                    if (Result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Can not Delete Employee");
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        return BadRequest();
                    }
                }
            }
            #endregion
        }
    }