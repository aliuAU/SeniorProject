using FilmFolio.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FilmFolio.Models
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private AppDbContext _appDbContext;
        public MovieRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _appDbContext.Update(movie);
        }
        public List<Movie> GetList(Expression<Func<Movie, bool>> filter = null, string includeProps = null)
        {
            IQueryable<Movie> query = _appDbContext.Set<Movie>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProps != null)
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }
        public double GetAverageRatingForMovie(int movieId)
        {
            return _appDbContext.Comments.Where(c => c.MovieId == movieId).Average(c => c.Rating);
        }

        public void UpdateMovieRating(int movieId, double newRating)
        {
            var movie = _appDbContext.Movies.Find(movieId);

            if (movie != null)
            {
                movie.AverageRating = newRating;
            }
        }
    }
}
