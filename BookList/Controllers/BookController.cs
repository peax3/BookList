using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookList.Controllers
{
	[Route("api/book")]
	[ApiController]
	public class BookController : Controller
	{
		private readonly AppDbContext _db;

		public BookController(AppDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Json(new { data = await _db.Books.ToListAsync() });
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var bookFromDb = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);

			if (bookFromDb == null) return Json(new { success = false, message = "Error while deleting" });

			_db.Books.Remove(bookFromDb);
			await _db.SaveChangesAsync();
			return Json(new { success = true, message = "Delete successful" });
		}
	}
}