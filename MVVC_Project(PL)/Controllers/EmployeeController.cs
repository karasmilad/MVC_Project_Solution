using Microsoft.AspNetCore.Mvc;
using MVC_Project_BLL_.DTOS.EmployeeDTO;
using MVC_Project_BLL_.Services.Interfaces;
using MVC_Project_DAL_.Models.Shared.Enums;

namespace MVVC_Project_PL_.Controllers
{
    public class EmployeeController(IEmployeeService employeeService,IWebHostEnvironment webHostEnvironment,ILogger<EmployeeController> logger): Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly ILogger _logger = logger;
        #region Index
        [HttpGet]

        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees(false);
            return View(Employees);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO createEmployee)
        {
            if(ModelState.IsValid)
            {
                try
                {
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
                catch(Exception ex)
                {
                    if(_webHostEnvironment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
                return View(createEmployee);
        }
        #endregion
        #region Details
        public IActionResult Details(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if(employee == null)
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
            if(!id.HasValue) return BadRequest();
            var Employee = _employeeService.GetEmployeeById(id.Value);
            if(Employee == null) return NotFound();
            var updateEmployee = new UpdateEmployeeDTO
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                Salary = Employee.Salary,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                IsActive = Employee.IsActive,
                HiringDate = (DateTime)Employee.HiringDate,
                Gender = Employee.Gender,
                EmployeeType = Employee.EmployeeTypes
            };
            return View(updateEmployee);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id,UpdateEmployeeDTO updateEmployeeDTO)
        {
            if(!ModelState.IsValid) return View(updateEmployeeDTO);
            if(id != updateEmployeeDTO.Id) return BadRequest();
            try
            {
                int Result = _employeeService.UpdateEmployee(updateEmployeeDTO);
                if(Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Can not Update Employee");
                    return View(updateEmployeeDTO);
                }
            }
            catch(Exception ex)
            {
                if(_webHostEnvironment.IsDevelopment()) 
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
                return View(updateEmployeeDTO);
            }
        }
        #endregion
        [HttpPost]
        #region Delete
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
