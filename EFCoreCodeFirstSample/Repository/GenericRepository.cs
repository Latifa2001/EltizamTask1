using EFCoreCodeFirstSample.Data;
using EFCoreCodeFirstSample.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Services.Identity;
using System.Linq.Expressions;

namespace EFCoreCodeFirstSample.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EFCoreCodeFirstSampleContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(EFCoreCodeFirstSampleContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db; //get all records in table
            //if they need to includ any addintional info (for hotel, when it refrenced to a certen country, it will include all that country info with it.. feched from country table)
            if (includes != null)
            {
                foreach(var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            //expression means using (x => x.id = ..) which helps in retreaving the table with any sort of attribute
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db; //get all records in table

            //condtion
            if(expression != null)
            {
                query = query.Where(expression);
            }

            //if they need to includ any addintional info (for hotel, when it refrenced to a certen country, it will include all that country info with it.. feched from country table)
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }
            //expression means using (x => x.id = ..) which helps in retreaving the table with any sort of attribute
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            //check if the given table simular to the one that must be updated 
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
