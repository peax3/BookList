using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookList.Pages.Books
{
	public class CreateModel : PageModel
	{
		private readonly AppDbContext _db;

		public CreateModel(AppDbContext db)
		{
			this._db = db;
		}

		[BindProperty]
		public Book Book { get; set; }

		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				await _db.Books.AddAsync(Book);
				await _db.SaveChangesAsync();
				return RedirectToPage("Index");
			}
			else
			{
				return Page();
			}
		}
	}
}