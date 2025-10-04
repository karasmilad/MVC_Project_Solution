using MVC_Project_DAL_.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL_.Repositories
{
    public class DepartmentRepository(ApplicationDBContext dBContext) : IDepartmentRepository
    {
        private readonly ApplicationDBContext _dBContext = dBContext;
        //Get BY ID
        public Department? GetById(int id)
        {
            return _dBContext.Departments.Find(id);
        }
        //Get All
        public IEnumerable<Department> GetAll(bool Tracking = false)
        {
            if (Tracking)
            {
                return _dBContext.Departments.ToList();
            }
            else
            {
                return _dBContext.Departments.AsNoTracking().ToList();
            }
        }
        //ADD
        public int Add(Department department)
        {
            _dBContext.Departments.Add(department);
            return _dBContext.SaveChanges();
        }
        //Update
        public int Update(Department department)
        {
            _dBContext.Departments.Update(department);
            return _dBContext.SaveChanges();
        }
        //Delete
        public int Delete(Department department)
        {
            _dBContext.Departments.Remove(department);
            return _dBContext.SaveChanges();
        }
    }
}
