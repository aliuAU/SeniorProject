using System.Linq.Expressions;

namespace FilmFolio.Models
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie movie);
        void Save();
        List<Movie> GetList(Expression<Func<Movie, bool>> filter = null, string includeProps = null);
        double GetAverageRatingForMovie(int movieId);
        void UpdateMovieRating(int movieId, double newRating);
    }
}

