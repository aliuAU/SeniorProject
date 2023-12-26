using FilmFolio.Models;
using FilmFolio.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmFolio.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]//authorize
    public class MovieGenreController : Controller
    {
        private readonly IMovieGenreRepository _movieGenreRepo;

        public MovieGenreController(IMovieGenreRepository context)
        {
            _movieGenreRepo = context;
        }

        public IActionResult Index()
        {
            List<MovieGenre> objMovieGenreList = _movieGenreRepo.GetAll().ToList();
            return View(objMovieGenreList);
        }
        public IActionResult MovieGenreAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieGenreAdd(MovieGenre movieGenre)
        {
            if (ModelState.IsValid)
            {
                _movieGenreRepo.Add(movieGenre);
                _movieGenreRepo.Save();
                TempData["ok"] = "Adding new genre is succesfull.";
                return RedirectToAction("Index", "MovieGenre");//aynı controller içine yazdığım için 2.kısım gereksiz ama öğrenmek amaçlı
            }
            return View();
        }
        public IActionResult EditMovieGenre(int? Id)
        {
            if (Id==null || Id == 0)
            {
                return NotFound();
            }
            MovieGenre? movieGenreFF = _movieGenreRepo.Get(u=>u.Id==Id); //gönderilen idyi nesneye çeviriyo "Genres" tableında o idyi bulup getirir
            if (movieGenreFF == null) {
                return NotFound();
            }
            return View(movieGenreFF);
        }

        [HttpPost]
        public IActionResult EditMovieGenre(MovieGenre movieGenre)
        {
            if (ModelState.IsValid)
            {
                _movieGenreRepo.Update(movieGenre);
                _movieGenreRepo.Save();
                TempData["ok"] = "Edit is succesful.";
                return RedirectToAction("Index", "MovieGenre");//aynı controller içine yazdığım için 2.kısım gereksiz ama öğrenmek amaçlı
            }
            return View();
        }
        public IActionResult DeleteMovieGenre(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            MovieGenre? movieGenreFF = _movieGenreRepo.Get(u=>u.Id==id); //gönderilen idyi nesneye çeviriyo "Genres" tableında o idyi bulup getirir
            if (movieGenreFF == null)
            {
                return NotFound();
            }
            return View(movieGenreFF);
        }
        
        [HttpPost,ActionName ("DeleteMovieGenre")]
        public IActionResult DeleteMovieGenrePOST(int? id)
        {
            MovieGenre? movieGenre = _movieGenreRepo.Get(u => u.Id == id);
            if (movieGenre== null)
            {
                return NotFound();
            }
            _movieGenreRepo.Delete(movieGenre);
            _movieGenreRepo.Save();
            TempData["ok"] = "Delete is succesful.";
            return RedirectToAction("Index", "MovieGenre");
        }
    }
}