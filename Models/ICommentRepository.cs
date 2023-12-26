using NuGet.Protocol.Core.Types;

namespace FilmFolio.Models
{
    public interface ICommentRepository : IRepository<Comment>
    {
        void AddComment(Comment comment);
        void Save();
        List<Comment> GetCommentsByMovieId(int movieId);

    }
}
