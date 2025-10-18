using MVC_Project_DAL_.Data.DBContext;
using MVC_Project_DAL_.Models.DepartmentModel;
using MVC_Project_DAL_.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL_.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDBContext dBContext) :GenericRepository<Department>(dBContext), IDepartmentRepository
    {
        private readonly ApplicationDBContext _dBContext = dBContext;
    }
}
