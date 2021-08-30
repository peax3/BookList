using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookList.Controllers
{
	[Route("api/Book")]
	[ApiController]
	public class BookController : Controller
	{
		private readonly AppDbContext _db;

		public BookController(AppDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return Json(new { data = _db.Books.ToList() });
		}
	}
}