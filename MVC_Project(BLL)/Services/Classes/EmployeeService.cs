using AutoMapper;
using MVC_Project_BLL_.DTOS.EmployeeDTO;
using MVC_Project_BLL_.Services.Interfaces;
using MVC_Project_DAL_.Models.EmployeeModel;
using MVC_Project_DAL_.Repositories.Classes;
using MVC_Project_DAL_.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services.Classes
{
    public class EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMapper _mapper = mapper;

        public IEnumerable<EmployeesDTO> GetAllEmployees(bool WithTracking = false)
        {
            var Employees = _employeeRepository.GetAll(WithTracking);
            var EmployeesDTO = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeesDTO>>(Employees);
            return EmployeesDTO;
        }

        public EmployeeByIdDTO GetEmployeeById(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            return Employee is null ? null : _mapper.Map<Employee, EmployeeByIdDTO>(Employee);
        }

        public int CreateEmployee(CreateEmployeeDTO employee)
        {
            var Employee = _mapper.Map<CreateEmployeeDTO, Employee>(employee);
            return _employeeRepository.Add(Employee);
        }

        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetById(id);
            if(Employee is null)
                return false;
            else
            {
                Employee.IsDeleted = true;
                return _employeeRepository.Delete(Employee) > 0 ? true : false;
            }
        }

        public int UpdateEmployee(UpdateEmployeeDTO employee)
        {
            var Employee = _mapper.Map<UpdateEmployeeDTO, Employee>(employee);
            return _employeeRepository.Update(Employee);
        }
    }
}
