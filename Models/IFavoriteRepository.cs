namespace FilmFolio.Models
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        void Update(Favorite favorite);
        void Save();
    }
}
