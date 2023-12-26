using FilmFolio.Models;
using FilmFolio.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmFolio.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepo;
        private readonly IMovieRepository _movieRepo;//foreign key ilişkisi nerdeyse onu yazıyoruz kitapidyi movierep ten çekiyo
        public readonly IWebHostEnvironment _webHostEnvironment;

        public FavoriteController(IFavoriteRepository favoriteRepo, IMovieRepository movieRepo, IWebHostEnvironment webHostEnvironment) //10 ve 15.satırla kitap türü interfaceini inherit ediyo
        {
            _favoriteRepo = favoriteRepo;
            _movieRepo = movieRepo;
            _webHostEnvironment = webHostEnvironment;//wwroottan image çekmek
        }

        public IActionResult Index()
        {
            List<Favorite> objFavoriteList = _favoriteRepo.GetAll(includeProps:"Movie").ToList();
            return View(objFavoriteList);
        }
        public IActionResult MovieAddEdit(int? id)
        {
            IEnumerable<SelectListItem> MovieList = _movieRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.MovieName,
                Value = u.Id.ToString()
            });
            ViewBag.MovieList = MovieList; //controllerdan viewa veri aktarıyo ama visa versa olmaz 

            if(id==null || id == 0)
            {
                //ADD
                return View();
            }
            else
            {
                //UPDATE
                Favorite? favoriteFF = _favoriteRepo.Get(u => u.Id == id); //gönderilen idyi nesneye çeviriyo "Movie" tableında o idyi bulup getirir
                if (favoriteFF == null)
                {
                    return NotFound();
                }
                return View(favoriteFF);
            }
        }

        [HttpPost]
        public IActionResult MovieAddEdit(Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                if (favorite.Id == 0)
                {
                    _favoriteRepo.Add(favorite);
                    TempData["ok"] = "Added to watchlist/favoritelist";
                }
                else
                {
                    _favoriteRepo.Update(favorite);
                    TempData["ok"] = "Editing list is succesful.";
                }
                _favoriteRepo.Save();

                return RedirectToAction("Index", "Favorite");//aynı controller içine yazdığım için 2.kısım gereksiz ama öğrenmek amaçlı
            }
            return View();
        }
        public IActionResult DeleteMovie(int? id)
        {
            IEnumerable<SelectListItem> MovieList = _movieRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.MovieName,
                Value = u.Id.ToString()
            });
            ViewBag.MovieList = MovieList; //controllerdan viewa veri aktarıyo ama visa versa olmaz 
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Favorite? favoriteFF = _favoriteRepo.Get(u=>u.Id==id); //gönderilen idyi nesneye çeviriyo "Favorite" tableında o idyi bulup getirir
            if (favoriteFF == null)
            {
                return NotFound();
            }
            return View(favoriteFF);
        }
        
        [HttpPost,ActionName ("DeleteMovie")]
        public IActionResult DeleteMoviePOST(int? id)
        {
            Favorite? favorite= _favoriteRepo.Get(u => u.Id == id);
            if (favorite== null)
            {
                return NotFound();
            }
            _favoriteRepo.Delete(favorite);
            _favoriteRepo.Save();
            TempData["ok"] = "Delete is succesful.";
            return RedirectToAction("Index", "Favorite");
        }
    }
}