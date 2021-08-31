using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookList.Pages.Books
{
	public class UpsertModel : PageModel
	{
		private readonly AppDbContext _db;

		public UpsertModel(AppDbContext db)
		{
			this._db = db;
		}

		[BindProperty]
		public Book Book { get; set; }

		public async Task<IActionResult> OnGet(int? id)
		{
			Book = new Book();
			if (id == null)
			{
				// create
				return Page();
			}
			else
			{
				Book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
				if (Book == null)
				{
					return NotFound();
				}
				// update
				return Page();
			}
		}

		public async Task<IActionResult> OnPost()
		{
			if (ModelState.IsValid)
			{
				if (Book.Id == 0)
				{
					await _db.Books.AddAsync(Book);
				}
				else
				{
					_db.Books.Update(Book);
				}

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