using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryManagementSystem.ViewComponents
{
	public class HeaderViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;
		public HeaderViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			NavbarModel navbar = new NavbarModel();

			navbar.BookCategories = _context.BookCategory.ToList();
			return View(navbar);
		}
	}
}
