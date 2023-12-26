using FilmFolio.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace FilmFolio.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        internal DbSet<T> dbSet; // dbset = _appDbContext.MovieGenres
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
            this.dbSet= appDbContext.Set<T>();
            _appDbContext.Movies.Include(k => k.Genre);// foreign keyden data çekme
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteSpare(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> quest = dbSet;
            quest = quest.Where(filter);

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quest = quest.Include(includeProp);
                }
            }
            return quest.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(string? includeProps= null )
        {
            IQueryable<T> quest = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quest = quest.Include(includeProp);
                }
            }
            return quest.ToList();
        }
    }
}