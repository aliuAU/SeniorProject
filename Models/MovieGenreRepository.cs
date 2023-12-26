using FilmFolio.Utility;

namespace FilmFolio.Models
{
    public class MovieGenreRepository : Repository<MovieGenre> , IMovieGenreRepository
    {
        private AppDbContext _appDbContext;
        public MovieGenreRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(MovieGenre movieGenre)
        {
            _appDbContext.Update(movieGenre);
        }
    }
}
