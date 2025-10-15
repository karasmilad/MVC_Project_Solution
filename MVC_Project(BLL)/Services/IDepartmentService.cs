using MVC_Project_BLL_.DTOS;

namespace MVC_Project_BLL_.Services
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentsDTO> AllDepartments();
        int CreateDepartment(CreatedDepartmentDTO createdDepartmentDTO);
        bool DeleteDepartment(int id);
        DepartmentByIdDTO? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDTO UpdateDepartmentDTO);
    }
}