using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookList.Pages.Books
{
	public class EditModel : PageModel
	{
		private readonly AppDbContext _db;

		public EditModel(AppDbContext db)
		{
			this._db = db;
		}

		[BindProperty]
		public Book Book { get; set; }

		public async Task OnGet(int id)
		{
			Book = await _db.Books.FindAsync(id);
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				var bookFromDb = await _db.Books.FindAsync(Book.Id);
				bookFromDb.Name = Book.Name;
				bookFromDb.ISBN = Book.ISBN;
				bookFromDb.Author = Book.Author;

				await _db.SaveChangesAsync();

				return RedirectToPage("Index");
			}
			else
			{
				return RedirectToPage();
			}
		}
	}
}