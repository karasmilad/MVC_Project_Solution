using MVC_Project_BLL_.DTOS;
using MVC_Project_DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_.Factories
{
    public static class DepartmentsFactory
    {
        public static DepartmentsDTO ToDepartmentsDTO(this Department department)
        {
            return new DepartmentsDTO
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
        }
        public static DepartmentByIdDTO ToDepartmentByIdDTO(this Department department)
        {
            return new DepartmentByIdDTO
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn,
                IsDeleted = department.IsDeleted
            };
        }
        public static Department ToDepartmentEntity(this CreatedDepartmentDTO createdDepartmentDTO)
        {
            return new Department
            {
                Name = createdDepartmentDTO.Name,
                Code = createdDepartmentDTO.Code,
                Description = createdDepartmentDTO.Description,
                CreatedOn = createdDepartmentDTO.DateOfCreation
            };
        }
        public static Department ToDepartmentEntity(this UpdateDepartmentDTO createdDepartmentDTO)
        {
            return new Department
            {
                Id = createdDepartmentDTO.DeptId,
                Name = createdDepartmentDTO.Name,
                Code = createdDepartmentDTO.Code,
                Description = createdDepartmentDTO.Description,
                CreatedOn = createdDepartmentDTO.DateOfCreation
            };
        }

    }
}
