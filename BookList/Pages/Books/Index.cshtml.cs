using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookList.Pages.Books
{
	public class IndexModel : PageModel
	{
		private readonly AppDbContext _db;

		public IndexModel(AppDbContext db)
		{
			this._db = db;
		}

		public IEnumerable<Book> Books { get; set; }

		public async Task OnGet()
		{
			Books = await _db.Books.ToListAsync();
		}

		public async Task<IActionResult> OnPostDelete(int id)
		{
			var book = await _db.Books.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			_db.Books.Remove(book);
			await _db.SaveChangesAsync();

			return RedirectToPage("Index");
		}
	}
}