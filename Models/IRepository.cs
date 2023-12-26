using System.Linq.Expressions;

namespace FilmFolio.Models
{
    public interface IRepository <T> where T : class 
    {
        //T-> MovieGenre
        IEnumerable<T> GetAll (string? includeProps = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProps = null);

        void Add(T entity);
        void Delete(T entity);
        void DeleteSpare(IEnumerable<T> entities);
    }
}
