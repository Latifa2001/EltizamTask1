using EFCoreCodeFirstSample.Data;
using EFCoreCodeFirstSample.Models;

namespace EFCoreCodeFirstSample.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<Department> Departments { get; }
        Task Save();
    }
}
