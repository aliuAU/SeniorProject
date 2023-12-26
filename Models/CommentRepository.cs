using FilmFolio.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace FilmFolio.Models
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;

        public List<Comment> GetCommentsByMovieId(int movieId)
        {
            return _appDbContext.Comments
            .Include(c => c.User) 
            .Where(c => c.MovieId == movieId)
            .ToList();
        }
        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddComment(Comment comment)
        {
            _appDbContext.Comments.Add(comment);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
        void IRepository<Comment>.Add(Comment entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Comment>.Delete(Comment entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Comment>.DeleteSpare(IEnumerable<Comment> entities)
        {
            throw new NotImplementedException();
        }

        Comment IRepository<Comment>.Get(Expression<Func<Comment, bool>> filter, string? includeProps)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Comment> IRepository<Comment>.GetAll(string? includeProps)
        {
            IQueryable<Comment> quest = _appDbContext.Set<Comment>();
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quest = quest.Include(includeProp);
                }
            }
            return quest.ToList();
        }
  
    }
}
