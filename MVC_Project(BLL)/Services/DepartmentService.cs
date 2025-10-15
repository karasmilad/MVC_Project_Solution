using MVC_Project_BLL_.DTOS;
using MVC_Project_BLL_.Factories;
using MVC_Project_DAL_.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Services
{
    public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;

        public IEnumerable<DepartmentsDTO> AllDepartments()
        {
            var Departments = _departmentRepository.GetAll();
            return Departments.Select(d => d.ToDepartmentsDTO());
        }
        public DepartmentByIdDTO? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentByIdDTO();
        }
        public int CreateDepartment(CreatedDepartmentDTO createdDepartmentDTO)
        {
            var department = createdDepartmentDTO.ToDepartmentEntity();
            return _departmentRepository.Add(department);
        }
        public int UpdateDepartment(UpdateDepartmentDTO UpdateDepartmentDTO)
        {
            return _departmentRepository.Update(UpdateDepartmentDTO.ToDepartmentEntity());
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null)
                return false;
            else
            {
                int result = _departmentRepository.Delete(department);
                return result > 0 ? true : false;
            }
        }

    }
}
