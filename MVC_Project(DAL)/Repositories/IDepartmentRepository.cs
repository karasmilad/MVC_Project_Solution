
namespace MVC_Project_DAL_.Repositories
{
    public interface IDepartmentRepository
    {
        int Add(Department department);
        int Delete(Department department);
        IEnumerable<Department> GetAll(bool Tracking = false);
        Department? GetById(int id);
        int Update(Department department);
    }
}