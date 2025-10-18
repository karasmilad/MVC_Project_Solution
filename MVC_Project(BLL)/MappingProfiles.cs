using AutoMapper;
using MVC_Project_BLL_.DTOS.EmployeeDTO;
using MVC_Project_DAL_.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL_
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeesDTO>();
            CreateMap<Employee, EmployeeByIdDTO>();
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();
        }
    }
}
