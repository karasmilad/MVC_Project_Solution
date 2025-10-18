using MVC_Project_BLL_.DTOS.EmployeeDTO;
using MVC_Project_DAL_.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeesDTO> GetAllEmployees(bool WithTracking);
        EmployeeByIdDTO GetEmployeeById(int id);
        int CreateEmployee(CreateEmployeeDTO employee);
        int UpdateEmployee(UpdateEmployeeDTO employee);
        bool DeleteEmployee(int id); 
    }
}
