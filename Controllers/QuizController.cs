using Microsoft.AspNetCore.Mvc;

namespace FilmFolio.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
