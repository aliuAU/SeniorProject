namespace FilmFolio.Models
{
    public interface IMovieGenreRepository : IRepository<MovieGenre>
    {
        void Update(MovieGenre movieGenre);
        void Save();
    }
}
