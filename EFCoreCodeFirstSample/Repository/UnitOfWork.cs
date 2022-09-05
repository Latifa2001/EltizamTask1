using EFCoreCodeFirstSample.Data;
using EFCoreCodeFirstSample.IRepository;
using EFCoreCodeFirstSample.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstSample.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFCoreCodeFirstSampleContext _context;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;
        private IGenericRepository<Employee> _employees;
        private IGenericRepository<Department> _departments;
        public UnitOfWork(EFCoreCodeFirstSampleContext context)
        {
            _context = context;
        }
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

        public IGenericRepository<Employee> Employees => _employees ??= new GenericRepository<Employee>(_context);

        public IGenericRepository<Department> Departments => _departments ??= new GenericRepository<Department>(_context);

        public void Dispose()
        {
            //when am done free up the memory
            _context.Dispose();
            GC.SuppressFinalize(this); //GC is Garbage Collector
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
