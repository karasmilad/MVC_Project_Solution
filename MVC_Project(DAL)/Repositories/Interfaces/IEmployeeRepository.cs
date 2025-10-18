using MVC_Project_DAL_.Models.DepartmentModel;
using MVC_Project_DAL_.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL_.Repositories.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
    }
}
