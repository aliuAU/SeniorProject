using FilmFolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmFolio.Controllers
{
    public class MoviePageController : Controller
    {
        private readonly IMovieRepository _movieRepo;

        public MoviePageController(IMovieRepository context)
        {
            _movieRepo = context;
        }
        public IActionResult Index(int? id)
        {
           // List<Movie> objMovieList = _movieRepo.GetAll().ToList();

            Movie movie = _movieRepo.Get(u => u.Id == id, includeProps: "Genre");
            return View(movie);// modelin ismini eklemek gerekiyormuş 


        }
    }
}
