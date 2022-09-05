using System.Linq.Expressions;

namespace EFCoreCodeFirstSample.IRepository
{
    public interface IGenericRepository <T> where T : class
    {
        //getting a list
        Task<IList<T>> GetAll(
            Expression<Func<T,bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
            );
        //getting one record
        Task<T> Get(Expression<Func<T,bool>> expression, List<string> includes = null);

        //inserting
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
