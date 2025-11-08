using MVC_Project_DAL_.Data.DBContext;
using MVC_Project_DAL_.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL_.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
            _employeeRepository = new Lazy<IEmployeeRepository>(()=> new EmployeeRepository(dBContext)) ;
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dBContext));
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges()
        {
            return _dBContext.SaveChanges();
        }
    }
}
