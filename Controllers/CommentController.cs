using FilmFolio.Models;
using FilmFolio.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

public class CommentController : Controller
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public IActionResult Index(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index", "MoviePage");
        }

        List<Comment> objCommentList = _commentRepository.GetCommentsByMovieId(id.Value).ToList();
        return View(objCommentList);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Role_User)]
    public IActionResult AddComment(int movieId, string comment, int rating)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return BadRequest("Current user not found");
        }

        var newComment = new Comment
        {
            MovieId = movieId,
            UserId = userId,
            CommentText = comment,
            CommentDate = DateTime.Now,
            Rating = rating
        };

        _commentRepository.AddComment(newComment);
        _commentRepository.Save();

        return RedirectToAction("Index", "Comment", new { id = movieId });
    }
}
