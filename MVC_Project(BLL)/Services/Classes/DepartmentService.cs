using MVC_Project_BLL_.DTOS.DepartmentDTO;
using MVC_Project_BLL_.Factories;
using MVC_Project_BLL_.Services.Interfaces;
using MVC_Project_DAL_.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services.Classes
{
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IEnumerable<DepartmentsDTO> AllDepartments()
        {
            var Departments = _unitOfWork.DepartmentRepository.GetAll();
            return Departments.Select(d => d.ToDepartmentsDTO());
        }
        public DepartmentByIdDTO? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentByIdDTO();
        }
        public int CreateDepartment(CreatedDepartmentDTO createdDepartmentDTO)
        {
            var department = createdDepartmentDTO.ToDepartmentEntity();
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }
        public int UpdateDepartment(UpdateDepartmentDTO UpdateDepartmentDTO)
        {
            _unitOfWork.DepartmentRepository.Update(UpdateDepartmentDTO.ToDepartmentEntity());
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null)
                return false;
            else
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

    }
}
