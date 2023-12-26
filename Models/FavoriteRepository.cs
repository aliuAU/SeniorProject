using FilmFolio.Utility;

namespace FilmFolio.Models
{
    public class FavoriteRepository : Repository<Favorite> , IFavoriteRepository
    {
        private AppDbContext _appDbContext;
        public FavoriteRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Favorite favorite)
        {
            _appDbContext.Update(favorite);
        }
    }
}
