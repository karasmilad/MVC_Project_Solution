using AutoMapper;
using MVC_Project_BLL_.DTOS.EmployeeDTO;
using MVC_Project_BLL_.Services.Attachment_Service;
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
    public class EmployeeService(IUnitOfWork unitOfWork,IMapper mapper,IAttachmentService attachmentService) : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IAttachmentService _attachmentService = attachmentService;

        public IEnumerable<EmployeesDTO> GetAllEmployees(bool WithTracking = false)
        {
            var Employees = _unitOfWork.EmployeeRepository.GetAll(WithTracking);
            var EmployeesDTO = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeesDTO>>(Employees);
            return EmployeesDTO;
        }

        public EmployeeByIdDTO GetEmployeeById(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            return Employee is null ? null : _mapper.Map<Employee, EmployeeByIdDTO>(Employee);
        }

        public int CreateEmployee(CreateEmployeeDTO employee)
        {
            var Employee = _mapper.Map<CreateEmployeeDTO, Employee>(employee);
            if(employee.ImageName is not null)
            {
                var uploadedImageName = _attachmentService.Upload(employee.ImageName, "Images");
                Employee.ImageName = uploadedImageName;
            }
            _unitOfWork.EmployeeRepository.Add(Employee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            if(Employee is null)
                return false;
            else
            {
                Employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Delete(Employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        public int UpdateEmployee(UpdateEmployeeDTO employee)
        {
            var Employee = _mapper.Map<UpdateEmployeeDTO, Employee>(employee);
            _unitOfWork.EmployeeRepository.Update(Employee);
            return _unitOfWork.SaveChanges();
        }
    }
}
