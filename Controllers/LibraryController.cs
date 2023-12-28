using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
	public class LibraryController : Controller
	{
		protected readonly ApplicationDbContext _context;

		public LibraryController(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Show all book
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View(_context.Book.Include(a => a.BookCategory).ToList());
		}

		public IActionResult BookIssue(int bookId)
		{
			BookModel Book = _context.Book.Where(a => a.Id == bookId).FirstOrDefault();

			ViewBag.StudentList = new SelectList(_context.Member, "Id", "Name");
			return View(Book);
		}

		[HttpPost]
		public IActionResult BookIssue(int bookId, int memberId, DateTime expectedDate)
		{
			BookModel book = _context.Book.Where(a => a.Id == bookId).FirstOrDefault();

			MemberModel member = _context.Member.Where(a => a.Id == memberId).FirstOrDefault();

			BookIssueModel bookIssue = new BookIssueModel
			{
				BookId = book.Id,
				IssueDate = DateTime.Now,
				ExpectedReturnDate = expectedDate,
				MemberId = member.Id,
				ActualRetuerndate = null,
			};

			_context.BookIssue.Add(bookIssue);
			_context.SaveChanges();

			return RedirectToAction("IssuedList");
		}

		public IActionResult IssuedList()
		{
			return View(_context.BookIssue.Include(a => a.Book).Include(a => a.Member)
				.Where(a => a.ActualRetuerndate == null).ToList());
		}

		public IActionResult ReturningBook(int bookId, int memberId)
		{
			BookIssueModel bookIssue = _context.BookIssue.Where(a => a.BookId == bookId && a.MemberId == memberId && a.ActualRetuerndate == null)
				.FirstOrDefault();

			bookIssue.ActualRetuerndate = DateTime.Now;


			_context.BookIssue.Update(bookIssue);
			_context.SaveChanges();

			if (bookIssue.ExpectedReturnDate.Date < DateTime.Now)
			{
				int days = (bookIssue.ActualRetuerndate.Value - bookIssue.ExpectedReturnDate).Days;
				FineModel fine = new FineModel
				{
					BookIssueId = bookIssue.Id,
					Amount = 100 * days,
				};

				_context.Fine.Add(fine);
				_context.SaveChanges();
			}

			return RedirectToAction("IssuedList");
		}

		public IActionResult FineList()
		{
			return View(_context.Fine.Include(a => a.BookIssue)
				.Include("BookIssue.Book").Include("BookIssue.Member").OrderByDescending(a => a.Id).ToList());
		}

		public IActionResult BookCateogrylist(int Id)
		{
			return View(_context.Book.Include(a => a.Publisher).Where(a => a.BookTypeId == Id).ToList());
		}

        
    }
}
