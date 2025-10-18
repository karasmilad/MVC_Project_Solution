using MVC_Project_DAL_.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL_.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        int Add(TEntity department);
        int Delete(TEntity department);
        IEnumerable<TEntity> GetAll(bool Tracking = false);
        TEntity? GetById(int id);
        int Update(TEntity department);
    }
}
