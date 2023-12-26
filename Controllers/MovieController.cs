using FilmFolio.Models;
using FilmFolio.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmFolio.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IMovieGenreRepository _movieGenreRepo;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(IMovieRepository context, IMovieGenreRepository movieGenreRepo, IWebHostEnvironment webHostEnvironment) //10 ve 15.satırla kitap türü interfaceini inherit ediyo
        {
            _movieRepo = context;
            _movieGenreRepo = movieGenreRepo;
            _webHostEnvironment = webHostEnvironment;//wwroottan image çekmek
        }
        public IActionResult Index(string searchTerm)
        {
            List<Movie> objMovieList = _movieRepo.GetAll(includeProps:"Genre").ToList();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // If search term is provided, filter movies by name
                objMovieList = _movieRepo.GetList(m => m.MovieName.Contains(searchTerm), includeProps: "Genre").ToList();
            }
            else
            {
                // If no search term, get all movies
                objMovieList = _movieRepo.GetAll(includeProps: "Genre").ToList();
            }

            return View(objMovieList);
        }
    
        [Authorize(Roles = UserRoles.Role_Admin)]//authorize
        public IActionResult MovieAddEdit(int? id)
        {
            IEnumerable<SelectListItem> MovieGenreList = _movieGenreRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Genre,
                Value = u.Id.ToString()
            });
            ViewBag.MovieGenreList = MovieGenreList; //controllerdan viewa veri aktarıyo ama visa versa olmaz 

            if(id==null || id == 0)
            {
                //ADD
                return View();
            }
            else
            {
                //UPDATE
                Movie? movieFF = _movieRepo.Get(u => u.Id == id); //gönderilen idyi nesneye çeviriyo "Movie" tableında o idyi bulup getirir
                if (movieFF == null)
                {
                    return NotFound();
                }
                return View(movieFF);
            }
        }
        [Authorize(Roles = UserRoles.Role_Admin)]//authorize
        [HttpPost]
        public IActionResult MovieAddEdit(Movie movie, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string moviePath = Path.Combine(wwwRootPath, @"img");
                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(moviePath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    movie.ImageURL = @"\img\" + file.FileName;
                }

                if (movie.Id == 0)
                {
                    _movieRepo.Add(movie);
                    TempData["ok"] = "Adding new movie is succesful.";
                }
                else
                {
                    _movieRepo.Update(movie);
                    TempData["ok"] = "Editing movie is succesful.";
                }
                _movieRepo.Save();

                return RedirectToAction("Index", "Movie");//aynı controller içine yazdığım için 2.kısım gereksiz ama öğrenmek amaçlı
            }
            return View();
        }
       
        [Authorize(Roles = UserRoles.Role_Admin)]//authorize
        public IActionResult DeleteMovie(int? id)
        {
          
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Movie? movieFF = _movieRepo.Get(u=>u.Id==id); //gönderilen idyi nesneye çeviriyo "Movie" tableında o idyi bulup getirir
            if (movieFF == null)
            {
                return NotFound();
            }
            return View(movieFF);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]//authorize
        [HttpPost,ActionName ("DeleteMovie")]
        public IActionResult DeleteMoviePOST(int? id)
        {
            Movie? movie = _movieRepo.Get(u => u.Id == id);
            if (movie== null)
            {
                return NotFound();
            }
            _movieRepo.Delete(movie);
            _movieRepo.Save();
            TempData["ok"] = "Delete is succesful.";
            return RedirectToAction("Index", "Movie");
        }
    }
}