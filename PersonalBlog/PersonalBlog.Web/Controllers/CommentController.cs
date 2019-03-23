using System;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Database;

namespace PersonalBlog.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class CommentController : Controller
	{
		public CommentController()
		{

		}

		[HttpPost]
		public IActionResult Add(Guid postId, string commentBody)
		{
			var userId = GetUserId();
			if (!_repository.Add(userId, postId, commentBody))
				return BadRequest();
			return Ok();
		}

		[HttpGet]
		public IActionResult Show(Guid postId)
		{
			try
			{
				var comments = _repository.Get(postId);
				return Ok(comments);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return BadRequest();
			}
		}

		private readonly ICommentRepository _repository;

		private Guid GetUserId()
		{
			var userLogin = HttpContext.Request.Cookies["userLogin"];
			var userRepo = new UserRepository();
			return userRepo.GetUserByLogin(userLogin).Id;
		}
	}
}